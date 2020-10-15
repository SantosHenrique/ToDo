using System.Collections.Generic;
using ToDo.Core.Models;

namespace ToDo.API.Models
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public IEnumerable<Item> Items { get; set; }

        public bool EmailIsValid() => Email.Contains("@");
        public void SetRole() => Role = Email == "admin@admin" ? "Admin" : "Basic";
    }
}
