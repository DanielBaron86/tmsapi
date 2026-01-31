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
    
    public async Task < (IEnumerable<v_GoodsTypesModel>, PaginationMetadata)>GetGoodTypes(int pageNumber, int pageSize)
    {
            
        var collection = _dbContext.v_GoodsTypes as IQueryable<v_GoodsTypes>;
            
        var totalItemCount = await collection.CountAsync();
        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var collectionReturn = await collection
            .OrderBy(c => c.Id)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
            
        var returnCollection = _mapper.Map<IEnumerable<v_GoodsTypesModel>>(collectionReturn);

        return (returnCollection,paginationMetadata);
    }
    
    public async Task<v_GoodsTypesModel> GetGoodTypeById(int goodId)
    {
        var good = await _dbContext.v_GoodsTypes.FirstOrDefaultAsync(i => i.Id == goodId);
        return _mapper.Map<v_GoodsTypesModel>(good);
    }
    public async Task<GoodsTypesModel> CreateGoodType(CreateGoodsTypesModel goodtypeModel)
    {
        var goodToBeCreated = _mapper.Map<GoodsTypes>(goodtypeModel);
        _dbContext.Add(goodToBeCreated);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        return _mapper.Map<GoodsTypesModel>(goodToBeCreated);
    }
    public async Task<GoodsTypesModel> UpdateGoodType(int goodId, UpdateGoodsTypesModel goodType)
    {
        var itemToBeUpdated = await _dbContext.GoodsTypes.FirstOrDefaultAsync(g => g.Id == goodId) ?? throw new ArgumentException("Item not found");
            
        _mapper.Map(goodType, itemToBeUpdated);

        await _dbContext.SaveChangesAsync(CancellationToken.None);
        var updatedItem = await _dbContext.GoodsTypes.FirstOrDefaultAsync(g => g.Id == itemToBeUpdated.Id);
        return _mapper.Map<GoodsTypesModel>(updatedItem);
            
    }
    public async Task<bool> DeleteGoodTypess(int goodId)
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