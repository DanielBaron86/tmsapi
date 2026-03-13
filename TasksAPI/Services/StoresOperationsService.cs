using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TasksAPI.DataBaseContext;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;


namespace TasksAPI.Services
{
    public class StoresOperationsService : IStoresOperationsService
    {
        private readonly DatabaseConnectContext _DBContext;
        private readonly IGoodsInstancesServices _goodsInstancesServices;
        private readonly IMapper _mapper;

        public StoresOperationsService(DatabaseConnectContext context, IMapper mapper, IGoodsInstancesServices goodsInstancesServices)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _DBContext = context ?? throw new ArgumentNullException(nameof(context));
            _goodsInstancesServices = goodsInstancesServices ?? throw new ArgumentNullException(nameof(goodsInstancesServices));
        }

        public async Task<StoreCartsEntityDetailsModel> AddDetailsToCart(int cartId, CreateRegisterOperationsModel operationModel)
        {

            var checkCart = await _DBContext.StoreCartsEntity.Where(t => t.Status == 1).FirstOrDefaultAsync(t => t.Id == cartId) ?? throw new ArgumentException("Cart not found or already closed");

            var items = await _DBContext.StoreCartsEntityDetails.Where(t => t.CartId == checkCart.Id).Where(t => t.GoodId == operationModel.GoodId).FirstOrDefaultAsync();
            if (operationModel.OperationType == 1)
            {
                _ = await _DBContext.GoodsTypesInstances.Where(t => t.Id == operationModel.GoodId && t.LocationId == checkCart.storeLocation).FirstOrDefaultAsync() ?? throw new Exception($"Item {operationModel.GoodId} not found in location {checkCart.storeLocation}");
            }

            if (items == null)
            {
                var checkItem = await _DBContext.GoodsTypesInstances.FirstAsync(t => t.Id == operationModel.GoodId) ?? throw new Exception($"Item with id {operationModel.GoodId} not found ");

                ValidateCartItem(operationModel, checkCart, checkItem);

                Decimal Finalprice = operationModel.Price;
                if (operationModel.OperationType == 2 && operationModel.Price > 0)
                {
                    Finalprice = Decimal.Multiply(Finalprice, -1);
                }
                await _goodsInstancesServices.CreateMovementHistory(operationModel.GoodId, 0, 0, (int)GoodsStatus.RESERVED, checkCart.clerktId);
                var patchResevered = new JsonPatchDocument();
                patchResevered.Replace("Status", GoodsStatus.RESERVED);
                patchResevered.ApplyTo(checkItem);


                var patchPrice = new JsonPatchDocument();
                patchPrice.Replace("Total", Decimal.Add(checkCart.Total, Finalprice));
                patchPrice.Replace("Remaining", Decimal.Add(checkCart.Remaining, Finalprice));

                patchPrice.ApplyTo(checkCart);

                var newOperation = new StoreCartsEntityDetails { CartId = cartId, OperationType = operationModel.OperationType, GoodId = operationModel.GoodId, Price = Finalprice, Notes = operationModel.Notes };

                _DBContext.StoreCartsEntityDetails.Add(newOperation);

                await _DBContext.SaveChangesAsync(CancellationToken.None);

                return _mapper.Map<StoreCartsEntityDetailsModel>(newOperation);
            }
            else
            {
                throw new Exception($"Item with id {operationModel.GoodId}  already in cart");
            }


        }

        private void ValidateCartItem(CreateRegisterOperationsModel operationModel, StoreCartsEntity checkCart, GoodsTypesInstances checkItem)
        {
            if (operationModel.OperationType == 1 && checkItem.Status != (int)GoodsStatus.AVAILABLE)
            {
                throw new Exception("Item is not available");
            }

            if (operationModel.OperationType == 2 && checkItem.Status != (int)GoodsStatus.SOLD)
            {
                throw new Exception("Unable to return this item");
            }

            if (operationModel.OperationType == 2 && checkItem.Status == (int)GoodsStatus.SOLD)
            {
                var checkOwner = _DBContext.AccountsGoodsEntity.Where(t => t.Status == (int)GoodsStatus.SOLD).FirstOrDefault(t => t.GoodId == checkItem.Id) ?? throw new Exception("Item for return not found");
                if (checkOwner.AccountId != checkCart.clientId) throw new Exception("Returned items belongs to another account");
            }
        }

        public Task<StoreCartsEntityDetailsModel> AddReturnToCart(int cartId, CreateRegisterOperationsModel operationModel)
        {
            throw new NotImplementedException();
        }

        public async Task<CashRegisterEntitySessionsModel> CloseSession(int sessionId)
        {
            var session = await _DBContext.CashRegisterEntitySessions.FirstOrDefaultAsync(t => t.Id == sessionId) ?? throw new ArgumentException("Session not found");

            var patcher = new JsonPatchDocument();
            patcher.Replace("SessionStatus", 2);
            patcher.Replace("CloseHour", DateTime.UtcNow);
            patcher.ApplyTo(session);
            await _DBContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<CashRegisterEntitySessionsModel>(session);

        }

        public async Task<CashRegisterEntityModel> CreateCashRegister(CreateCashRegisterEntity cashRegisterEntity)
        {
            var cashRegister = await _DBContext.CashRegisterEntity.Include(c => c.LocationTypesInstances)
                .Where(c => c.RegisterNumber == cashRegisterEntity.RegisterNumber)
                .Where(c => c.LocationID == cashRegisterEntity.LocationId)
                .FirstOrDefaultAsync();
            if (cashRegister != null)
            {
                throw new ArgumentException($"Register number {cashRegister.RegisterNumber} already exists in {cashRegister.LocationTypesInstances.Address} - {cashRegister.LocationTypesInstances.Description}");
            }
            var location = await _DBContext.LocationTypesInstances.Where(t => t.LocationTypeID == 2).FirstOrDefaultAsync(t => t.Id == cashRegisterEntity.LocationId);
            if (location == null || location.LocationTypeID != 2)
            {
                throw new ArgumentException("Location doesn't exists or wrong type");
            }
            else
            {
                var newCashRegister = _mapper.Map<CashRegisterEntity>(new CashRegisterEntityModel { LocationId = cashRegisterEntity.LocationId, RegisterNumber = cashRegisterEntity.RegisterNumber, Notes = cashRegisterEntity.Notes });
                _DBContext.CashRegisterEntity.Add(newCashRegister);
                await _DBContext.SaveChangesAsync(CancellationToken.None);
                return _mapper.Map<CashRegisterEntityModel>(newCashRegister);
            }

        }

        public async Task<CashRegisterEntityModel> UpdateRegister(int id, CreateCashRegisterEntity updateModel)
        {
            var registerToBeUpdated = await _DBContext.CashRegisterEntity.FirstOrDefaultAsync(t => t.Id == id);
            if (registerToBeUpdated == null)
            {
                throw new ArgumentException($"Register with id {id} not found");
            }
            var checkDuplicate = await _DBContext.CashRegisterEntity
                .Include(c => c.LocationTypesInstances)
                .Where(r => r.LocationID == updateModel.LocationId)
                .FirstOrDefaultAsync(t => t.RegisterNumber == updateModel.RegisterNumber);
            if (checkDuplicate != null)
            {
                throw new ArgumentException($"Register number {checkDuplicate.RegisterNumber} already exists in Location Id {checkDuplicate.LocationID} Address {checkDuplicate.LocationTypesInstances.Address} - {checkDuplicate.LocationTypesInstances.Description}");
            }
            _mapper.Map(updateModel, registerToBeUpdated);
            await _DBContext.SaveChangesAsync(CancellationToken.None);
            var updatedRegister = await _DBContext.CashRegisterEntity.Include(r => r.LocationTypesInstances).FirstOrDefaultAsync(g => g.Id == registerToBeUpdated.Id);

            return _mapper.Map<CashRegisterEntityModel>(updatedRegister);
        }

        public async Task<(IEnumerable<CashRegisterEntityModel>, PaginationMetadata)> GetCashRegisters(int pageNumber, int pageSize)
        {
            var collection = _DBContext.CashRegisterEntity
                    .Include(c => c.LocationTypesInstances)
                as IQueryable<CashRegisterEntity>;
            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);
            var collectionReturn = await collection.OrderBy(c => c.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            var returnCollection = _mapper.Map<IEnumerable<CashRegisterEntityModel>>(collectionReturn);
            return (returnCollection, paginationMetadata);
        }


        public async Task<CashRegisterEntityModel> GetCashRegistersById(int registerId)
        {
            var registerInstance = await _DBContext.CashRegisterEntity.Include(r => r.LocationTypesInstances).FirstOrDefaultAsync(t => t.Id == registerId);
            if (registerInstance == null)
            {
                throw new ArgumentException($"Register with id {registerId} not found");
            }
            return _mapper.Map<CashRegisterEntityModel>(registerInstance);
        }

        public async Task<(IEnumerable<CashRegisterEntityModel>, PaginationMetadata)> GetCashRegisterWithConditions(QueryFilters queryFilters)
        {
            var pageSize = queryFilters.pageSize;
            var pageNumber = queryFilters.pageNumber;
            var collection = _DBContext.CashRegisterEntity
                    .Include(c => c.LocationTypesInstances)
                as IQueryable<CashRegisterEntity>;
            if (queryFilters.queryFields != null)
            {
                foreach (var q in queryFilters.queryFields)
                {
                    collection = ServiceUtils.CreateFilter(collection, q.keyField, q.keyValue);
                }
            }
            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, queryFilters.pageSize, queryFilters.pageNumber);
            var collectionReturn = await collection.OrderByDescending(c => c.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            var returnCollection = _mapper.Map<IEnumerable<CashRegisterEntityModel>>(collectionReturn);

            return (returnCollection, paginationMetadata);
        }

        public async Task<(IEnumerable<CashRegisterEntitySessionsModel>, PaginationMetadata)> GetSession(int pageNumber, int pageSize)
        {
            var collection = _DBContext.CashRegisterEntitySessions.Include(s => s.User)
                as IQueryable<CashRegisterEntitySessions>;
            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);
            var collectionReturn = await collection.OrderByDescending(c => c.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            var returnCollection = _mapper.Map<IEnumerable<CashRegisterEntitySessionsModel>>(collectionReturn);
            return (returnCollection, paginationMetadata);
        }

        public async Task<(IEnumerable<CashRegisterEntitySessionsModel>, PaginationMetadata)> GetSessionrWithConditions(
            QueryFilters queryFilters)
        {
            var pageSize = queryFilters.pageSize;
            var pageNumber = queryFilters.pageNumber;
            var collection = _DBContext.CashRegisterEntitySessions.Include(s => s.User)
                as IQueryable<CashRegisterEntitySessions>;

            if (queryFilters.queryFields != null)
            {
                foreach (var q in queryFilters.queryFields)
                {
                    collection = ServiceUtils.CreateFilter(collection, q.keyField, q.keyValue);
                }
            }

            var totalItemCount = await collection.CountAsync();
            var paginationMetadata =
                new PaginationMetadata(totalItemCount, queryFilters.pageSize, queryFilters.pageNumber);
            var collectionReturn = await collection.OrderBy(c => c.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            var returnCollection = _mapper.Map<IEnumerable<CashRegisterEntitySessionsModel>>(collectionReturn);
            return (returnCollection, paginationMetadata);
        }


        public async Task<StoreCartsEntityModel> CreateNewCart(CreateNewCart CreateNewCart)
        {
            var session = await _DBContext.CashRegisterEntitySessions.Where(t => t.SessionStatus == 1).FirstOrDefaultAsync(t => t.AssignedClerk == CreateNewCart.ClerkId) ?? throw new ArgumentException($"No opened registers found for clerk with id {CreateNewCart.ClerkId} ");
            var location =
                await _DBContext.LocationTypesInstances.Where(l => l.LocationTypeID == (int)LocationTypesList.STORE).FirstOrDefaultAsync(l => l.Id == CreateNewCart.StoreLocation) ?? throw new ArgumentException($"No opened registers found for clerk with id {CreateNewCart.ClerkId} "); ;

            var cartToCreate = _mapper.Map<StoreCartsEntity>(new StoreCartsEntityModel { SessionId = session.Id, Status = 1, ClientId = CreateNewCart.ClientId, ClerktId = CreateNewCart.ClerkId, StoreLocation = CreateNewCart.StoreLocation });

            _DBContext.StoreCartsEntity.Add(cartToCreate);
            await _DBContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<StoreCartsEntityModel>(cartToCreate);

        }



        public async Task<StoreCartsEntityModelWithDetails> GetCartByID(int cartId)
        {
            var newCart = await _DBContext.StoreCartsEntity
                .Include(t => t.StoreCartsEntityDetails)
                .ThenInclude(d => d.GoodsTypesInstance)
                .Include(t => t.LocationTypesInstances)
                .Include(e => e.Accounts)
                .Include(e => e.UserEntity)
                .FirstOrDefaultAsync(t => t.Id == cartId);
            return _mapper.Map<StoreCartsEntityModelWithDetails>(newCart);
        }

        public async Task<StoreCartsEntityDetailsModel> GetCartDetilsByID(int cartDetailsId)
        {
            var details = await _DBContext.StoreCartsEntityDetails
                .FirstOrDefaultAsync(t => t.Id == cartDetailsId) ?? throw new ArgumentException("Fail");

            return _mapper.Map<StoreCartsEntityDetailsModel>(details);
        }

        public async Task<CashRegisterEntitySessionsModel> OpenNewSession(CreateCashRegisterSessionsEntityModel args)
        {
            _ = await _DBContext.CashRegisterEntity.FirstOrDefaultAsync(t => t.Id == args.CashRegisterId) ?? throw new ArgumentException("Register not found");

            var checkClerk = await _DBContext.CashRegisterEntitySessions.Where(t => t.AssignedClerk == args.AssignedClerk && t.SessionStatus == 1).FirstOrDefaultAsync();
            if (checkClerk != null)
            {
                throw new ArgumentException("Clerk is already assigned to register. Please close session with id " + checkClerk.Id);
            }

            var newSession = _mapper.Map<CashRegisterEntitySessions>(new CashRegisterEntitySessionsModel { AssignedClerk = args.AssignedClerk, SessionStatus = 1, CashRegisterId = args.CashRegisterId, OpenHour = DateTime.UtcNow, Notes = args.Notes });
            _DBContext.CashRegisterEntitySessions.Add(newSession);

            await _DBContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<CashRegisterEntitySessionsModel>(newSession);

        }

        public async Task<StoreCartsEntityModelWithDetails> PayForCartByID(int cartId, Decimal money)
        {
            var cartToBePayed = await _DBContext.StoreCartsEntity.FirstOrDefaultAsync(t => t.Id == cartId) ?? throw new ArgumentException("Cart not found");

            if (cartToBePayed.Status == 2) { throw new ArgumentException("Cart already paid and closed"); }

            if (cartToBePayed.Total < 0 && money > 0)
            {
                money = Decimal.Multiply(money, -1);
            }

            var applyPayment = new JsonPatchDocument();
            applyPayment.Replace("Paid", Decimal.Add(cartToBePayed.Paid, money));
            applyPayment.ApplyTo(cartToBePayed);
            applyPayment.Replace("Remaining", Decimal.Subtract(cartToBePayed.Total, cartToBePayed.Paid));
            applyPayment.ApplyTo(cartToBePayed);

            if (cartToBePayed.Remaining <= 0)
            {
                applyPayment.Replace("Status", 2);
                applyPayment.ApplyTo(cartToBePayed);

                var SellGoods = new List<SellGoods>();
                var returnGoodsIds = new List<int>();
                var goods = await _DBContext.StoreCartsEntityDetails.Where(t => t.CartId == cartToBePayed.Id).ToListAsync();
                foreach (var good in goods)
                {
                    if (good.OperationType == 1)
                    {
                        SellGoods.Add(
                            new SellGoods { ClerkId = cartToBePayed.clerktId, StoreLocation = cartToBePayed.storeLocation, GoodId = good.GoodId, Price = good.Price }
                            );
                    }

                    if (good.OperationType == 2)
                    {
                        returnGoodsIds.Add(good.GoodId);
                    }
                }
                await _goodsInstancesServices.SellItem(cartToBePayed.clientId, SellGoods);
                if (returnGoodsIds.Count > 0)
                {
                    await _goodsInstancesServices.ReturnItems(cartToBePayed.clientId, new ReturnGoods { ClerkId = cartToBePayed.clerktId, ReturnLocation = cartToBePayed.storeLocation, GoodId = returnGoodsIds });
                }

            }

            await _DBContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<StoreCartsEntityModelWithDetails>(cartToBePayed);
        }

        public async Task<int> RemoveCart(int cartId)
        {
            var cartItem = await _DBContext.StoreCartsEntity
                .Include(t => t.StoreCartsEntityDetails)
                .Where(t => t.Status == 1)
                .FirstOrDefaultAsync(t => t.Id == cartId) ?? throw new ArgumentException($"{nameof(cartId)} not found");

            foreach (var item in cartItem.StoreCartsEntityDetails)
            {
                await RemoveCartDetail(item.Id);
            }

            var applyStatus = new JsonPatchDocument();
            applyStatus.Replace("Status", 3);
            applyStatus.ApplyTo(cartItem);

            return await _DBContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<int> RemoveCartDetail(int cartDetailsId)
        {
            var cartItem = await _DBContext.StoreCartsEntityDetails.FirstOrDefaultAsync(t => t.Id == cartDetailsId) ?? throw new ArgumentException("Cart item not found");
            _DBContext.Remove(cartItem);
            var patchJson = new JsonPatchDocument();
            patchJson.Replace("Status", (int)GoodsStatus.AVAILABLE);
            await _goodsInstancesServices.PatchGood(cartItem.GoodId, patchJson);

            var updateCart = await _DBContext.StoreCartsEntity.FirstOrDefaultAsync(t => t.Id == cartItem.CartId);
            var patchCart = new JsonPatchDocument();

            patchCart.Replace("Total", Decimal.Subtract(updateCart.Total, cartItem.Price));
            patchCart.Replace("Remaining", Decimal.Subtract(updateCart.Remaining, cartItem.Price));
            patchCart.ApplyTo(updateCart);

            return await _DBContext.SaveChangesAsync(CancellationToken.None);

        }


        public async Task<(IEnumerable<StoreCartsEntityModelWithDetails>, PaginationMetadata)> GetCarts(int pageNumber, int pageSize)
        {
            var collection = _DBContext.StoreCartsEntity
                    .Include(t => t.StoreCartsEntityDetails)
                as IQueryable<StoreCartsEntity>;
            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionReturn = await collection.OrderByDescending(c => c.clientId)
                   .Skip(pageSize * (pageNumber - 1))
                   .Take(pageSize)
                   .ToListAsync();
            var returnCollection = _mapper.Map<IEnumerable<StoreCartsEntityModelWithDetails>>(collectionReturn);

            return (returnCollection, paginationMetadata);
        }

        public async Task<(IEnumerable<StoreCartsEntityModelWithDetails>, PaginationMetadata)> GetCartsWithConditions(QueryFilters queryFilters)
        {
            var pageSize = queryFilters.pageSize;
            var pageNumber = queryFilters.pageNumber;
            var collection = _DBContext.StoreCartsEntity
                    .Include(t => t.StoreCartsEntityDetails)
                as IQueryable<StoreCartsEntity>;
            if (queryFilters.queryFields != null)
            {
                foreach (var q in queryFilters.queryFields)
                {
                    collection = ServiceUtils.CreateFilter(collection, q.keyField, q.keyValue);
                }
            }
            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, queryFilters.pageSize, queryFilters.pageNumber);
            var collectionReturn = await collection.OrderByDescending(c => c.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            var returnCollection = _mapper.Map<IEnumerable<StoreCartsEntityModelWithDetails>>(collectionReturn);
            return (returnCollection, paginationMetadata);
        }

        public async Task<IEnumerable<StoreCartsEntityModelWithDetails>> GetCartsByAccountID(int accountId)
        {
            return _mapper.Map<IEnumerable<StoreCartsEntityModelWithDetails>>(await _DBContext.StoreCartsEntity.Include(t => t.StoreCartsEntityDetails).Where(t => t.clientId == accountId).ToListAsync(CancellationToken.None));
        }
    }
}

