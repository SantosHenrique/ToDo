using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToDo.API.Data.Repository.Interfaces;
using ToDo.API.Idenity;
using ToDo.API.Models;

namespace ToDo.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        public AuthUser Authenticate(string email, string password)
        {
            var user = _userRepository.Authenticate(email, password).Result;
            if(user == null)
                throw new Exception("Incorrect login or password");

            var token = Token.GenerateToken(user);

            user.Password = string.Empty;

            // Retorna os dados
            return new AuthUser
            {
                User = user,
                Token = token
            };
        }
    }
}
