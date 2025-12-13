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


        private readonly IGoodsServices _goodsService;

        public GoodsController(IConfiguration configuration, IGoodsServices goodsServices)
        {
            _goodsService = goodsServices ?? throw new ArgumentNullException(nameof(goodsServices));
        }

        /// <summary>
        /// Returns a list of all goods
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoodsModels>>> GelAllGoods()
        {
            return Ok(await _goodsService.GetGoods());
        }


        /// <summary>
        /// Returs a list of good types
        /// </summary>
        /// <returns></returns>
        [HttpGet("goodtypes")]
        public async Task<ActionResult<IEnumerable<GoodsModels>>> GelAllGoodTypes()
        {
            return Ok(await _goodsService.GetGoodTypes());
        }


        /// <summary>
        /// Returns goods by ID
        /// </summary>
        /// <param name="goodID"></param>
        /// <returns></returns>
        [HttpGet("{goodID}")]
        public async Task<ActionResult<GoodsModels>> GetGoodsByID(int goodID)
        {
            var good = await _goodsService.GetGoodById(goodID);
            if (good == null)
            {
                return NotFound();
            }
            return Ok(good);


        }


        /// <summary>
        /// Get Good Type by Id
        /// </summary>
        /// <param name="goodID"></param>
        /// <returns></returns>
        [HttpGet("goodtypes/{goodID}")]
        public async Task<ActionResult<GoodsTypesModel>> GetGoodTypesByID(int goodID)
        {
            var good = await _goodsService.GetGoodTypeById(goodID);
            if (good == null)
            {
                return NotFound();
            }
            return Ok(good);


        }

        /// <summary>
        /// Get the movement history of an item
        /// </summary>
        /// <param name="goodID"></param>
        /// <returns></returns>
        [HttpGet("history/{goodID}")]
        public async Task<ActionResult<ICollection<ItemMovementEntity>>> GetGoodHistorysByID(int goodID)
        {
            var good = await _goodsService.GetGoodHistorysByID(goodID);
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
        public async Task<ActionResult<GoodsModels>> CreateGoodTypes(CeateGoodsTypesModel goodsModels)
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
        /// <param name="goodID"></param>
        /// <param name="goodsModels"></param>
        /// <returns></returns>
        [HttpPut("{goodID}")]
        public async Task<ActionResult<GoodsModels>> UpdateGoods(int goodID, UpdateGoodsModels goodsModels)
        {
            try
            {
                return Ok(await _goodsService.UpdateGood(goodID, goodsModels));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Update a good type
        /// </summary>
        /// <param name="goodID"></param>
        /// <param name="goodsModels"></param>
        /// <returns></returns>
        [HttpPut("goodtypes/{goodID}")]
        public async Task<ActionResult<GoodsModels>> UpdateGoodTypes(int goodID, UpdateGoodsTypesModel goodsModels)
        {
            try
            {
                return Ok(await _goodsService.UpdateGoodType(goodID, goodsModels));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Patch user values
        /// </summary>
        /// <param name="goodID"></param>
        /// <param name="patchGood"></param>
        /// <returns></returns>
        [HttpPatch("{goodID}")]
        public async Task<ActionResult<GoodsModels>> PatchGoods(int goodID, JsonPatchDocument patchGood)
        {
            try
            {
                return Ok(await _goodsService.PatchGood(goodID, patchGood));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete Goods by ID
        /// </summary>
        /// <param name="goodID"></param>
        /// <returns></returns>
        [HttpDelete("{goodID}")]
        [Authorize(Policy = "Supervisor")]
        public async Task<ActionResult<bool>> DeleteGoods(int goodID)
        {
            try
            {
                return Ok(await _goodsService.DeleteGoods(goodID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Delete Good Types by ID
        /// </summary>
        /// <param name="goodID"></param>
        /// <returns></returns>
        [HttpDelete("goodtypes/{goodID}")]
        [Authorize(Policy = "Supervisor")]
        public async Task<ActionResult<bool>> DeleteGoodTypess(int goodID)
        {
            try
            {
                return Ok(await _goodsService.DeleteGoodTypess(goodID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
    }
}
