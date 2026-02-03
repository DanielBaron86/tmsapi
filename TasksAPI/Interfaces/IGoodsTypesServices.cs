using TasksAPI.Entities;
using TasksAPI.Models;
using TasksAPI.Services;

namespace TasksAPI.Interfaces;

public interface IGoodsTypesServices
{
    Task< (IEnumerable<v_GoodsTypes>, PaginationMetadata)>GetGoodTypes(int pageNumber, int pageSize);
    Task<v_GoodsTypes> GetGoodTypeById(int goodId);
    Task<v_GoodsTypes> CreateGoodType(CreateGoodsTypesModel goodtypeModel);
    Task<GoodsTypesModel> UpdateGoodType(int goodId, UpdateGoodsTypesModel goodType);
    Task<bool> DeleteGoodTypes(int goodId);
    Task<IEnumerable<GoodsTypesModel>>GetGoodTypesEntity();
    
}