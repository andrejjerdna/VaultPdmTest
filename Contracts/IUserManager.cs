using System.Threading.Tasks;
using VaultPdmTest.Models;

namespace VaultPdmTest.Contracts
{
    public interface IUserManager
    {
        Task<RegisterResult> Register(string username, string password, string email);

        Task<LogInResult> LogIn(string username, string password);
    }
}