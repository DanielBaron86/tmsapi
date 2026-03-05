using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasksAPI.Interfaces;
using TasksAPI.Models;

namespace TasksAPI.Controllers
{
    [Route("api/v{version:apiVersion}/goods_base")]
    [ApiController]
    [Authorize(Roles = "clerk")]


    public class GoodBaseController : ControllerBase
    {

        const int MaxPagesSize = 1000;
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
                if (pageSize > MaxPagesSize) pageSize = 1000;
                var (baseItems, paginationMetadata) = await _goodsBaseService.GetBaseGoodTypes(pageNumber, pageSize);

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
                Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");

                return Ok(baseItems);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
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
            if (!ModelState.IsValid) { throw new Exception("Validation Error"); }
            try
            {

                return Ok(await _goodsBaseService.CreateBaseType(goodBaseModel));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update Existing Good Base Good
        /// </summary>
        /// <param name="baseGoodId"></param>
        /// <param name="goodBaseModel"></param>
        /// <returns></returns>
        [HttpPut("{baseGoodId}")]
        public async Task<ActionResult<GoodBaseTypeModel>> UpdateBaseGoods(int baseGoodId, UpdateGoodBaseTypeModel goodBaseModel)
        {
            try
            {
                return Ok(await _goodsBaseService.UpdateBaseType(baseGoodId, goodBaseModel));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
