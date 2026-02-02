using TasksAPI.Models;
using TasksAPI.Services;

namespace TasksAPI.Interfaces;

public interface IGoodsBaseServices
{
    Task< (IEnumerable<GoodBaseTypeModel>, PaginationMetadata)>  GetBaseGoodTypes(int pageNumber, int pageSize);
    Task<GoodBaseTypeModel> CreateBaseType(CreateGoodBaseTypeModel goodBaseModel);
    Task<GoodBaseTypeModel> UpdateBaseType(int goodId,UpdateGoodBaseTypeModel goodBaseModel);
}