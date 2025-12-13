using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TasksAPI.Interfaces;
using TasksAPI.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TasksAPI.Controllers
{
    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]

    public class UsersControllers : ControllerBase
    {

        private readonly IUserService _userService;



        public UsersControllers(IUserService userService, IConfiguration configuration)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));

        }


        /// <summary>
        /// Register a new User. User type  3- Clerk , 4 - Supervisor
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _userService.Register(resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _userService.Login(resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }


        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "clerk")]
        public async Task<ActionResult<IEnumerable<UserResource>>> GetAllUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        

        /// <summary>
        /// Return user by ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet("{userID}", Name = "GetUserByID")]
        [Authorize(Roles = "clerk")]
        public async Task<ActionResult<UserResource>> GetUserByID(int userID)
        {
            var user = await _userService.GetUserById(userID);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);

        }


        /// <summary>
        /// Update user details
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{userID}")]
        [Authorize(Policy = "Supervisor")]
        public async Task<ActionResult<UserResource>> UpdateUser(int userID, UserResourceForUpdate user, CancellationToken cancellationToken)
        {
            var result = await _userService.UpdateUser(userID, user, cancellationToken);
            if (result == null)
            {
                return BadRequest();
            }
            return result;
        }

        
     /// <summary>
     /// Patch User Details
     /// </summary>
     /// <param name="userID"></param>
     /// <param name="patchUser"></param>
     /// <param name="cancellationToken"></param>
     /// <returns></returns>
        [HttpPatch("{userID}")]
        [Authorize(Policy = "Supervisor")]
        public async Task<ActionResult<UserResource>> PatchUser(int userID, JsonPatchDocument patchUser, CancellationToken cancellationToken)
        {

            try
            {
                return await _userService.PatchUser(userID, patchUser, cancellationToken);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete User and any entities related to the user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpDelete("{userID}")]
        [Authorize(Policy = "Supervisor")]
        public async Task<ActionResult> DeleteUser(int userID)
        {
            try
            {
                return Ok(await _userService.DeleteUser(userID, CancellationToken.None));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }            
        }
    }
}
