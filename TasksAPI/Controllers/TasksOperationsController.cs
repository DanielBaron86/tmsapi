using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TasksAPI.Configuration;
using TasksAPI.Interfaces;
using TasksAPI.Models;

namespace TasksAPI.Controllers
{
    [Route("api/v{version:apiVersion}/operations")]
    [ApiController]
    [Authorize(Roles = "clerk")]
    [SwaggerControllerOrder(4)]
    public class TasksOperationsController : ControllerBase
    {
        private readonly ITasksService _tasksServices;
        private readonly IReportsServices _ReportsServices;
        public TasksOperationsController(ITasksService tasksServices, IReportsServices reportsServices)
        {
            _tasksServices = tasksServices ?? throw new ArgumentNullException(nameof(tasksServices));
            _ReportsServices = reportsServices ?? throw new ArgumentNullException(nameof(reportsServices));
        }

        /// <summary>
        /// Supply a list of items requested by a fulfilment task
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="FulfilProcurements"></param>
        /// <returns></returns>
        [HttpPost("procurements/{taskID}")]
        public async Task<ActionResult<ReturnFulfillTask>> FullfillProcurement(int taskID, IEnumerable<FulfillModel> FulfilProcurements)
        {
            try
            {
                return Ok(await _tasksServices.FullfillProcurement(taskID, FulfilProcurements));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Supply a list of items requested by a transfer task
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="fulfillGoodsTransfer"></param>
        /// <returns></returns>
        [HttpPost("transfers/{taskID}")]
        public async Task<ActionResult<ReturnFulfillTransferTask>> FulfillTranfer(int taskID, FulfillTransferTask fulfillGoodsTransfer)
        {
            try
            {
                return Ok(await _tasksServices.FulfillTranfer(taskID, fulfillGoodsTransfer));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Get the results of a report as CSV
        /// </summary>
        /// <param name="reportResultsTaskId"></param>
        /// <returns></returns>
        [HttpGet("reports/{reportResultsTaskId}")]
        public async Task<FileResult> GetTaskResults(int reportResultsTaskId)
        {
            try
            {
                var result = await _ReportsServices.RetrieveReportResults(reportResultsTaskId);

                return (File(Encoding.UTF8.GetBytes(result.ReportResults), "text/csv", $"Result-{result.RunDate}.csv"));
            }
            catch (Exception ex)
            {

                return (File(Encoding.UTF8.GetBytes(ex.Message), "text/csv", $"Error.csv"));
            }
        }

        /// <summary>
        /// Run an existing Report Task
        /// </summary>
        /// <param name="reportTaskId"></param>
        /// <returns></returns>
        [HttpPost("reports/{reportTaskId}")]

        public async Task<ActionResult<int>> RunReportTask(int reportTaskId)
        {
            try
            {
                return Ok((await _ReportsServices.RunReport(reportTaskId)));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
