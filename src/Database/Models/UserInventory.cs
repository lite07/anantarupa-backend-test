using System;
using System.Collections.Generic;

namespace Anantarupa.Database.Models
{
    public partial class UserInventory
    {
        public int UserInventoryId { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }

        public UserInventory(int inventoryId, int userId, int itemId, int quantity)
        {
            UserInventoryId = inventoryId;
            UserId = userId;
            ItemId = itemId;
            Quantity = quantity;
        }

        public UserInventory(int userId, int itemId, int quantity)
        {
            UserId = userId;
            ItemId = itemId;
            Quantity = quantity;
        }

        public virtual Item Item { get; set; } = null!;
        public virtual UserDatum User { get; set; } = null!;
    }
}
