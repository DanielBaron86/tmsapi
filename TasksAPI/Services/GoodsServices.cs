using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TasksAPI.DataBaseContext;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;


namespace TasksAPI.Services
{
    public class GoodsServices : IGoodsServices
    {

        private readonly DatabaseConnectContext _dbContext;
        private readonly IMapper _mapper;
        
         
        public GoodsServices(DatabaseConnectContext context, IMapper mapper)
        {

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task< (IEnumerable<GoodsModels>, PaginationMetadata)> GetGoods(int pageNumber, int pageSize)
        {
           var collection =  _dbContext.GoodsTypesInstances
               .Include( i => i.GoodsTypes)
               .Include( i => i.LocationTypesInstances)
               as IQueryable<GoodsTypesInstances>;

            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionReturn = await collection.OrderBy(c => c.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            
            var returnCollection = _mapper.Map<IEnumerable<GoodsModels>>(collectionReturn);

            return (returnCollection,paginationMetadata);
        }
        
        public async Task< (IEnumerable<GoodBaseTypeModel>, PaginationMetadata)>  GetBaseGoodTypes(int pageNumber, int pageSize)
        {
            
            
            
            var collection = _dbContext.GoodModelBaseType.AsEnumerable() as IQueryable<GoodModelBaseType>;
            
            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionReturn = await collection.OrderBy(c => c.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            
            var returnCollection = _mapper.Map<IEnumerable<GoodBaseTypeModel>>(collectionReturn);

            return (returnCollection,paginationMetadata);
        }
        

        public async Task < (IEnumerable<v_GoodsTypesModel>, PaginationMetadata)>GetGoodTypes(int pageNumber, int pageSize)
        {
            
            var collection = _dbContext.v_GoodsTypes as IQueryable<v_GoodsTypes>;
            
            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionReturn = await collection
                .OrderBy(c => c.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            
            var returnCollection = _mapper.Map<IEnumerable<v_GoodsTypesModel>>(collectionReturn);

            return (returnCollection,paginationMetadata);
        }

        public async Task<GoodsModels> GetGoodById(int goodId)
        {
            var good = await _dbContext.GoodsTypesInstances.FirstOrDefaultAsync(i => i.Id == goodId);
            return _mapper.Map<GoodsModels>(good);
        }

        public async Task<GoodsModels> CreateGood(CreateGoodsModels goodUnitModel)
        {
            _ = await _dbContext.GoodsTypes.FirstOrDefaultAsync(t => t.Id == goodUnitModel.GoodModelId) ??throw new ArgumentException("Item model not found");
            _ = await _dbContext.LocationTypesInstances.FirstOrDefaultAsync(t => t.Id == goodUnitModel.LocationId) ??throw new ArgumentException("Location not found");


            var goodToBeCreated = _mapper.Map<GoodsTypesInstances>(goodUnitModel);
            _dbContext.Add(goodToBeCreated);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<GoodsModels>(goodToBeCreated);

        }

        public async Task<bool> DeleteGoods(int goodId)
        {
            var item = await _dbContext.GoodsTypesInstances.FirstOrDefaultAsync(i => i.Id == goodId) ??throw new ArgumentException("Item not found");

            if (item.Status == (int)GoodsStatus.NONE || item.Status == (int)GoodsStatus.DELETED)
            {
                _dbContext.Remove(item);
                await _dbContext.SaveChangesAsync(CancellationToken.None);
                return true;

            }
            else
            {
                throw new ArgumentException("Unable to delete items with operation performeded on them. Please mark the item as deleted first");
            }
        }


        public async Task<GoodsModels> UpdateGood(int goodId, UpdateGoodsModels good)
        {
            var itemToBeUpdated = await _dbContext.GoodsTypesInstances.FirstOrDefaultAsync(g => g.Id == goodId) ??throw new ArgumentException("Item not found");
            if (good.LocationId != itemToBeUpdated.LocationId || (int)good.Status != itemToBeUpdated.Status)
            {
                await CreateMovementHistory(itemToBeUpdated.Id, 0, good.LocationId, (int)good.Status, 1);
            }
            _mapper.Map(good, itemToBeUpdated);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
            var updatedItem = await _dbContext.GoodsTypesInstances.FirstOrDefaultAsync(g => g.Id == itemToBeUpdated.Id);
            return _mapper.Map<GoodsModels>(updatedItem);
        }

        public async Task<GoodsModels> PatchGood(int goodId, JsonPatchDocument patchGood)
        {
            var goodToPatch = await _dbContext.GoodsTypesInstances.FirstOrDefaultAsync(g => g.Id == goodId) ??throw new ArgumentException("Item not found");


            if (goodToPatch.Status == (int)GoodsStatus.DELETED)
            {
               throw new ArgumentException("Can't edit items marked as DELETED");
            }
            patchGood.ApplyTo(goodToPatch);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<GoodsModels>(goodToPatch);

        }

        public async Task<IEnumerable<AccountsGoodsEntity>> SellItem(int clientId, ICollection<SellGoods> args)
        {
            _= await _dbContext.Accounts.FirstOrDefaultAsync(u => u.Id == clientId) ?? throw new Exception("Client not found");
            var list = new List<int>();
            foreach (var itemArg in args)
            {
                var itemToSell = await _dbContext.GoodsTypesInstances.FirstOrDefaultAsync(t => t.Id == itemArg.GoodId) ??throw new ArgumentException("Item not found");

                if (itemToSell.Status != (int)GoodsStatus.AVAILABLE && itemToSell.Status != (int)GoodsStatus.RESERVED)
                {
                   throw new ArgumentException($"Item {itemToSell.serialNumber} not available");
                }

                if(itemToSell.LocationId != itemArg.StoreLocation)
                {
                   throw new ArgumentException($"Item {itemToSell.serialNumber} not available in  location {itemArg.StoreLocation}");
                }
                
                list.Add(itemArg.GoodId);
                var soldItem = _mapper.Map<AccountsGoodsEntity>(new CreateSellGoods { AccountId = clientId, GoodId = itemArg.GoodId, Price = itemArg.Price, Status = GoodsStatus.SOLD });
                _dbContext.AccountsGoodsEntity.Add(soldItem);
                await CreateMovementHistory(itemToSell.Id, itemToSell.LocationId, 4, (int)GoodsStatus.SOLD, itemArg.ClerkId);
                var patchItem = new JsonPatchDocument();
                patchItem.Replace("Status", GoodsStatus.SOLD);
                patchItem.Replace("LocationId", 4);
                patchItem.ApplyTo(itemToSell);

               await _dbContext.SaveChangesAsync();

            }

            return await _dbContext.AccountsGoodsEntity.Where(i => list.Contains(i.GoodId)).Where(t => t.Status == (int)GoodsStatus.SOLD).ToListAsync();
        }

        public async Task<int> CreateMovementHistory(int itemId, int fromLocation, int toLocation, int toStatus, int userId)
        {

            var item = await _dbContext.GoodsTypesInstances.FirstOrDefaultAsync(t => t.Id == itemId) ?? throw new ArgumentException($"Item {itemId} not found") ;

            var itemMovement = new CreateItemMovementModel
            {
                GoodId = item.Id,
                FromLocation = fromLocation != 0 ? fromLocation : item.LocationId,
                ToLocation = toLocation != 0 ? toLocation : item.LocationId,
                FromStatus = item.Status,
                ToStatus = toStatus != 0 ? toStatus : item.Status,
                UserId = userId
            };

            var itemHistory = _mapper.Map<ItemMovementEntity>(itemMovement);
            _dbContext.ItemMovementEntity.Add(itemHistory);
            return await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<IEnumerable<AccountsGoodsEntity>> ReturnItems(int userId, ReturnGoods returnGoods)
        {

            foreach (var iD in returnGoods.GoodId)
            {
                var item = await _dbContext.GoodsTypesInstances.FirstOrDefaultAsync(t => t.Id == iD) ??throw new ArgumentException("Item not found");
                var itemInstance = await _dbContext.AccountsGoodsEntity.FirstOrDefaultAsync(t => t.GoodId == iD);
                await CreateMovementHistory(item.Id, 0, returnGoods.ReturnLocation, (int)GoodsStatus.RETURNED, returnGoods.ClerkId);

                var jSonPatchInstance = new JsonPatchDocument();
                jSonPatchInstance.Replace("Status", (int)GoodsStatus.RETURNED);

                var jSonPatch = new JsonPatchDocument();
                jSonPatch.Replace("Status", (int)GoodsStatus.RETURNED);
                jSonPatch.Replace("LocationId", returnGoods.ReturnLocation);

               
                jSonPatch.ApplyTo(item);
                jSonPatchInstance.ApplyTo(itemInstance);

                await _dbContext.SaveChangesAsync(CancellationToken.None);

            }
            return await _dbContext.AccountsGoodsEntity.Where(t => returnGoods.GoodId.Contains(t.GoodId) && t.Status == (int)GoodsStatus.RETURNED).ToListAsync();
        }

        public async Task<ICollection<ItemMovementEntity>> GetGoodHistorysById(int goodId)
        {
            return await _dbContext.ItemMovementEntity.Where(t => t.goodId == goodId).OrderBy(t => t.CreatedDate).ToListAsync();
        }
        

        public async Task<GoodsTypesModel> GetGoodTypeById(int goodId)
        {
            var good = await _dbContext.GoodsTypes.FirstOrDefaultAsync(i => i.Id == goodId);
            return _mapper.Map<GoodsTypesModel>(good);
        }

        public async Task<GoodsTypesModel> CreateGoodType(CreateGoodsTypesModel goodtypeModel)
        {
            var goodToBeCreated = _mapper.Map<GoodsTypes>(goodtypeModel);
            _dbContext.Add(goodToBeCreated);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<GoodsTypesModel>(goodToBeCreated);
        }

        public async Task<GoodsTypesModel> UpdateGoodType(int goodId, UpdateGoodsTypesModel goodType)
        {
            var itemToBeUpdated = await _dbContext.GoodsTypes.FirstOrDefaultAsync(g => g.Id == goodId) ?? throw new ArgumentException("Item not found");
            
            _mapper.Map(goodType, itemToBeUpdated);

            await _dbContext.SaveChangesAsync(CancellationToken.None);
            var updatedItem = await _dbContext.GoodsTypes.FirstOrDefaultAsync(g => g.Id == itemToBeUpdated.Id);
            return _mapper.Map<GoodsTypesModel>(updatedItem);
            
        }

        public async Task<bool> DeleteGoodTypess(int goodId)
        {
            var item = await _dbContext.GoodsTypes.FirstOrDefaultAsync(i => i.Id == goodId) ?? throw new ArgumentException("Item not found");
            var countItems = await _dbContext.GoodsTypesInstances.Where( t => t.GoodModelId == goodId).CountAsync();
            

            if (countItems <= 0)
            {
                _dbContext.Remove(item);
                await _dbContext.SaveChangesAsync(CancellationToken.None);
                return true;

            }
            else
            {
                throw new ArgumentException("Unable to delete item type. Already has items instances associated");
            }
            
        }
        
        public  async Task<GoodBaseTypeModel> CreateBaseType(CreateGoodBaseTypeModel goodBaseModel)
        {
            var goodBaseToBeCreated = _mapper.Map<GoodModelBaseType>(goodBaseModel);
            _dbContext.Add(goodBaseToBeCreated);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<GoodBaseTypeModel>(goodBaseToBeCreated);
        }

        public async Task<GoodBaseTypeModel> UpdateBaseType(int goodId, CreateGoodBaseTypeModel goodBaseModel)
        {
            var itemToBeUpdated = await _dbContext.GoodModelBaseType.FirstOrDefaultAsync(g => g.Id == goodId) ?? throw new ArgumentException("Item not found");
            
            _mapper.Map(goodBaseModel, itemToBeUpdated);

            await _dbContext.SaveChangesAsync(CancellationToken.None);
            var updatedItem = await _dbContext.GoodModelBaseType.FirstOrDefaultAsync(g => g.Id == itemToBeUpdated.Id);
            return _mapper.Map<GoodBaseTypeModel>(updatedItem);
        }
    }
}
