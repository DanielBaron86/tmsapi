using Microsoft.AspNetCore.Mvc;
using TasksAPI.Entities;
using TasksAPI.Models;
using TasksAPI.Services;

namespace TasksAPI.Interfaces
{
    public interface IStoresOperationsService
    {
        Task<CashRegisterEntityModel> CreateCashRegister(CreateCashRegisterEntity cashRegisterEntity);
        Task<CashRegisterEntity_SessionsModel> OpenNewSession(CreateCashRegisterSessionsEntityModel args);
        Task<CashRegisterEntity_SessionsModel> CloseSession(int sessionId);

        Task<StoreCartsEntityModel> CreateNewCart(CreateNewCart CreateNewCart);
        Task<StoreCartsEntity_DetailsModel> AddDetailsToCart(int cartId, CreateRegisterOperationsModel operationModel);
        Task<StoreCartsEntity_DetailsModel> AddReturnToCart(int cartId, CreateRegisterOperationsModel operationModel);

        Task< (IEnumerable<StoreCartsEntityModelWithDetails>, PaginationMetadata)> GetCarts(int pageNumber, int pageSize);
        Task<StoreCartsEntityModelWithDetails> GetCartByID(int cartId);
        Task<IEnumerable<StoreCartsEntityModelWithDetails>> GetCartsByAccountID(int accountId);

        Task<StoreCartsEntityModelWithDetails> PayForCartByID(int cartId,Decimal money);

        Task<StoreCartsEntity_DetailsModel> GetCartDetilsByID(int cartDetailsId);

        Task<int> RemoveCartDetail(int cartDetailsId);
        Task<int> RemoveCart(int cartId);
    }
}
