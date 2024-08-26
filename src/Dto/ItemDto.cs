using Anantarupa.Database.Models;

namespace Anantarupa.Dto
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set;}

        public ItemDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static ItemDto FromEntity(Item item){
            return new ItemDto(item.ItemId, item.ItemName);
        }
    }
}