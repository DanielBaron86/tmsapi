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


    public class GoodBaseController : ControllerBase
    {

        const int MaxCitiesPagesSize = 20;
        private readonly IGoodsServices _goodsService;

        public GoodBaseController(IConfiguration configuration, IGoodsServices goodsServices)
        {
            _goodsService = goodsServices ?? throw new ArgumentNullException(nameof(goodsServices));
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
