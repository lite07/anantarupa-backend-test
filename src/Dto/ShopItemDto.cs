using Anantarupa.Database.Models;

namespace Anantarupa.Dto
{
    public class ShopItemDto
    {
        public int ItemId { get; set; }
        public string ItemName { get; set;}
        public int Price { get; set; }
        public string CurrencyName { get; set; }
        public int AllowedQuantity {get; set; }

        public ShopItemDto(int itemId, string itemName, int price, string currencyName, int allowedQuantity)
        {
            ItemId = itemId;
            ItemName = itemName;
            Price = price; 
            CurrencyName = currencyName;
            AllowedQuantity = allowedQuantity;
        }

        public static ShopItemDto FromEntity(ShopItem shopItem){
            return new ShopItemDto(shopItem.ItemId, shopItem.Item.ItemName, shopItem.Price, shopItem.CurrencyTypeNavigation.CurrencyName, shopItem.AllowedQuantity);
        }
    }
}