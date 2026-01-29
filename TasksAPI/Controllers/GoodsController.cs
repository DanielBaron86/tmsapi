using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;

namespace TasksAPI.Controllers
{
    [Route("api/v{version:apiVersion}/goods")]
    [ApiController]
    [Authorize(Roles = "clerk")]


    public class GoodsController : ControllerBase
    {

        const int MaxCitiesPagesSize = 20;
        private readonly IGoodsServices _goodsService;

        public GoodsController(IConfiguration configuration, IGoodsServices goodsServices)
        {
            _goodsService = goodsServices ?? throw new ArgumentNullException(nameof(goodsServices));
        }

        /// <summary>
        /// Returns a list of all goods
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
                var (itemInstances, paginationMetadata) = await _goodsService.GetGoods(pageNumber, pageSize);

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
                return Ok(itemInstances);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
        
        /// <summary>
        /// Returs a list of base good types
        /// </summary>
        /// <returns></returns>
        [HttpGet("base_goods")]
        public async Task<ActionResult<IEnumerable<GoodBaseTypeModel>>> GetBaseGoods(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageSize > 1000) pageSize = 1000;
                var (baseItems, paginationMetadata) = await _goodsService.GetBaseGoodTypes(pageNumber, pageSize);

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
                return Ok(baseItems);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Returs a list of good types
        /// </summary>
        /// <returns></returns>
        [HttpGet("goodtypes")]
        public async Task<ActionResult<IEnumerable<GoodsModels>>> GelAllGoodTypes(int pageNumber = 1, int pageSize = 10)
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
        /// Returns goods by ID
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        [HttpGet("{goodId}")]
        public async Task<ActionResult<GoodsModels>> GetGoodsById(int goodId)
        {
            var good = await _goodsService.GetGoodById(goodId);
            if (good == null)
            {
                return NotFound();
            }
            return Ok(good);


        }


        /// <summary>
        /// Get Good Type by Id
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        [HttpGet("goodtypes/{goodId}")]
        public async Task<ActionResult<GoodsTypesModel>> GetGoodTypesById(int goodId)
        {
            var good = await _goodsService.GetGoodTypeById(goodId);
            if (good == null)
            {
                return NotFound();
            }
            return Ok(good);


        }

        /// <summary>
        /// Get the movement history of an item
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        [HttpGet("history/{goodId}")]
        public async Task<ActionResult<ICollection<ItemMovementEntity>>> GetGoodHistorysById(int goodId)
        {
            var good = await _goodsService.GetGoodHistorysById(goodId);
            if (good == null)
            {
                return NotFound();
            }
            return Ok(good);


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
                return Ok(await _goodsService.CreateGood(goodsModels));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        /// Update Existing Good
        /// </summary>
        /// <param name="goodId"></param>
        /// <param name="goodsModels"></param>
        /// <returns></returns>
        [HttpPut("{goodId}")]
        public async Task<ActionResult<GoodsModels>> UpdateGoods(int goodId, UpdateGoodsModels goodsModels)
        {
            try
            {
                return Ok(await _goodsService.UpdateGood(goodId, goodsModels));
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
        /// Patch user values
        /// </summary>
        /// <param name="goodId"></param>
        /// <param name="patchGood"></param>
        /// <returns></returns>
        [HttpPatch("{goodId}")]
        public async Task<ActionResult<GoodsModels>> PatchGoods(int goodId, JsonPatchDocument patchGood)
        {
            try
            {
                return Ok(await _goodsService.PatchGood(goodId, patchGood));
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
                return Ok(await _goodsService.DeleteGoods(goodId));
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
        
        /// <summary>
        /// Create Base Model
        /// </summary>
        /// <param name="goodBaseModel"></param>
        /// <returns></returns>
        [HttpPost("base_goods")]
        public async Task<ActionResult<GoodBaseTypeModel>> CreateBaseModel(CreateGoodBaseTypeModel goodBaseModel)
        {
            if (goodBaseModel == null) { return NotFound(); }

            if (!ModelState.IsValid) { return BadRequest(); }

            try
            {
                return Ok(await _goodsService.CreateBaseType(goodBaseModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update Existing Good Base Good
        /// </summary>
        /// <param name="baseGoodId"></param>
        /// <param name="goodBaseModel"></param>
        /// <returns></returns>
        [HttpPut("base_goods/{baseGoodId}")]
        public async Task<ActionResult<GoodsModels>> UpdateBaseGoods(int baseGoodId, CreateGoodBaseTypeModel goodBaseModel)
        {
            try
            {
                return Ok(await _goodsService.UpdateBaseType(baseGoodId, goodBaseModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
