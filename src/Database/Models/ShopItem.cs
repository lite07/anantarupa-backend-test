using System;
using System.Collections.Generic;

namespace Anantarupa.Database.Models
{
    public partial class ShopItem
    {
        public int ShopItemId { get; set; }
        public int ItemId { get; set; }
        public int CurrencyType { get; set; }
        public int Price { get; set; }
        public int AllowedQuantity { get; set; }

        public virtual Currency CurrencyTypeNavigation { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;
    }
}
