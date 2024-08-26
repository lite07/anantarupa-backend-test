using System;
using System.Collections.Generic;

namespace Anantarupa.Database.Models
{
    public partial class UserCurrency
    {
        public int UserCurrencyId { get; set; }
        public int UserId { get; set; }
        public int? CurrencyType { get; set; }
        public int? Amount { get; set; }

        public virtual Currency? CurrencyTypeNavigation { get; set; }
        public virtual UserDatum User { get; set; } = null!;
    }
}
