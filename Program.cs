using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VaultPdmTest.Engine;

namespace VaultPdmTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = "http://localhost:12345/";

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Application deployed and hosted in {0}", baseAddress);
                Console.WriteLine("Press any key to terminate...");
                Console.ReadKey();
            }
        }
    }

}
