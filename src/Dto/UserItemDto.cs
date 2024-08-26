using Anantarupa.Database.Models;

namespace Anantarupa.Dto
{
    public class UserItemDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public UserItemDto(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public static UserItemDto FromEntity(UserInventory inventory)
        {
            return new UserItemDto(inventory.Item.ItemName, inventory.Quantity);
        }
    }
}