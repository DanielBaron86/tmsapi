using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using TasksAPI.DataBaseContext;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;


namespace TasksAPI.Services
{
    public class GoodsServices : IGoodsServices
    {

        private readonly DatabaseConnectContext _DBContext;
        private readonly IMapper _mapper;

        public GoodsServices(DatabaseConnectContext context, IMapper mapper)
        {

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _DBContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<GoodsModels>> GetGoods()
        {
            var goods = await _DBContext.GoodsTypesInstances.ToListAsync();
            return _mapper.Map<IEnumerable<GoodsModels>>(goods);
        }

        public async Task<IEnumerable<GoodsTypesModel>> GetGoodTypes()
        {
            var types = await _DBContext.GoodsTypes.ToListAsync();
            return _mapper.Map<IEnumerable<GoodsTypesModel>>(types);
        }

        public async Task<GoodsModels> GetGoodById(int GoodID)
        {
            var good = await _DBContext.GoodsTypesInstances.FirstOrDefaultAsync(i => i.Id == GoodID);
            return _mapper.Map<GoodsModels>(good);
        }

        public async Task<GoodsModels> CreateGood(CreateGoodsModels GoodUnitModel)
        {
            _ = await _DBContext.GoodsTypes.FirstOrDefaultAsync(t => t.GoodModelId == GoodUnitModel.GoodModelId) ??throw new ArgumentException("Item model not found");
            _ = await _DBContext.LocationTypesInstances.FirstOrDefaultAsync(t => t.Id == GoodUnitModel.LocationId) ??throw new ArgumentException("Location not found");


            var goodToBeCreated = _mapper.Map<GoodsTypesInstances>(GoodUnitModel);
            _DBContext.Add(goodToBeCreated);
            await _DBContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<GoodsModels>(goodToBeCreated);

        }

        public async Task<bool> DeleteGoods(int GoodID)
        {
            var item = await _DBContext.GoodsTypesInstances.FirstOrDefaultAsync(i => i.Id == GoodID) ??throw new ArgumentException("Item not found");

            if (item.Status == (int)GoodsStatus.NONE || item.Status == (int)GoodsStatus.DELETED)
            {
                _DBContext.Remove(item);
                await _DBContext.SaveChangesAsync(CancellationToken.None);
                return true;

            }
            else
            {
                throw new ArgumentException("Unable to delete items with operation performeded on them. Please mark the item as deleted first");
            }
        }


        public async Task<GoodsModels> UpdateGood(int goodId, UpdateGoodsModels Good)
        {
            var itemToBeUpdated = await _DBContext.GoodsTypesInstances.FirstOrDefaultAsync(g => g.Id == goodId) ??throw new ArgumentException("Item not found");
            if (Good.LocationId != itemToBeUpdated.LocationId || (int)Good.Status != itemToBeUpdated.Status)
            {
                await CreateMovementHistory(itemToBeUpdated.Id, 0, Good.LocationId, (int)Good.Status, 1);
            }
            _mapper.Map(Good, itemToBeUpdated);
            await _DBContext.SaveChangesAsync(CancellationToken.None);
            var updatedItem = await _DBContext.GoodsTypesInstances.FirstOrDefaultAsync(g => g.Id == itemToBeUpdated.Id);
            return _mapper.Map<GoodsModels>(updatedItem);
        }

        public async Task<GoodsModels> PatchGood(int goodID, JsonPatchDocument patchGood)
        {
            var goodToPatch = await _DBContext.GoodsTypesInstances.FirstOrDefaultAsync(g => g.Id == goodID) ??throw new ArgumentException("Item not found");


            if (goodToPatch.Status == (int)GoodsStatus.DELETED)
            {
               throw new ArgumentException("Can't edit items marked as DELETED");
            }
            patchGood.ApplyTo(goodToPatch);
            await _DBContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<GoodsModels>(goodToPatch);

        }

        public async Task<IEnumerable<AccountsGoodsEntity>> SellItem(int clientId, ICollection<SellGoods> args)
        {
            _= _DBContext.Accounts.FirstOrDefault(u => u.Id == clientId) ?? throw new Exception("Client not found");
            var list = new List<int>();
            foreach (var itemArg in args)
            {
                var itemToSell = _DBContext.GoodsTypesInstances.FirstOrDefault(t => t.Id == itemArg.goodID) ??throw new ArgumentException("Item not found");

                if (itemToSell.Status != (int)GoodsStatus.AVAILABLE && itemToSell.Status != (int)GoodsStatus.RESERVED)
                {
                   throw new ArgumentException($"Item {itemToSell.serialNumber} not available");
                }

                if(itemToSell.LocationId != itemArg.storeLocation)
                {
                   throw new ArgumentException($"Item {itemToSell.serialNumber} not available in  location {itemArg.storeLocation}");
                }
                
                list.Add(itemArg.goodID);
                var soldItem = _mapper.Map<AccountsGoodsEntity>(new CreateSellGoods { AccountId = clientId, GoodId = itemArg.goodID, price = itemArg.price, Status = GoodsStatus.SOLD });
                _DBContext.AccountsGoodsEntity.Add(soldItem);
                await CreateMovementHistory(itemToSell.Id, itemToSell.LocationId, 4, (int)GoodsStatus.SOLD, itemArg.clerkId);
                var patchItem = new JsonPatchDocument();
                patchItem.Replace("Status", GoodsStatus.SOLD);
                patchItem.Replace("LocationId", 4);
                patchItem.ApplyTo(itemToSell);

                _DBContext.SaveChanges();

            }

            return await _DBContext.AccountsGoodsEntity.Where(i => list.Contains(i.GoodId)).Where(t => t.Status == (int)GoodsStatus.SOLD).ToListAsync();
        }

        public async Task<int> CreateMovementHistory(int itemID, int FromLocation, int ToLocation, int ToStatus, int userID)
        {

            var item = _DBContext.GoodsTypesInstances.FirstOrDefault(t => t.Id == itemID) ?? throw new ArgumentException($"Item {itemID} not found") ;

            var itemMovement = new CreateItemMovementModel
            {
                goodId = item.Id,
                FromLocation = FromLocation != 0 ? FromLocation : item.LocationId,
                ToLocation = ToLocation != 0 ? ToLocation : item.LocationId,
                FromStatus = item.Status,
                ToStatus = ToStatus != 0 ? ToStatus : item.Status,
                UserId = userID
            };

            var itemHistory = _mapper.Map<ItemMovementEntity>(itemMovement);
            _DBContext.ItemMovementEntity.Add(itemHistory);
            return await _DBContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<IEnumerable<AccountsGoodsEntity>> ReturnItems(int userID, ReturnGoods returnGoods)
        {

            foreach (var iD in returnGoods.goodID)
            {
                var item = _DBContext.GoodsTypesInstances.FirstOrDefault(t => t.Id == iD) ??throw new ArgumentException("Item not found");
                var itemInstance = _DBContext.AccountsGoodsEntity.FirstOrDefault(t => t.GoodId == iD);
                await CreateMovementHistory(item.Id, 0, returnGoods.returnLocation, (int)GoodsStatus.RETURNED, returnGoods.clerkId);

                var JSonPatchInstance = new JsonPatchDocument();
                JSonPatchInstance.Replace("Status", (int)GoodsStatus.RETURNED);

                var JSonPatch = new JsonPatchDocument();
                JSonPatch.Replace("Status", (int)GoodsStatus.RETURNED);
                JSonPatch.Replace("LocationId", returnGoods.returnLocation);

               
                JSonPatch.ApplyTo(item);
                JSonPatchInstance.ApplyTo(itemInstance);

                await _DBContext.SaveChangesAsync(CancellationToken.None);

            }
            return await _DBContext.AccountsGoodsEntity.Where(t => returnGoods.goodID.Contains(t.GoodId) && t.Status == (int)GoodsStatus.RETURNED).ToListAsync();
        }

        public async Task<ICollection<ItemMovementEntity>> GetGoodHistorysByID(int goodID)
        {
            return await _DBContext.ItemMovementEntity.Where(t => t.goodId == goodID).OrderBy(t => t.CreatedDate).ToListAsync();
        }

        public async Task<GoodsTypesModel> GetGoodTypeById(int GoodID)
        {
            var good = await _DBContext.GoodsTypes.FirstOrDefaultAsync(i => i.Id == GoodID);
            return _mapper.Map<GoodsTypesModel>(good);
        }

        public async Task<GoodsTypesModel> CreateGoodType(CeateGoodsTypesModel GoodtypeModel)
        {
            var goodToBeCreated = _mapper.Map<GoodsTypes>(GoodtypeModel);
            _DBContext.Add(goodToBeCreated);
            await _DBContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<GoodsTypesModel>(goodToBeCreated);
        }

        public async Task<GoodsTypesModel> UpdateGoodType(int goodId, UpdateGoodsTypesModel GoodType)
        {
            var itemToBeUpdated = await _DBContext.GoodsTypes.FirstOrDefaultAsync(g => g.Id == goodId) ?? throw new ArgumentException("Item not found");
            
            _mapper.Map(GoodType, itemToBeUpdated);

            await _DBContext.SaveChangesAsync(CancellationToken.None);
            var updatedItem = await _DBContext.GoodsTypes.FirstOrDefaultAsync(g => g.Id == itemToBeUpdated.Id);
            return _mapper.Map<GoodsTypesModel>(updatedItem);
            
        }

        public async Task<bool> DeleteGoodTypess(int GoodID)
        {
            var item = await _DBContext.GoodsTypes.FirstOrDefaultAsync(i => i.Id == GoodID) ?? throw new ArgumentException("Item not found");
            var countItems = _DBContext.GoodsTypesInstances.Where( t => t.GoodModelId == GoodID).Count();
            

            if (countItems <= 0)
            {
                _DBContext.Remove(item);
                await _DBContext.SaveChangesAsync(CancellationToken.None);
                return true;

            }
            else
            {
                throw new ArgumentException("Unable to delete item type. Already has items instances associated");
            }
            
        }
    }
}
