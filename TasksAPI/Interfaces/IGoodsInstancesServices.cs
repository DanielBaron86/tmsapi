using Microsoft.AspNetCore.JsonPatch;
using TasksAPI.Entities;
using TasksAPI.Models;
using TasksAPI.Services;

namespace TasksAPI.Interfaces
{
    public interface IGoodsInstancesServices
    {
        Task<(IEnumerable<GoodsModels>, PaginationMetadata)> GetGoods(int pageNumber, int pageSize);
        Task<(IEnumerable<GoodsModels>, PaginationMetadata)> GetGoodsWithConditions(QueryFilters queryFilters);


        Task<GoodsModels> GetGoodById(int goodId);


        Task<GoodsModels> CreateGood(CreateGoodsModels goodUnitModel);

        Task<GoodsModels> UpdateGood(int goodId, UpdateGoodsModels good);

        Task<GoodsModels> PatchGood(int goodId, JsonPatchDocument patchGood);
        Task<bool> DeleteGoods(int goodId);
        Task<IEnumerable<AccountsGoodsEntity>> SellItem(int clientId, ICollection<SellGoods> args);
        Task<int> CreateMovementHistory(int itemId, int fromLocation, int toLocation, int toStatus, int userId);

        Task<IEnumerable<AccountsGoodsEntity>> ReturnItems(int userId, ReturnGoods returnGoods);

        Task<ICollection<ItemMovementEntity>> GetGoodHistorysById(int goodId);

        Task<(IEnumerable<v_GoodsTypesInstances>, PaginationMetadata)> GetGoodsByView(int pageNumber, int pageSize);



    }
}
