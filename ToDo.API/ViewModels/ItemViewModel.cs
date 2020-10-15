using ToDo.API.Models;

namespace ToDo.API.ViewModels
{
    public class ItemViewModel : Item
    {
        public bool Delayed { get; set; }
    }
}
