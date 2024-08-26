using Anantarupa.Database;
using Anantarupa.Database.Models;
using Anantarupa.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Anantarupa.Services
{
    public class UserService
    {
        private readonly GameContext _db;

        public UserService(GameContext db)
        {
            _db = db;
        }

        public async Task<List<UserItemDto>> GetUserItems(int userId)
        {
            await _CheckUserExistance(userId);
            
            return await _GetUserInventory(userId).Select(inventory => UserItemDto.FromEntity(inventory))
                                                  .ToListAsync();
        }

        public async Task<List<UserCurrencyDto>> GetUserCurrencies(int userId)
        {
            await _CheckUserExistance(userId);

            return await _GetUserCurrencies(userId).Select(currency => UserCurrencyDto.FromEntity(currency))
                                                   .ToListAsync();
        }

        public async Task PurchaseItem(int userId, int itemId)
        {
            await _CheckUserExistance(userId);

            var shopItem = await _db.ShopItems.Where(shopItem => shopItem.ItemId == itemId).FirstOrDefaultAsync();

            if(shopItem == null) throw new KeyNotFoundException($"Cannot find item with id {itemId} in the shop");

            var userOwnedItem = await _GetUserInventory(userId).Where(inventory => inventory.ItemId == itemId).FirstOrDefaultAsync();

            if(userOwnedItem != null && userOwnedItem.Quantity >= shopItem.AllowedQuantity) {
                throw new InvalidOperationException($"Maximum quantity of {shopItem.AllowedQuantity} reached");
            }

            var userRequiredCurrency = await _GetUserCurrencies(userId).Where(currency => currency.CurrencyType == shopItem.CurrencyType).FirstOrDefaultAsync();
            
            if(userRequiredCurrency == null || userRequiredCurrency.Amount < shopItem.Price){
                throw new InvalidOperationException($"Insufficient funds");
            }

            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                userRequiredCurrency.Amount = userRequiredCurrency.Amount - shopItem.Price;
                _db.Update(userRequiredCurrency);

                if(userOwnedItem != null)
                {
                    userOwnedItem.Quantity++; 
                    _db.Update(userOwnedItem);
                }
                else
                {
                    userOwnedItem = new UserInventory(_LastGeneratedUserInventoryId() + 1, userId, itemId, 1);
                    await _db.AddAsync(userOwnedItem);
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch(Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task _CheckUserExistance(int userId)
        {
            var user = await _db.UserData.FindAsync(userId);
            
            if(user == null) throw new KeyNotFoundException($"Cannot find user with id {userId}");
        }

        private IQueryable<UserInventory> _GetUserInventory(int userId)
        {
            return _db.UserInventories.Include("Item").Where(inventory => inventory.UserId == userId);
        }

        private IQueryable<UserCurrency> _GetUserCurrencies(int userId)
        {
            return _db.UserCurrencies.Include("CurrencyTypeNavigation").Where(userCurrency => userCurrency.UserId == userId);
        }

        private int _LastGeneratedUserInventoryId()
        {
            var lastEntry = _db.UserInventories.OrderByDescending(inventory => inventory.UserInventoryId).FirstOrDefault();
            
            if(lastEntry == null) return 0;

            return lastEntry.UserInventoryId;
        }
    }
}