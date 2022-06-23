using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using VaultPdmTest.Contracts;
using VaultPdmTest.Engine;

namespace VaultPdmTest
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            var container = GetContainer();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseAutofacWebApi(config);
            appBuilder.UseWebApi(config);
            appBuilder.UseAutofacMiddleware(container);
        }

        private IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<Repository>().As<IRepository>().SingleInstance();
            builder.RegisterType<CryptoServiceProvider>().As<ICryptoServiceProvider>();
            builder.RegisterType<UserManager>().As<IUserManager>();
            
            return builder.Build();
        }
    }
}
