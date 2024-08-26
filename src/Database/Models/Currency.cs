using System;
using System.Collections.Generic;

namespace Anantarupa.Database.Models
{
    public partial class Currency
    {
        public Currency()
        {
            ShopItems = new HashSet<ShopItem>();
            UserCurrencies = new HashSet<UserCurrency>();
        }

        public long CurrencyId { get; set; }
        public string CurrencyName { get; set; } = null!;

        public virtual ICollection<ShopItem> ShopItems { get; set; }
        public virtual ICollection<UserCurrency> UserCurrencies { get; set; }
    }
}
