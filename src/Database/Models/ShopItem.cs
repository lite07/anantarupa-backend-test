using System;
using System.Collections.Generic;

namespace Anantarupa.Database.Models
{
    public partial class ShopItem
    {
        public long ShopItemId { get; set; }
        public long ItemId { get; set; }
        public long CurrencyType { get; set; }
        public long Price { get; set; }
        public long AllowedQuantity { get; set; }

        public virtual Currency CurrencyTypeNavigation { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;
    }
}
