using System.Text.Json;
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
        const int MaxPagesSize = 100;
        
        private readonly ILocationService _locationService;
        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        ///  <summary>
        ///  Get a list of all locations
        ///  </summary>
        ///  <param name="pageNumber"></param>
        ///  <param name="pageSize"></param>
        ///  <response code="200">return list of locations</response>
        [HttpGet]

        public async Task<ActionResult<IEnumerable<LocationTypesModel>>> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageSize > MaxPagesSize) pageSize = MaxPagesSize;
                var (goodTypes, paginationMetadata) = await _locationService.GetLocations(pageNumber, pageSize);

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
                Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");
                return Ok(goodTypes);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        
        ///  <summary>
        ///  Get a list of all locations
        ///  </summary>
        ///  <param name="queryFilters"></param>
        ///  <response code="200">return list of locations</response>
        [HttpPost("query")]

        public async Task<ActionResult<IEnumerable<LocationTypesModel>>> GetAllWithQuery(QueryFilters queryFilters)
        {
            try
            {
                var (goodTypes, paginationMetadata) = await _locationService.GetLocationsWithConditions(queryFilters);

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
                Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");
                return Ok(goodTypes);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Get a list of all location types
        /// </summary>
        /// <returns></returns>
        [HttpGet("locationtype")]

        public async Task<ActionResult<IEnumerable<LocationTypesModel>>> GetAllLocationType(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageSize > MaxPagesSize) pageSize = MaxPagesSize;
                var (locationTypes, paginationMetadata) = await _locationService.GetLocationTypess(pageNumber, pageSize);

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
                Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");
                return Ok(locationTypes);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
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
