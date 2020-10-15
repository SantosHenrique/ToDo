using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.API.Data.Repository.Interfaces;
using ToDo.API.Models;
using ToDo.API.ViewModels;

namespace ToDo.API.Data.Repository
{
    public class ItemRepository : IItemRepository
    {
        private Context _context;

        public ItemRepository(Context context)
        {
            _context = context;
        }

        public async Task<bool> Add(Item item)
        {
            item.CreatedDate = DateTime.Now;
            await _context.Items.AddAsync(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Item item)
        {
            var updatedItem = _context.Items.First(i => i.Id == item.Id);

            item.UpdatedDate = DateTime.Now;
            _context.Entry(item).CurrentValues.SetValues(updatedItem);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Item> GetById(int id)
        => await _context.Items.FindAsync(id);

        public async Task<IEnumerable<ItemAdminViewModel>> GetAll(int pageNumber, int pageSize)
        => await _context.Items.Include(i => i.User)
            .Select(s => new ItemAdminViewModel
            {
                Email = s.User.Email,
                Description = s.Description,
                DueDate = s.DueDate
            }).Skip(((pageNumber) - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        public async Task<IEnumerable<ItemAdminViewModel>> GetAllDelayed(int pageNumber, int pageSize)
        => await _context.Items.Include(i => i.User)
            .Select(s => new ItemAdminViewModel
            {
                Email = s.User.Email,
                Description = s.Description,
                DueDate = s.DueDate
            }).Skip(((pageNumber) - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        public async Task<IEnumerable<Item>> GetByUserId(int userId)
        => await _context.Items.Where(i => i.UserId == userId).ToArrayAsync();

        public async Task<bool> Exists(int id)
        => await _context.Items.Where(i => i.Id == id).AnyAsync();


    }
}
