using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TasksAPI.DataBaseContext;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;

namespace TasksAPI.Services;

public class GoodsTypesServices : IGoodsTypesServices
{
    
    private readonly DatabaseConnectContext _dbContext;
    private readonly IMapper _mapper;
        
         
    public GoodsTypesServices(DatabaseConnectContext context, IMapper mapper)
    {

        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task < (IEnumerable<GoodsTypesModel>, PaginationMetadata)>GetGoodTypes(int pageNumber, int pageSize)
    {
        var collection =  _dbContext.GoodsTypes.Include( t => t.GoodModelBaseTypeEntity)  as IQueryable<GoodsTypesEntity>;
        var totalItemCount = await collection.CountAsync();
        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var collectionReturn = await collection
            .OrderBy(c => c.Id)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
        return (_mapper.Map<IEnumerable<GoodsTypesModel>>(collectionReturn),paginationMetadata);
    }

    public async Task<(IEnumerable<GoodsTypesModel>, PaginationMetadata)> GetGoodTypesQuery(QueryFilters queryFilters)
    {
        var pageSize = queryFilters.pageSize;
        var pageNumber = queryFilters.pageNumber;
        var collection =  _dbContext.GoodsTypes
                .Include( i => i.GoodModelBaseTypeEntity)
                
            as IQueryable<GoodsTypesEntity>;
        if (queryFilters.queryFields != null)
        {
            foreach (var q in queryFilters.queryFields)
            {
                collection = ServiceUtils.CreateFilter(collection, q.keyField, q.keyValue);
            }    
        }
        var totalItemCount = await collection.CountAsync();
        var paginationMetadata = new PaginationMetadata(totalItemCount, queryFilters.pageSize, queryFilters.pageNumber);
        var collectionReturn = await collection.OrderBy(c => c.Id)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
            
        var returnCollection = _mapper.Map<IEnumerable<GoodsTypesModel>>(collectionReturn);

        return (returnCollection,paginationMetadata);
    }

    public async Task<v_GoodsTypes> GetGoodTypeById(int goodId)
    {
        var good = await _dbContext.v_GoodsTypes.FirstOrDefaultAsync(i => i.Id == goodId);
        if (good == null)
        {
            throw new ArgumentException("Item not found");
        }
        return good;
    }
    
    public async Task<v_GoodsTypes> CreateGoodType(CreateGoodsTypesModel goodtypeModel)
    {
        var goodToBeCreated = _mapper.Map<GoodsTypesEntity>(goodtypeModel);
        _dbContext.Add(goodToBeCreated);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        try
        {
            return await _dbContext.v_GoodsTypes.Where(item => item.Id == goodToBeCreated.Id).FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            throw new ArgumentException("Unable to create good type",e);
        }
    }
    public async Task<GoodsTypesModel> UpdateGoodType(int goodId, UpdateGoodsTypesModel goodType)
    {
        var itemToBeUpdated = await _dbContext.GoodsTypes.FirstOrDefaultAsync(g => g.Id == goodId) ?? throw new ArgumentException("Item not found");
            
        _mapper.Map(goodType, itemToBeUpdated);

        await _dbContext.SaveChangesAsync(CancellationToken.None);
        var updatedItem = await _dbContext.GoodsTypes.FirstOrDefaultAsync(g => g.Id == itemToBeUpdated.Id);
        return _mapper.Map<GoodsTypesModel>(updatedItem);
            
    }
    public async Task<bool> DeleteGoodTypes(int goodId)
    {
        var item = await _dbContext.GoodsTypes.FirstOrDefaultAsync(i => i.Id == goodId) ?? throw new ArgumentException("Item not found");
        var countItems = await _dbContext.GoodsTypesInstances.Where( t => t.GoodModelId == goodId).CountAsync();
            

        if (countItems <= 0)
        {
            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
            return true;

        }
        else
        {
            throw new ArgumentException("Unable to delete item type. Already has items instances associated");
        }
            
    }
}