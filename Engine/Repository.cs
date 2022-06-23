using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using VaultPdmTest.Contracts;
using VaultPdmTest.Models;

namespace VaultPdmTest.Engine
{
    public class Repository : IRepository
    {
        private const string ConnectionString = 
            "Host=balarama.db.elephantsql.com;Username=hpoleges;Password=BRiCGCpxl2h_PVthRqOgRc3RMkO6Ji0-;Database=hpoleges;";
        
        public async Task<bool> AddUser(string username, string password, string email)
        {
            var sqlPattern = "INSERT INTO users (Name, Password, Email) " +
                                   "VALUES(@Name, @Password, @Email)";

            try
            {
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    await conn.OpenAsync();
                    
                    var param = new
                    {
                        Name = username,
                        Password = password,
                        Email = email
                    };
                    
                    var result = await conn.ExecuteAsync(sqlPattern, param);

                    return await Task.FromResult(true);
                }
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<UserData> GetUserByEmail(string email)
        {
            var sqlPattern = "SELECT * FROM users WHERE Email = @Email ";

            try
            {
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstAsync<UserData>(sqlPattern, new { Email = email });

                    return await Task.FromResult(result);
                }
            }
            catch (Exception e)
            {
                return await Task.FromResult<UserData>(null);
            }
        }
        
        public async Task<UserData> GetUserByName(string name)
        {
            var sqlPattern = "SELECT * FROM users WHERE Name = @Name ";

            try
            {
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstAsync<UserData>(sqlPattern, new { Name = name });

                    return await Task.FromResult(result);
                }
            }
            catch (Exception e)
            {
                return await Task.FromResult<UserData>(null);
            }
        }
    }
}
