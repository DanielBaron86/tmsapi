using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TasksAPI.Configuration;
using TasksAPI.Interfaces;
using TasksAPI.Models;



namespace TasksAPI.Controllers
{

    [Route("api/v{version:apiVersion}/stores")]
    [ApiController]
    [Authorize(Roles = "clerk")]
    [SwaggerControllerOrder(3)]
    public class StoresOperationsController : ControllerBase
    {
        private readonly IStoresOperationsService _storeServices;
        const int maxCitiesPagesSize = 20;
        public StoresOperationsController(IStoresOperationsService storeServices)
        {
            _storeServices = storeServices ?? throw new ArgumentNullException(nameof(storeServices));
        }

        /// <summary>
        /// Return the contents of a cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        [HttpGet("{cartId}")]
        public async Task<ActionResult<StoreCartsEntityModelWithDetails>> GetCartByID(int cartId)
        {
            try
            {
                return Ok(await _storeServices.GetCartByID(cartId));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get all existing carts
        /// </summary>
        /// <returns></returns>
        [HttpGet("carts")]
        public async Task<ActionResult<IEnumerable<StoreCartsEntityModelWithDetails>>> GetCarts(
                int pageNumber = 1,
                int pageSize = 10)
        {
            
            try
            {
                if (pageSize > maxCitiesPagesSize) pageSize = maxCitiesPagesSize;
                var (carts, paginationMetadata) = await _storeServices.GetCarts(pageNumber, pageSize);

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
                return Ok(carts);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Get all carts from a client
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet("carts/{accountId}")]
        public async Task<ActionResult<IEnumerable<StoreCartsEntityModelWithDetails>>> GetCartByAccountID(int accountId)
        {
            try
            {
                return Ok(await _storeServices.GetCartsByAccountID(accountId));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Remove an item from the cart
        /// </summary>
        /// <param name="cartDetailsId"></param>
        /// <returns></returns>
        [HttpDelete("details/{cartDetailsId}")]
        public async Task<ActionResult<int>> RemoveCartDetail(int cartDetailsId)
        {
            try
            {
                return Ok(await _storeServices.RemoveCartDetail(cartDetailsId));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Close cart and remove cart items
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        [HttpDelete("cart/{cartId}")]
        public async Task<ActionResult<int>> CloseCart(int cartId)
        {
            try
            {
                return Ok(await _storeServices.RemoveCart(cartId));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create a new cash register and assign it to a location
        /// </summary>
        /// <param name="cashRegisterEntity"></param>
        /// <returns></returns>
        [HttpPost("create_register")]
        public async Task<ActionResult<CashRegisterEntityModel>> CreateRegister(CreateCashRegisterEntity cashRegisterEntity)
        {
            try
            {
               return Ok( await _storeServices.CreateCashRegister(cashRegisterEntity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Open a new session on an existing cash register
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost("open_session")]
        public async Task<ActionResult<CashRegisterEntity_SessionsModel>> OpenNewSession(CreateCashRegisterSessionsEntityModel args)
        {
            try
            {
                return Ok(await _storeServices.OpenNewSession(args));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Close existing cash register sessions
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [HttpPost("close_session/{sessionId}")]
        public async Task<ActionResult<CashRegisterEntity_SessionsModel>> CloseSession(int sessionId)
        {
            try
            {
                return Ok(await _storeServices.CloseSession(sessionId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Create a new cart
        /// </summary>
        /// <param name="CreateNewCart"></param>
        /// <returns></returns>
        [HttpPost("create_cart")]
        public async Task<ActionResult<bool>> CreateNewCart(CreateNewCart CreateNewCart)
        {
            if (CreateNewCart.clientId <= 0) return BadRequest("Invalid client ID");
            try
            {
                return Ok(await _storeServices.CreateNewCart(CreateNewCart));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add items to an existing cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="operationModel"></param>
        /// <returns></returns>
        [HttpPost("addto_cart/{cartId}")]
        public async Task<ActionResult<StoreCartsEntity_DetailsModel>> AddDetailsToCart(int cartId, CreateRegisterOperationsModel operationModel)
        {
            try
            {
                return Ok(await _storeServices.AddDetailsToCart(cartId, operationModel));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); 
            }
        }

        /// <summary>
        /// Pay for an existing cart and close on full payment
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        [HttpPost("pay_cart/{cartId}")]
        public async Task<ActionResult<StoreCartsEntityModelWithDetails>> PayForCartByID(int cartId,[FromBody] Decimal money)
        {
            try
            {
                return Ok(await _storeServices.PayForCartByID(cartId, money));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
