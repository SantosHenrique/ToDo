using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.API.Data.Repository.Interfaces;
using ToDo.API.Models;

namespace ToDo.API.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string email, string password)
            => await _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();


        public async Task<bool> Add(User user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<User>> GetAll()
            => await _context.Users.Include(u => u.Items).ToListAsync();

    }
}
