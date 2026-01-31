using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasksAPI.Interfaces;
using TasksAPI.Models;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;

namespace TasksAPI.Controllers;

[Microsoft.AspNetCore.Mvc.Route("api/v{version:apiVersion}/goods")]
[ApiController]
[Authorize(Roles = "clerk")]

public class GoodTypesController : ControllerBase
{
    const int MaxCitiesPagesSize = 20;
    private readonly IGoodsServices _goodsService;
    
    public GoodTypesController(IConfiguration configuration, IGoodsServices goodsServices)
    {
        _goodsService = goodsServices ?? throw new ArgumentNullException(nameof(goodsServices));
    }
    
    /// <summary>
    /// Returs a list of good types
    /// </summary>
    /// <returns></returns>
    [HttpGet("goodtypes")]
    public async Task<ActionResult<IEnumerable<v_GoodsTypesModel>>> GelAllGoodTypes(int pageNumber = 1, int pageSize = 10)
    {   
        try
        {
            if (pageSize > MaxCitiesPagesSize) pageSize = MaxCitiesPagesSize;
            var (goodTypes, paginationMetadata) = await _goodsService.GetGoodTypes(pageNumber, pageSize);

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
    [HttpGet("goodtypes/{goodId}")]
    public async Task<ActionResult<v_GoodsTypesModel>> GetGoodTypesById(int goodId)
    {
        var good = await _goodsService.GetGoodTypeById(goodId);
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
    [HttpPost("goodtypes")]
    public async Task<ActionResult<GoodsModels>> CreateGoodTypes(CreateGoodsTypesModel goodsModels)
    {
        if (goodsModels == null) { return NotFound(); }

        if (!ModelState.IsValid) { return BadRequest(); }

        try
        {
            return Ok(await _goodsService.CreateGoodType(goodsModels));
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
    [HttpPut("goodtypes/{goodId}")]
    public async Task<ActionResult<GoodsModels>> UpdateGoodTypes(int goodId, UpdateGoodsTypesModel goodsModels)
    {
        try
        {
            return Ok(await _goodsService.UpdateGoodType(goodId, goodsModels));
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
    [HttpDelete("goodtypes/{goodId}")]
    [Authorize(Policy = "Supervisor")]
    public async Task<ActionResult<bool>> DeleteGoodTypess(int goodId)
    {
        try
        {
            return Ok(await _goodsService.DeleteGoodTypess(goodId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}