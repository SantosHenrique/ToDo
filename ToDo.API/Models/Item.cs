using System;
using ToDo.Core.Models;

namespace ToDo.API.Models
{
    public class Item : Entity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
