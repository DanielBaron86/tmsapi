using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TasksAPI.Interfaces;
using TasksAPI.Models;

namespace TasksAPI.Controllers
{
    [Route("api/v{version:apiVersion}/locations")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "clerk")]


    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;
        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        /// <summary>
        /// Get a list of all locations
        /// </summary>
        ///<response code="200">return list of locations</response>
        [HttpGet]

        public async Task<ActionResult<IEnumerable<LocationTypesModel>>> GetAll()
        {


            return Ok(await _locationService.GetLocations());

        }


        /// <summary>
        /// Get a list of all location types
        /// </summary>
        /// <returns></returns>
        [HttpGet("locationtype")]

        public async Task<ActionResult<IEnumerable<LocationTypesModel>>> GetAllLocationType()
        {
            return Ok(await _locationService.GetLocationTypess());
        }

        /// <summary>
        /// Get Locations by ID
        /// </summary>
        /// <param name="locationid"></param>
        /// <response code="200">returns list single location</response>
        [HttpGet("{locationid}")]
        public async Task<ActionResult<LocationUnitModel>> GetLocationById(int locationid)
        {
            var location = await _locationService.GetLocationById(locationid);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }


        /// <summary>
        /// Get Location Type by Id
        /// </summary>
        /// <param name="locationid"></param>
        /// <returns></returns>
        [HttpGet("locationtype/{locationid}")]
        public async Task<ActionResult<LocationUnitModel>> GetLocationTypeById(int locationid)
        {
            var location = await _locationService.GetLocationTypeById(locationid);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }


        /// <summary>
        /// Create A new location unit
        /// </summary>
        /// <param name="locationUnitModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<LocationUnitModel>> CreateLocation(LocationUnitForCreate locationUnitModel)
        {

            try
            {
                var result = await _locationService.CreateLocation(locationUnitModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }


        /// <summary>
        /// Create a new Location type
        /// </summary>
        /// <param name="locationUnitModel"></param>
        /// <returns></returns>
        [HttpPost("locationtype")]
        public async Task<ActionResult<LocationTypesModel>> CreateLocationType(CreateLocationTypesModel locationUnitModel)
        {

            try
            {
                var result = await _locationService.CreateLocationType(locationUnitModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        

        /// <summary>
        /// Update Location by ID
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPut("{locationID}")]
        public async Task<ActionResult<LocationUnitModel>> UpdateLocation(int locationID, LocationUnitForUpdate location)
        {
            try
            {
                var result = await _locationService.UpdateLocation(locationID, location);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Update Location Type
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPut("locationtype/{locationID}")]
        public async Task<ActionResult<LocationTypesModel>> UpdateLocationType(int locationID, EditLocationTypesModel location)
        {
            try
            {
                var result = await _locationService.UpdateLocationType(locationID, location);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        

        /// <summary>
        /// Partialy Update location instance
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="patchLocation"></param>
        /// <returns></returns>
        [HttpPatch("{locationID}")]
        public async Task<ActionResult<LocationUnitModel>> PatchLocation(int locationID, JsonPatchDocument patchLocation)
        {

            try
            {
                return Ok(await _locationService.PatchLocation(locationID, patchLocation));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        /// <summary>
        /// Delete Location
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        [HttpDelete("{locationId}")]
        public async Task<ActionResult<bool>> DeleteLocation(int locationId)
        {
            try
            {
                return Ok(await _locationService.DeleteLocation(locationId));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }


        /// <summary>
        /// Delete Location type
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        /// <response code="200">returns 1</response>
        [HttpDelete("locationtype/{locationId}")]
        public async Task<ActionResult<int>> DeleteLocationType(int locationId)
        {
            try
            {
                return Ok(await _locationService.DeleteLocationType(locationId));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
        
    }
}
