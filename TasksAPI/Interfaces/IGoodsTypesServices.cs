using TasksAPI.Models;
using TasksAPI.Services;

namespace TasksAPI.Interfaces;

public interface IGoodsTypesServices
{
    Task< (IEnumerable<v_GoodsTypesModel>, PaginationMetadata)>GetGoodTypes(int pageNumber, int pageSize);
    Task<v_GoodsTypesModel> GetGoodTypeById(int goodId);
    Task<GoodsTypesModel> CreateGoodType(CreateGoodsTypesModel goodtypeModel);
    Task<GoodsTypesModel> UpdateGoodType(int goodId, UpdateGoodsTypesModel goodType);
    Task<bool> DeleteGoodTypes(int goodId);
}