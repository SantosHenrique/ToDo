using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.API.Data.Repository.Interfaces;
using ToDo.API.Models;
using ToDo.API.ViewModels;

namespace ToDo.API.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepository;
        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpPost("items/post")]
        [Authorize]
        public async Task<IActionResult> Add(Item item)
        {
            await _itemRepository.Add(item);
            return Ok();
        }

        [HttpPost("items/conclude")]
        [Authorize]
        public async Task<IActionResult> Conclude(int id)
        {
            Item item = await _itemRepository.GetById(id);
            if (item == null)
                return NotFound();

            item.FinishedDate = DateTime.Now;

            await _itemRepository.Update(item);

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, Item item)
        {
            string message = Validate(id, item);
            if (message != string.Empty)
            {
                throw new Exception($"Error - {message}");
            }
            await _itemRepository.Update(item);
            return Ok();
        }

        private string Validate(int id, Item item)
        {
            string message = string.Empty;
            bool finishedItem = _itemRepository.GetById(id).Result.FinishedDate != null;
            if (id != item.Id)
            {
                message += "Item not found";
            }
            else if (!_itemRepository.Exists(id).Result)
            {
                message += "Item not exists";
            }
            else if (finishedItem)
            {
                message += "Item finished";
            }

            return message;
        }

        [HttpGet("items/{userId:int}")]
        [Authorize]
        public IEnumerable<ItemViewModel> GetByUserId(int userId)
        {
            var items = _itemRepository.GetByUserId(userId).Result;
            List<ItemViewModel> itemsVM = new List<ItemViewModel>();
            foreach (var item in items)
            {
                itemsVM.Add(new ItemViewModel
                {
                    CreatedDate = item.CreatedDate,
                    Delayed = item.DueDate < DateTime.Now,
                    Description = item.Description,
                    DueDate = item.DueDate,
                    FinishedDate = item.FinishedDate,
                    Id = item.Id,
                    UpdatedDate = item.UpdatedDate,
                    UserId = item.UserId,
                    User = item.User
                });
            }
            return itemsVM;
        }

        [HttpGet("items/index/{pageNumber:int}/{pageSize:int}")]
        [Authorize]
        public async Task<IEnumerable<ItemAdminViewModel>> GetAll(int pageNumber, int pageSize)
        => await _itemRepository.GetAll(pageNumber, pageSize);

        [HttpGet("items/filter/{pageNumber:int}/{pageSize:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<ItemAdminViewModel>> GetAllDelayed(int pageNumber, int pageSize)
        => await _itemRepository.GetAllDelayed(pageNumber, pageSize);

    }
}
