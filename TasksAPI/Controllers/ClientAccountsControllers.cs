using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using TasksAPI.Interfaces;
using TasksAPI.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TasksAPI.Controllers
{
    [Route("api/v{version:apiVersion}/clients")]
    [ApiVersion("1.0")]
    [ApiController]

    public class ClientAccounts : ControllerBase
    {

        private readonly IClientServices _clientService;



        public ClientAccounts(IClientServices clientService, IConfiguration configuration)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));

        }

        /// <summary>
        /// Register a new client
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] ClientResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _clientService.Register(resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Client Login
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _clientService.Login(resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }


        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        [HttpGet("{clientID}", Name = "GetClientById")]
        [Authorize(Roles = "clerk")]
        public async Task<ActionResult<UserResource>> GetUserByID(int clientID)
        {
            var user = await _clientService.GetClientById(clientID);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);

        }

       

       

        /// <summary>
        /// Update Client
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{clientID}")]
        [Authorize(Roles = "clerk")]
        public async Task<ActionResult<UserResource>> UpdateUser(int clientID, ClientResourceForUpdate user, CancellationToken cancellationToken)
        {
            var result = await _clientService.UpdateClient(clientID, user, cancellationToken);
            if (result == null)
            {
                return BadRequest();
            }
            return result;
        }

        
        /// <summary>
        /// Patch Client
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="patchUser"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("{clientID}")]
        [Authorize(Roles = "clerk")]
        public async Task<ActionResult<UserResource>> PatchUser(int clientID, JsonPatchDocument patchUser, CancellationToken cancellationToken)
        {

            try
            {
                return await _clientService.PatchClient(clientID, patchUser, cancellationToken);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete client account
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        [HttpDelete("{clientID}")]
        [Authorize(Policy = "Supervisor")]
        public async Task<ActionResult> DeleteUser(int clientID)
        {
            try
            {
                return Ok(await _clientService.DeleteClient(clientID, CancellationToken.None));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }            
        }
    }
}
