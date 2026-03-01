using TasksAPI.Entities;
using TasksAPI.Models;
using TasksAPI.Services;

namespace TasksAPI.Interfaces;

public interface IGoodsTypesServices
{
    Task< (IEnumerable<GoodsTypesModel>, PaginationMetadata)>GetGoodTypes(int pageNumber, int pageSize);
    Task< (IEnumerable<GoodsTypesModel>, PaginationMetadata)>GetGoodTypesQuery(QueryFilters queryFilters);
    Task<v_GoodsTypes> GetGoodTypeById(int goodId);
    Task<v_GoodsTypes> CreateGoodType(CreateGoodsTypesModel goodtypeModel);
    Task<GoodsTypesModel> UpdateGoodType(int goodId, UpdateGoodsTypesModel goodType);
    Task<bool> DeleteGoodTypes(int goodId);
    
}