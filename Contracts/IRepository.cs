using System.Threading.Tasks;
using VaultPdmTest.Models;

namespace VaultPdmTest.Contracts
{
    public interface IRepository
    {
        Task<bool> AddUser(string username, string password, string email);
        Task<UserData> GetUserByEmail(string email);
        Task<UserData> GetUserByName(string name);
    }
}
