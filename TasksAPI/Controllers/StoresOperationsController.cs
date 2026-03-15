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
        const int MaxPagesSize = 100;

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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
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

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
                Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");
                return Ok(carts);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Returns a list of all Carts with filters
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost("carts/query")]
        public async Task<ActionResult<IEnumerable<CashRegisterEntityModel>>> GetCartsByQuery(QueryFilters queryFilters)
        {

            try
            {

                var (cartsInstances, paginationMetadata) = await _storeServices.GetCartsWithConditions(queryFilters);

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
                Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");
                return Ok(cartsInstances);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
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

                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Returns a list of all Registers
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("cash_register")]
        public async Task<ActionResult<IEnumerable<CashRegisterEntityModel>>> GelAllRegistersbyView(int pageNumber = 1, int pageSize = 10)
        {

            try
            {
                if (pageSize > MaxPagesSize) pageSize = MaxPagesSize;
                var (itemInstances, paginationMetadata) = await _storeServices.GetCashRegisters(pageNumber, pageSize);

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
                Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");
                return Ok(itemInstances);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Get Register by Id
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        [HttpGet("cash_register/{registerId}")]
        public async Task<ActionResult<int>> GetRegisterById(int registerId)
        {
            try
            {
                return Ok(await _storeServices.GetCashRegistersById(registerId));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update Register
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPut("cash_register/{id}")]
        public async Task<ActionResult<IEnumerable<CashRegisterEntityModel>>> UpdateRegister(int id, CreateCashRegisterEntity updateModel)
        {

            try
            {
                var registerItem = await _storeServices.UpdateRegister(id, updateModel);
                return Ok(registerItem);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Returns a list of all Registers with filters
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost("cash_register/query")]
        public async Task<ActionResult<IEnumerable<CashRegisterEntityModel>>> GelAllRegistersByQuery(QueryFilters queryFilters)
        {

            try
            {

                var (itemInstances, paginationMetadata) = await _storeServices.GetCashRegisterWithConditions(queryFilters);

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
                Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");
                return Ok(itemInstances);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
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
                return Ok(await _storeServices.CreateCashRegister(cashRegisterEntity));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Open a new session on an existing cash register
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost("open_session")]
        public async Task<ActionResult<CashRegisterEntitySessionsModel>> OpenNewSession(CreateCashRegisterSessionsEntityModel args)
        {
            try
            {
                return Ok(await _storeServices.OpenNewSession(args));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Close existing cash register sessions
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [HttpPost("close_session/{sessionId}")]
        public async Task<ActionResult<CashRegisterEntitySessionsModel>> CloseSession(int sessionId)
        {
            try
            {
                return Ok(await _storeServices.CloseSession(sessionId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Returns a list of all register Session
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("get_sessions")]
        public async Task<ActionResult<IEnumerable<CashRegisterEntitySessionsModel>>> GelAllRegisterSessions(int pageNumber = 1, int pageSize = 10)
        {

            try
            {
                if (pageSize > MaxPagesSize) pageSize = MaxPagesSize;
                var (returnInstance, paginationMetadata) = await _storeServices.GetSession(pageNumber, pageSize);

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
                Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");
                return Ok(returnInstance);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Returns active session for user
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("get_sessions/active/{userId}")]
        public async Task<ActionResult<IEnumerable<CashRegisterEntitySessionsModel>>> GetActiveSessionForUser(int userId)
        {

            try
            {

                var activeSession = await _storeServices.GetActiveSessionForUser(userId);


                return Ok(activeSession);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Returns a list of all registers Sessions with filters
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost("get_sessions/query")]
        public async Task<ActionResult<IEnumerable<CashRegisterEntitySessionsModel>>> GelAllSessionsByQuery(QueryFilters queryFilters)
        {

            try
            {

                var (returnInstance, paginationMetadata) = await _storeServices.GetSessionrWithConditions(queryFilters);

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
                Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");
                return Ok(returnInstance);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
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
            if (CreateNewCart.ClientId <= 0) return BadRequest("Invalid client ID");
            try
            {
                return Ok(await _storeServices.CreateNewCart(CreateNewCart));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add items to an existing cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="operationModel"></param>
        /// <returns></returns>
        [HttpPost("addto_cart/{cartId}")]
        public async Task<ActionResult<StoreCartsEntityDetailsModel>> AddDetailsToCart(int cartId, CreateRegisterOperationsModel operationModel)
        {
            try
            {
                return Ok(await _storeServices.AddDetailsToCart(cartId, operationModel));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Pay for an existing cart and close on full payment
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        [HttpPost("pay_cart/{cartId}")]
        public async Task<ActionResult<StoreCartsEntityModelWithDetails>> PayForCartByID(int cartId, [FromBody] Decimal money)
        {
            try
            {
                return Ok(await _storeServices.PayForCartByID(cartId, money));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
