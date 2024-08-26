using System;
using System.Collections.Generic;

namespace Anantarupa.Database.Models
{
    public partial class Item
    {
        public Item()
        {
            ShopItems = new HashSet<ShopItem>();
            UserInventories = new HashSet<UserInventory>();
        }

        public long ItemId { get; set; }
        public string ItemName { get; set; } = null!;

        public virtual ICollection<ShopItem> ShopItems { get; set; }
        public virtual ICollection<UserInventory> UserInventories { get; set; }
    }
}
