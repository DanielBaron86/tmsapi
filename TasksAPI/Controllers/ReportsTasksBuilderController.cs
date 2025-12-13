using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasksAPI.Configuration;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;


namespace TasksAPI.Controllers
{

    [Route("api/v{version:apiVersion}/reports")]
    [ApiController]
    [Authorize(Policy = "Supervisor")]
    [SwaggerControllerOrder(2)]
    public class ReportsTasksBuilderController : ControllerBase
    {
        private readonly IReportsServices _ReportsServices;
        
        public ReportsTasksBuilderController(IReportsServices reportsServices)
        {
            _ReportsServices = reportsServices ?? throw new ArgumentNullException(nameof(reportsServices));
            
        }

      

        /// <summary>
        /// Create a new inventory type report. ReportMode 1 - Full list, 2 - Summary
        /// </summary>
        /// <param name="InventoryReportsModel"></param>
        /// <returns></returns>
        [HttpPost("inventory")]
        public async Task<ActionResult<ReportsEntities>> CreateInventoryReport(CreateReportsEntitiesModel InventoryReportsModel)
        {
            try
            {
                return Ok(await _ReportsServices.CreateInventoryReport(InventoryReportsModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create Sales Report. ReportMode 1 - Full list, 2 - Summary
        /// </summary>
        /// <param name="salesReportsModel"></param>
        /// <returns></returns>
        [HttpPost("sales")]
        public async Task<ActionResult<int>> CreateSalesReport(CreateSalesReportsEntitiesModel salesReportsModel)
        {
            try
            {
                return Ok(await _ReportsServices.CreateSalesReport(salesReportsModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Delete Report Task
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [HttpDelete("{reportId}")]
        public async Task<ActionResult<int>> DeleteReport(int reportId)
        {
            try
            {
                return Ok(await _ReportsServices.DeleteReport(reportId));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Delete the results of a report Task
        /// </summary>
        /// <param name="resultsId"></param>
        /// <returns></returns>
        [HttpDelete("results/{resultsId}")]
        public async Task<ActionResult<int>> DeleteReportResults(int resultsId)
        {
            try
            {
                return Ok(await _ReportsServices.DeleteReportResults(resultsId));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }


}
