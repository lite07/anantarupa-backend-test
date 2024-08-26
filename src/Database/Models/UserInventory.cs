using System;
using System.Collections.Generic;

namespace Anantarupa.Database.Models
{
    public partial class UserInventory
    {
        public long UserInventoryId { get; set; }
        public long UserId { get; set; }
        public long ItemId { get; set; }
        public long Quantity { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual UserDatum User { get; set; } = null!;
    }
}
