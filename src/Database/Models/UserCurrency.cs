using System;
using System.Collections.Generic;

namespace Anantarupa.Database.Models
{
    public partial class UserCurrency
    {
        public long UserCurrencyId { get; set; }
        public long UserId { get; set; }
        public long? CurrencyType { get; set; }
        public long? Amount { get; set; }

        public virtual Currency? CurrencyTypeNavigation { get; set; }
        public virtual UserDatum User { get; set; } = null!;
    }
}
