using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TasksAPI.Entities;
using TasksAPI.Models;

namespace TasksAPI.Interfaces
{
    public interface IGoodsServices
    {
        Task<IEnumerable<GoodsModels>> GetGoods();
        Task<IEnumerable<GoodsTypesModel>> GetGoodTypes();
        Task<GoodsModels> GetGoodById(int GoodID);
        Task<GoodsTypesModel> GetGoodTypeById(int GoodID);

        Task<GoodsModels> CreateGood(CreateGoodsModels GoodUnitModel);
        Task<GoodsTypesModel> CreateGoodType(CeateGoodsTypesModel GoodtypeModel);
        Task<GoodsModels> UpdateGood(int goodId, UpdateGoodsModels Good);
        Task<GoodsTypesModel> UpdateGoodType(int goodId, UpdateGoodsTypesModel GoodType);
        Task<GoodsModels> PatchGood(int goodID, JsonPatchDocument patchGood);
        Task<bool> DeleteGoods(int GoodID);
        Task<bool> DeleteGoodTypess(int GoodID);
        Task<IEnumerable<AccountsGoodsEntity>> SellItem(int clientId, ICollection<SellGoods> args);
        Task<int> CreateMovementHistory(int itemID, int FromLocation, int ToLocation, int ToStatus, int userID);

        Task<IEnumerable<AccountsGoodsEntity>> ReturnItems(int userID, ReturnGoods returnGoods);

        Task<ICollection<ItemMovementEntity>> GetGoodHistorysByID(int goodID);


    }
}
