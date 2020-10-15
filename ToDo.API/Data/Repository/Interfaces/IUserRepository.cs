using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.API.Models;

namespace ToDo.API.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Add(User user);
        Task<IEnumerable<User>> GetAll();
        Task<User> Authenticate(string email, string password);
    }
}
