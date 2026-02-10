using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TasksAPI.DataBaseContext;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;


namespace TasksAPI.Services
{
    public class GoodsInstancesInstancesServices : IGoodsInstancesServices
    {

        private readonly DatabaseConnectContext _dbContext;
        private readonly IMapper _mapper;
        
         
        public GoodsInstancesInstancesServices(DatabaseConnectContext context, IMapper mapper)
        {

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task< (IEnumerable<GoodsModels>, PaginationMetadata)> GetGoods(int pageNumber, int pageSize)
        {
           var collection =  _dbContext.GoodsTypesInstances
               .Include( i => i.GoodsTypes)
               .Include( i=> i.GoodsTypes.GoodModelBaseTypeEntity)
               .Include( i => i.LocationTypesInstances)
               .Include( i => i.LocationTypesInstances.LocationTypesEntity)
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

        public async Task<(IEnumerable<GoodsModels>, PaginationMetadata)> GetGoodsWithConditions(QueryFilters queryFilters)
        {
            var pageSize = queryFilters.pageSize;
           var pageNumber = queryFilters.pageNumber;
            var collection =  _dbContext.GoodsTypesInstances
                    .Include( i => i.GoodsTypes)
                    .Include( i=> i.GoodsTypes.GoodModelBaseTypeEntity)
                    .Include( i => i.LocationTypesInstances)
                    .Include( i => i.LocationTypesInstances.LocationTypesEntity)
                as IQueryable<GoodsTypesInstances>;
            
            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, queryFilters.pageSize, queryFilters.pageNumber);
            
            foreach (var q in queryFilters.queryFields)
            {
                collection = CreateFilter(collection, q.keyField, q.keyValue);
            }
            
            var collectionReturn = await collection.OrderBy(c => c.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            
            var returnCollection = _mapper.Map<IEnumerable<GoodsModels>>(collectionReturn);

            return (returnCollection,paginationMetadata);
        }


        public async Task<GoodsModels> GetGoodById(int goodId)
        {
            var good = await _dbContext.GoodsTypesInstances
                .Include( g =>g.GoodsTypes)
                .Include(g => g.LocationTypesInstances)
                .FirstOrDefaultAsync(i => i.Id == goodId);
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

        public async Task<(IEnumerable<v_GoodsTypesInstances>, PaginationMetadata)> GetGoodsByView(int pageNumber, int pageSize)
        {
            var collection = _dbContext.v_GoodsTypesInstances as IQueryable<v_GoodsTypesInstances>;
            
            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionReturn = await collection
                .OrderBy(c => c.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return (collectionReturn,paginationMetadata);
        }
        
        public static IQueryable<T> CreateFilter<T>(IQueryable<T> query, string propertyName, string searchTerm)
        {
            var parameter = Expression.Parameter(typeof(T),"e");
            var property = Expression.Property(parameter, propertyName);
            object value =  searchTerm;
            if (property.Type != typeof(string))
                value = Convert.ChangeType(value, property.Type);
            if (property.Type != typeof(string))
            {
                var filterLambda = Expression.Lambda<Func<T, bool>>(
                    Expression.Equal(
                        property,
                        Expression.Constant(value)
                    ),
                    parameter
                );
                return query.Where(filterLambda);
            }
            else
            {
                var filterLambda = Expression.Lambda<Func<T, bool>>(
                    Expression.Call(
                        property,
                        typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) }),
                        Expression.Constant(value)
                    ),
                    parameter
                );
              
                return query.Where(filterLambda);
            }
            

            
            
        }
    }
}
