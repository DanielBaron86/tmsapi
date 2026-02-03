using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;

namespace TasksAPI.Controllers;

[Route("api/v{version:apiVersion}/goods_instance")]
[ApiController]
[Authorize(Roles = "clerk")]

public class GoodInstancesController : ControllerBase
{
    const int MaxCitiesPagesSize = 20;
    private readonly IGoodsInstancesServices _goodsInstancesService;
    
    public GoodInstancesController(IConfiguration configuration, IGoodsInstancesServices goodsInstancesServices)
    {
        _goodsInstancesService = goodsInstancesServices ?? throw new ArgumentNullException(nameof(goodsInstancesServices));
    }
    
    /// <summary>
        /// Create a new Goods instance
        /// </summary>
        /// <param name="goodsModels"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GoodsModels>> CreateGoods(CreateGoodsModels goodsModels)
        {
            if (goodsModels == null) { return NotFound(); }

            if (!ModelState.IsValid) { return BadRequest(); }

            try
            {
                return Ok(await _goodsInstancesService.CreateGood(goodsModels));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Update Existing Good Instance
        /// </summary>
        /// <param name="goodId"></param>
        /// <param name="goodsModels"></param>
        /// <returns></returns>
        [HttpPut("{goodId}")]
        public async Task<ActionResult<GoodsModels>> UpdateGoods(int goodId, UpdateGoodsModels goodsModels)
        {
            try
            {
                return Ok(await _goodsInstancesService.UpdateGood(goodId, goodsModels));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        /// <summary>
        /// Returns goods instance by ID
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        [HttpGet("{goodId}")]
        public async Task<ActionResult<GoodsModels>> GetGoodsById(int goodId)
        {
            var good = await _goodsInstancesService.GetGoodById(goodId);
            if (good == null)
            {
                return NotFound();
            }
            return Ok(good);


        }
        
        /// <summary>
        /// Returns a list of all goods instances
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoodsModels>>> GelAllGoods(int pageNumber = 1, int pageSize = 10)
        {
            
            try
            {
                if (pageSize > MaxCitiesPagesSize) pageSize = MaxCitiesPagesSize;
                var (itemInstances, paginationMetadata) = await _goodsInstancesService.GetGoods(pageNumber, pageSize);

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
                return Ok(itemInstances);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
        
        /// <summary>
        /// Get the movement history of an item
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        [HttpGet("history/{goodId}")]
        public async Task<ActionResult<ICollection<ItemMovementEntity>>> GetGoodHistorysById(int goodId)
        {
            var good = await _goodsInstancesService.GetGoodHistorysById(goodId);
            if (good == null)
            {
                return NotFound();
            }
            return Ok(good);


        }
        
        /// <summary>
        /// Patch Good Instance values
        /// </summary>
        /// <param name="goodId"></param>
        /// <param name="patchGood"></param>
        /// <returns></returns>
        [HttpPatch("{goodId}")]
        public async Task<ActionResult<GoodsModels>> PatchGoods(int goodId, JsonPatchDocument patchGood)
        {
            try
            {
                return Ok(await _goodsInstancesService.PatchGood(goodId, patchGood));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Delete Goods by ID
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        [HttpDelete("{goodId}")]
        [Authorize(Policy = "Supervisor")]
        public async Task<ActionResult<bool>> DeleteGoods(int goodId)
        {
            try
            {
                return Ok(await _goodsInstancesService.DeleteGoods(goodId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        /// <summary>
        /// Returns a list of all goods instances from a View
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("view")]
        public async Task<ActionResult<IEnumerable<v_GoodsTypesInstances>>> GelAllGoodsbyView(int pageNumber = 1, int pageSize = 10)
        {
            
            try
            {
                if (pageSize > MaxCitiesPagesSize) pageSize = MaxCitiesPagesSize;
                var (itemInstances, paginationMetadata) = await _goodsInstancesService.GetGoodsByView(pageNumber, pageSize);

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
                return Ok(itemInstances);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
    
}