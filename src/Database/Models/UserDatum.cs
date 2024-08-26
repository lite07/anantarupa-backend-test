using System;
using System.Collections.Generic;

namespace Anantarupa.Database.Models
{
    public partial class UserDatum
    {
        public UserDatum()
        {
            UserCurrencies = new HashSet<UserCurrency>();
            UserInventories = new HashSet<UserInventory>();
        }

        public long UserId { get; set; }
        public string Username { get; set; } = null!;
        public byte[]? JoinDate { get; set; }

        public virtual ICollection<UserCurrency> UserCurrencies { get; set; }
        public virtual ICollection<UserInventory> UserInventories { get; set; }
    }
}
