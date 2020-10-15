using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.API.Data.Repository.Interfaces;
using ToDo.API.Models;

namespace ToDo.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("users/post")]
        public async Task<IActionResult> Add(string email, string password)
        {
            User user = new User()
            {
                Email = email,
                Password = password,
            };
            if (!user.EmailIsValid())
                throw new Exception("Email is invalid.");

            user.SetRole();
            await _userRepository.Add(user);

            return Ok();
        }

        [HttpGet("users/index")]
        public async Task<IEnumerable<User>> GetAll()
        => await _userRepository.GetAll();

        
    }
}
