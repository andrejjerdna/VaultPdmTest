using System;
using System.Threading.Tasks;
using VaultPdmTest.Contracts;
using VaultPdmTest.Models;

namespace VaultPdmTest.Engine
{
    public class UserManager : IUserManager
    {
        private readonly IRepository _repository;
        private readonly ICryptoServiceProvider _cryptoServiceProvider;

        public UserManager(IRepository repository, ICryptoServiceProvider cryptoServiceProvider)
        {
            _repository = repository;
            _cryptoServiceProvider = cryptoServiceProvider;
        }
        
        public async Task<RegisterResult> Register(string username, string password, string email)
        {
            var passwordHash = _cryptoServiceProvider.GetHash(password);

            var user = await _repository.GetUserByEmail(email);

            if (user != null)
            {
                return new RegisterResult
                {
                    Success = false,
                    Message = "This user is in data base!"
                };
            }

            var result = await _repository.AddUser(username, passwordHash, email);

            if (result)
            {
                return new RegisterResult
                {
                    Success = true,
                    Message = "Completed!"
                };
            }

            return new RegisterResult
            {
                Success = false,
                Message = "Failed to add user to database!"
            }; 
        }
        
        public async Task<LogInResult> LogIn(string username, string password)
        {
            var user = await _repository.GetUserByName(username);

            if (user == null)
            {
                return new LogInResult
                {
                   Success = false,
                   Message = "This user isn't in data base!",
                   AuthorizedState = AuthorizedState.BadRequest
                };
            }
            
            var passwordHash = _cryptoServiceProvider.GetHash(password);

            if (user.Password == passwordHash)
            {
                return new LogInResult
                {
                    Success = true,
                    Message = "Completed!",
                    UserData = user,
                    AuthorizedState = AuthorizedState.Completed
                };
            }
            
            return new LogInResult
            {
                Success = false,
                Message = "Password in incorrect!",
                AuthorizedState = AuthorizedState.BadPassword
            };
        }
    }
}