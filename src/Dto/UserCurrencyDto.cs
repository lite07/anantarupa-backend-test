using Anantarupa.Database.Models;

namespace Anantarupa.Dto
{
    public class UserCurrencyDto
    {
        public string Type { get; set; }
        public int Amount { get; set; }

        public UserCurrencyDto(string type, int amount)
        {
            Type = type;
            Amount = amount;
        }

        public static UserCurrencyDto FromEntity(UserCurrency userCurrency)
        {
            return new UserCurrencyDto(userCurrency.CurrencyTypeNavigation.CurrencyName, (int)userCurrency.Amount);
        }
    }
}