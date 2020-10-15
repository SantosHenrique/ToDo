using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.API.Models;
using ToDo.API.ViewModels;

namespace ToDo.API.Data.Repository.Interfaces
{
    public interface IItemRepository 
    {
        Task<bool> Add(Item item);
        Task<bool> Update(Item item);
        Task<Item> GetById(int id);
        Task<IEnumerable<ItemAdminViewModel>> GetAll(int pageNumber, int pageSize);
        Task<IEnumerable<Item>> GetByUserId(int userId);
        Task<IEnumerable<ItemAdminViewModel>> GetAllDelayed(int pageNumber, int pageSize);
        Task<bool> Exists(int id);
    }
}
