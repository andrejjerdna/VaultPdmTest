using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using VaultPdmTest.Contracts;
using VaultPdmTest.Models;

namespace VaultPdmTest.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [ActionName("register")]
        public async Task<IHttpActionResult> Register(string username, string password, string email)
        {
            var result = await _userManager.Register(username, password, email);
            
            if(result.Success)
                return Ok(result);
            
            return BadRequest(result.Message);
        }

        [HttpPost]
        [ActionName("login")]
        public async Task<IHttpActionResult> Login(string username, string password)
        {
            var result = await _userManager.LogIn(username, password);

            switch (result.AuthorizedState)
            {
                case AuthorizedState.Completed: 
                    return Ok(result);
                case AuthorizedState.BadPassword:
                    return Unauthorized();
                case AuthorizedState.BadRequest:
                    return BadRequest(result.Message);
                default:
                    return BadRequest(result.Message);
            }
        }
    }
}
