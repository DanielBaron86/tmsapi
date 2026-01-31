using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;

namespace TasksAPI.Controllers
{
    [Route("api/v{version:apiVersion}/goods_base")]
    [ApiController]
    [Authorize(Roles = "clerk")]


    public class GoodBaseController : ControllerBase
    {

        const int MaxCitiesPagesSize = 1000;
        private readonly IGoodsBaseServices _goodsBaseService;

        public GoodBaseController(IConfiguration configuration, IGoodsBaseServices goodsInstancesServices)
        {
            _goodsBaseService = goodsInstancesServices ?? throw new ArgumentNullException(nameof(goodsInstancesServices));
        }
        
        
        
        
        /// <summary>
        /// Returs a list of base good types
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<GoodBaseTypeModel>>> GetBaseGoods(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageSize > MaxCitiesPagesSize) pageSize = 1000;
                var (baseItems, paginationMetadata) = await _goodsBaseService.GetBaseGoodTypes(pageNumber, pageSize);

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
        [HttpPost()]
        public async Task<ActionResult<GoodBaseTypeModel>> CreateBaseModel(CreateGoodBaseTypeModel goodBaseModel)
        {
            if (goodBaseModel == null) { return NotFound(); }

            if (!ModelState.IsValid) { return BadRequest(); }

            try
            {
                return Ok(await _goodsBaseService.CreateBaseType(goodBaseModel));
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
        [HttpPut("{baseGoodId}")]
        public async Task<ActionResult<GoodsModels>> UpdateBaseGoods(int baseGoodId, CreateGoodBaseTypeModel goodBaseModel)
        {
            try
            {
                return Ok(await _goodsBaseService.UpdateBaseType(baseGoodId, goodBaseModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
