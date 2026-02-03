using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;

namespace TasksAPI.Controllers;

[Route("api/v{version:apiVersion}/goods_type")]
[ApiController]
[Authorize(Roles = "clerk")]

public class GoodTypesController : ControllerBase
{
    const int MaxCitiesPagesSize = 20;
    private readonly IGoodsTypesServices _goodsInstancesService;
    
    public GoodTypesController(IConfiguration configuration, IGoodsTypesServices goodsInstancesServices)
    {
        _goodsInstancesService = goodsInstancesServices ?? throw new ArgumentNullException(nameof(goodsInstancesServices));
    }
    
    /// <summary>
    /// Returns a list of good types
    /// </summary>
    /// <returns></returns>
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<GoodsTypesModel>>> GelAllGoodTypes(int pageNumber = 1, int pageSize = 10)
    {   
        try
        {
            if (pageSize > MaxCitiesPagesSize) pageSize = MaxCitiesPagesSize;
            var (goodTypes, paginationMetadata) = await _goodsInstancesService.GetGoodTypes(pageNumber, pageSize);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            return Ok(goodTypes);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
            
    }
    
    
    /// <summary>
    /// Get Good Type by Id
    /// </summary>
    /// <param name="goodId"></param>
    /// <returns></returns>
    [HttpGet("{goodId}")]
    public async Task<ActionResult<v_GoodsTypes>> GetGoodTypesById(int goodId)
    {
        var good = await _goodsInstancesService.GetGoodTypeById(goodId);
        if (good == null)
        {
            return NotFound();
        }
        return Ok(good);


    }
    
    
    /// <summary>
    /// Create a new Good Type
    /// </summary>
    /// <param name="goodsModels"></param>
    /// <returns></returns>
    [HttpPost()]
    public async Task<ActionResult<GoodsModels>> CreateGoodTypes(CreateGoodsTypesModel goodsModels)
    {
        if (goodsModels == null) { return NotFound(); }

        if (!ModelState.IsValid) { return BadRequest(); }

        try
        {
            return Ok(await _goodsInstancesService.CreateGoodType(goodsModels));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    /// <summary>
    /// Update a good type
    /// </summary>
    /// <param name="goodId"></param>
    /// <param name="goodsModels"></param>
    /// <returns></returns>
    [HttpPut("{goodId}")]
    public async Task<ActionResult<GoodsModels>> UpdateGoodTypes(int goodId, UpdateGoodsTypesModel goodsModels)
    {
        try
        {
            return Ok(await _goodsInstancesService.UpdateGoodType(goodId, goodsModels));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    
    /// <summary>
    /// Delete Good Types by ID
    /// </summary>
    /// <param name="goodId"></param>
    /// <returns></returns>
    [HttpDelete("{goodId}")]
    [Authorize(Policy = "Supervisor")]
    public async Task<ActionResult<bool>> DeleteGoodTypes(int goodId)
    {
        try
        {
            return Ok(await _goodsInstancesService.DeleteGoodTypes(goodId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

}