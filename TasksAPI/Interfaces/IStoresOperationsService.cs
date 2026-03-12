using Microsoft.AspNetCore.Mvc;
using TasksAPI.Entities;
using TasksAPI.Models;
using TasksAPI.Services;

namespace TasksAPI.Interfaces
{
    public interface IStoresOperationsService
    {
        Task<CashRegisterEntityModel> CreateCashRegister(CreateCashRegisterEntity cashRegisterEntity);
        Task<CashRegisterEntityModel> UpdateRegister(int id, CreateCashRegisterEntity updateModel);
        Task<(IEnumerable<CashRegisterEntityModel>, PaginationMetadata)> GetCashRegisters(int pageNumber, int pageSize);
        Task<CashRegisterEntityModel> GetCashRegistersById(int registerId);
        Task<(IEnumerable<CashRegisterEntityModel>, PaginationMetadata)> GetCashRegisterWithConditions(QueryFilters queryFilters);


        Task<(IEnumerable<CashRegisterEntitySessionsModel>, PaginationMetadata)> GetSession(int pageNumber, int pageSize);
        Task<(IEnumerable<CashRegisterEntitySessionsModel>, PaginationMetadata)> GetSessionrWithConditions(QueryFilters queryFilters);
        Task<CashRegisterEntitySessionsModel> OpenNewSession(CreateCashRegisterSessionsEntityModel args);
        Task<CashRegisterEntitySessionsModel> CloseSession(int sessionId);

        Task<StoreCartsEntityModel> CreateNewCart(CreateNewCart CreateNewCart);
        Task<StoreCartsEntityDetailsModel> AddDetailsToCart(int cartId, CreateRegisterOperationsModel operationModel);
        Task<StoreCartsEntityDetailsModel> AddReturnToCart(int cartId, CreateRegisterOperationsModel operationModel);

        Task<(IEnumerable<StoreCartsEntityModelWithDetails>, PaginationMetadata)> GetCarts(int pageNumber, int pageSize);
        Task<(IEnumerable<StoreCartsEntityModelWithDetails>, PaginationMetadata)> GetCartsWithConditions(QueryFilters queryFilters);
        Task<StoreCartsEntityModelWithDetails> GetCartByID(int cartId);
        Task<IEnumerable<StoreCartsEntityModelWithDetails>> GetCartsByAccountID(int accountId);

        Task<StoreCartsEntityModelWithDetails> PayForCartByID(int cartId, Decimal money);

        Task<StoreCartsEntityDetailsModel> GetCartDetilsByID(int cartDetailsId);

        Task<int> RemoveCartDetail(int cartDetailsId);
        Task<int> RemoveCart(int cartId);
    }
}
