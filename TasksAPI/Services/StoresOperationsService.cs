using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TasksAPI.DataBaseContext;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;


namespace TasksAPI.Services
{
    public class StoresOperationsService: IStoresOperationsService
    {
        private readonly DatabaseConnectContext _DBContext;
        private readonly IGoodsServices _goodsServices;
        private readonly IMapper _mapper;

        public StoresOperationsService(DatabaseConnectContext context, IMapper mapper, IGoodsServices goodsServices) {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _DBContext = context ?? throw new ArgumentNullException(nameof(context));
            _goodsServices = goodsServices ?? throw new ArgumentNullException(nameof(goodsServices));
        }

        public async Task<StoreCartsEntity_DetailsModel> AddDetailsToCart(int cartId, CreateRegisterOperationsModel operationModel)
        {
            
            var checkCart = _DBContext.StoreCartsEntity.Where(t => t.Status == 1).FirstOrDefault(t => t.Id == cartId) ??throw new ArgumentException("Cart not found or already closed");

            var items = _DBContext.StoreCartsEntityDetails.Where(t => t.CartId == checkCart.Id).Where(t => t.GoodId == operationModel.GoodId).FirstOrDefault();
            if(operationModel.OperationType ==1)
            {
                _= _DBContext.GoodsTypesInstances.Where(t => t.Id == operationModel.GoodId && t.LocationId == checkCart.storeLocation).FirstOrDefault() ?? throw new Exception("Item not found in this location");
            }
            
            if (items == null)
            {
                var checkItem = _DBContext.GoodsTypesInstances.First(t => t.Id == operationModel.GoodId) ?? throw new Exception($"Item with id {operationModel.GoodId} not found ");

                ValidateCartItem(operationModel, checkCart, checkItem);

                Decimal Finalprice = operationModel.Price;
                if (operationModel.OperationType == 2 && operationModel.Price > 0)
                {
                    Finalprice = Decimal.Multiply(Finalprice, -1);
                }
                await _goodsServices.CreateMovementHistory(operationModel.GoodId, 0, 0, (int)GoodsStatus.RESERVED, checkCart.clerktId);
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

                return _mapper.Map<StoreCartsEntity_DetailsModel>(newOperation);
            }
            else {
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

        public Task<StoreCartsEntity_DetailsModel> AddReturnToCart(int cartId, CreateRegisterOperationsModel operationModel)
        {
            throw new NotImplementedException();
        }

        public async Task<CashRegisterEntity_SessionsModel> CloseSession(int sessionId)
        {
            var session = _DBContext.CashRegisterEntitySessions.FirstOrDefault(t => t.Id == sessionId) ??throw new ArgumentException("Session not found");  

            var patcher = new JsonPatchDocument();
            patcher.Replace("SessionStatus", 2);
            patcher.Replace("CloseHour", DateTime.UtcNow);
            patcher.ApplyTo(session);
            await _DBContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<CashRegisterEntity_SessionsModel>(session);

        }

        public async Task<CashRegisterEntityModel> CreateCashRegister(CreateCashRegisterEntity cashRegisterEntity)
        {
            var location = _DBContext.LocationTypesInstances.Where(t => t.LocationTypeID == 2).FirstOrDefault(t => t.Id == cashRegisterEntity.LocationID);
            if(location == null || location.LocationTypeID != 2) {
               throw new ArgumentException("Location doesn't exists or wrong type");
            }
            else {
                var newCashRegister = _mapper.Map<CashRegisterEntity>(new CashRegisterEntityModel { LocationID = cashRegisterEntity.LocationID, Notes = cashRegisterEntity.Notes });
                _DBContext.CashRegisterEntity.Add(newCashRegister);
                await _DBContext.SaveChangesAsync(CancellationToken.None);
                return _mapper.Map<CashRegisterEntityModel>(newCashRegister);
            }
            
        }


        public async Task<StoreCartsEntityModel> CreateNewCart(CreateNewCart CreateNewCart)
        {
            var session = _DBContext.CashRegisterEntitySessions.Where( t => t.SessionStatus == 1).FirstOrDefault( t => t.AssignedClerk == CreateNewCart.clerkId) ??throw new ArgumentException($"No opened registers found for clerk with id {CreateNewCart.clerkId} ");
            
            var cartToCreate = _mapper.Map<StoreCartsEntity>(new StoreCartsEntityModel { SessionID = session.Id, Status = 1 ,clientId = CreateNewCart.clientId, clerktId = CreateNewCart.clerkId, storeLocation= CreateNewCart.storeLocation });

            _DBContext.StoreCartsEntity.Add(cartToCreate);
            await _DBContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<StoreCartsEntityModel>(cartToCreate);

        }

        public async Task<StoreCartsEntityModelWithDetails> GetCartByID(int cartId)
        {
            var newCart = await _DBContext.StoreCartsEntity.Include( t => t.StoreCartsEntityDetails).FirstOrDefaultAsync(t => t.Id == cartId);
            return _mapper.Map<StoreCartsEntityModelWithDetails>(newCart);
        }

        public async Task<StoreCartsEntity_DetailsModel> GetCartDetilsByID(int cartDetailsId)
        {
            var details =await _DBContext.StoreCartsEntityDetails.FirstOrDefaultAsync(t => t.Id == cartDetailsId) ??throw new ArgumentException("Fail");

            return _mapper.Map<StoreCartsEntity_DetailsModel>(details);
        }

        public async  Task<CashRegisterEntity_SessionsModel> OpenNewSession(CreateCashRegisterSessionsEntityModel args)
        {
            _= _DBContext.CashRegisterEntity.FirstOrDefault( t => t.Id ==args.CashRegisterID) ??throw new ArgumentException("Register not found");

            var checkClerk = _DBContext.CashRegisterEntitySessions.Where( t=> t.AssignedClerk == args.AssignedClerk && t.SessionStatus == 1).FirstOrDefault();
            if(checkClerk != null ) {
               throw new ArgumentException("Clerk is already assigned to register. Please close session with id " + checkClerk.Id);
            }

            var newSession = _mapper.Map<CashRegisterEntitySessions>(new CashRegisterEntity_SessionsModel { AssignedClerk = args.AssignedClerk, SessionStatus = 1, CashRegisterID = args.CashRegisterID, OpenHour = DateTime.UtcNow, Notes = args.Notes });
            _DBContext.CashRegisterEntitySessions.Add(newSession);

            await _DBContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<CashRegisterEntity_SessionsModel>(newSession);

        }

        public async Task<StoreCartsEntityModelWithDetails> PayForCartByID(int cartId,Decimal money)
        {
            var cartToBePayed = _DBContext.StoreCartsEntity.FirstOrDefault(t => t.Id == cartId) ??throw new ArgumentException("Cart not found");
            
            if(cartToBePayed.Status == 2) {throw new ArgumentException("Cart already paid and closed"); }

            if(cartToBePayed.Total < 0 && money > 0)
            {
                money = Decimal.Multiply(money, -1);
            }

            var applyPayment = new JsonPatchDocument();
            applyPayment.Replace("Paid", Decimal.Add(cartToBePayed.Paid,money));
            applyPayment.ApplyTo(cartToBePayed);
            applyPayment.Replace("Remaining", Decimal.Subtract(cartToBePayed.Total,cartToBePayed.Paid));
            applyPayment.ApplyTo(cartToBePayed);

            if(cartToBePayed.Remaining <= 0)
            {
                applyPayment.Replace("Status", 2);
                applyPayment.ApplyTo(cartToBePayed);

                var SellGoods = new List<SellGoods>();
                var returnGoodsIds = new List<int>();
                var goods = _DBContext.StoreCartsEntityDetails.Where( t => t.CartId == cartToBePayed.Id).ToList();
                foreach (var good in goods) { 
                    if(good.OperationType == 1)
                    {
                        SellGoods.Add( 
                            new SellGoods { clerkId = cartToBePayed.clerktId, storeLocation = cartToBePayed.storeLocation, goodID= good.GoodId, price= good.Price }
                            );
                    }
                    
                    if( good.OperationType == 2)
                    {
                        returnGoodsIds.Add(good.GoodId);
                    }
                }
                await _goodsServices.SellItem(cartToBePayed.clientId, SellGoods);
                if(returnGoodsIds.Count > 0) {
                    await _goodsServices.ReturnItems(cartToBePayed.clientId, new ReturnGoods { clerkId = cartToBePayed.clerktId, returnLocation = cartToBePayed.storeLocation, goodID = returnGoodsIds });
                }
                
            }

            await _DBContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<StoreCartsEntityModelWithDetails>(cartToBePayed);
        }

        public async Task<int> RemoveCart(int cartId)
        {
            var cartItem = _DBContext.StoreCartsEntity
                .Include(t => t.StoreCartsEntityDetails)
                .Where(t => t.Status == 1)
                .FirstOrDefault(t => t.Id == cartId) ?? throw new ArgumentException($"{nameof(cartId)} not found");

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
            var cartItem = _DBContext.StoreCartsEntityDetails.FirstOrDefault( t => t.Id == cartDetailsId) ??throw new ArgumentException("Cart item not found");
            _DBContext.Remove(cartItem);
            var patchJson = new JsonPatchDocument();
            patchJson.Replace("Status",(int) GoodsStatus.AVAILABLE);
            await _goodsServices.PatchGood(cartItem.GoodId, patchJson);

            var updateCart = _DBContext.StoreCartsEntity.FirstOrDefault(t => t.Id == cartItem.CartId);
            var patchCart =new JsonPatchDocument();
            
            patchCart.Replace("Total",Decimal.Subtract(updateCart.Total, cartItem.Price));
            patchCart.Replace("Remaining", Decimal.Subtract(updateCart.Remaining, cartItem.Price));
            patchCart.ApplyTo(updateCart);

            return await _DBContext.SaveChangesAsync(CancellationToken.None);
            
        }


        public async Task< (IEnumerable<StoreCartsEntityModelWithDetails>, PaginationMetadata)> GetCarts(int pageNumber, int pageSize)
        {
            var collection = _DBContext.StoreCartsEntity as IQueryable<StoreCartsEntity>;
            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionReturn = await collection.OrderBy(c => c.clientId)
                   .Skip(pageSize * (pageNumber - 1))
                   .Take(pageSize)
                   .ToListAsync();
            var returnCollection = _mapper.Map<IEnumerable<StoreCartsEntityModelWithDetails>>(collectionReturn);

            return (returnCollection, paginationMetadata);
        }

        public async Task<IEnumerable<StoreCartsEntityModelWithDetails>> GetCartsByAccountID(int accountId)
        {
            return _mapper.Map<IEnumerable<StoreCartsEntityModelWithDetails>>(await _DBContext.StoreCartsEntity.Include(t => t.StoreCartsEntityDetails).Where(t => t.clientId == accountId).ToListAsync(CancellationToken.None) );
        }
    }
}

