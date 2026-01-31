using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TasksAPI.DataBaseContext;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;

namespace TasksAPI.Services;

public class GoodsBaseServices : IGoodsBaseServices
{
    
    private readonly DatabaseConnectContext _dbContext;
    private readonly IMapper _mapper;
        
         
    public GoodsBaseServices(DatabaseConnectContext context, IMapper mapper)
    {

        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task< (IEnumerable<GoodBaseTypeModel>, PaginationMetadata)>  GetBaseGoodTypes(int pageNumber, int pageSize)
    {
            
            
            
        var collection = _dbContext.GoodModelBaseType.AsEnumerable() as IQueryable<GoodModelBaseType>;
            
        var totalItemCount = await collection.CountAsync();
        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var collectionReturn = await collection.OrderBy(c => c.Id)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
            
        var returnCollection = _mapper.Map<IEnumerable<GoodBaseTypeModel>>(collectionReturn);

        return (returnCollection,paginationMetadata);
    }
    
    public  async Task<GoodBaseTypeModel> CreateBaseType(CreateGoodBaseTypeModel goodBaseModel)
    {
        var goodBaseToBeCreated = _mapper.Map<GoodModelBaseType>(goodBaseModel);
        _dbContext.Add(goodBaseToBeCreated);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        return _mapper.Map<GoodBaseTypeModel>(goodBaseToBeCreated);
    }
    public async Task<GoodBaseTypeModel> UpdateBaseType(int goodId, CreateGoodBaseTypeModel goodBaseModel)
    {
        var itemToBeUpdated = await _dbContext.GoodModelBaseType.FirstOrDefaultAsync(g => g.Id == goodId) ?? throw new ArgumentException("Item not found");
            
        _mapper.Map(goodBaseModel, itemToBeUpdated);

        await _dbContext.SaveChangesAsync(CancellationToken.None);
        var updatedItem = await _dbContext.GoodModelBaseType.FirstOrDefaultAsync(g => g.Id == itemToBeUpdated.Id);
        return _mapper.Map<GoodBaseTypeModel>(updatedItem);
    }
}