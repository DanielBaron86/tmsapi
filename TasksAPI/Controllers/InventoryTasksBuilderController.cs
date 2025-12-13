using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasksAPI.Configuration;
using TasksAPI.Interfaces;
using TasksAPI.Models;


namespace TasksAPI.Controllers
{

    [Route("api/v{version:apiVersion}/tasks")]
    [ApiController]
    [Authorize(Policy = "Supervisor")]
    [SwaggerControllerOrder(1)]

    public class InventoryTasksBuilderController : ControllerBase
    {

        private readonly ITasksService _tasksServices;



        public InventoryTasksBuilderController(ITasksService tasksServices)
        {
            _tasksServices = tasksServices ?? throw new ArgumentNullException(nameof(tasksServices));
        }

        //[Authorize(Policy = "Supervisor")]
        /// <summary>
        /// Returns a list of existing tasks
        /// taskType 1 = PROCUREMENT, 2 = TRANSFER.
        /// taskStatus 1 = PENDING, 2 = OPEN, 3 = CLOSED  4 = COMPLETE
        /// </summary>
        /// <returns></returns>
        /// <response code="200">return list of tasks</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasksModel>>> GetAllTasks(int? taskType, int? taskStatus)
        {

            return Ok(await _tasksServices.GetAllTasks(taskType, taskStatus));
        }

        /** Generic Tasks **/

        /// <summary>
        /// Return Task by ID
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        /// <response code="200">return task</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{taskID}")]
        public async Task<ActionResult<TasksModel>> GetTaskByID(int taskID)
        {
            try
            {
                return Ok(await _tasksServices.GetTasksByID(taskID));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }


        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="taskToUpdate"></param>
        /// <returns></returns>
        [HttpPut("{taskID}")]
        public async Task<ActionResult<TasksModel>> UpdateTask(int taskID, UpdateTasksModel taskToUpdate)
        {
            try
            {
                return Ok(await _tasksServices.UpdateTask(taskID, taskToUpdate));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }


        /// <summary>
        /// Get all Procurements type tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet("procurement")]
        public async Task<ActionResult<TasksModelWithProcurements>> GetTasksModelWithProcurementsTasks()
        {
            try
            {
                return Ok(await _tasksServices.GetAllProcurementTasks());
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }

        [HttpGet("procurement/{taskID}")]
        public async Task<ActionResult<TasksModelWithProcurements>> ProcurementsTasksByID(int taskID)
        {
            try
            {
                return Ok(await _tasksServices.GetTasksModelWithProcurementsTasksByID(taskID));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }


        }


        /// <summary>
        /// Update Procurement Task Subtasks
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        /// 
        [HttpPut("procurement")]
        public async Task<ActionResult<IEnumerable<ProcurementsSubtaskModel>>> UpdateProcurementSubTasks(IEnumerable<ProcurementsSubtaskModelForUpdate> taskModel)
        {
            try
            {
                return Ok(await _tasksServices.UpdateProcurementTaskDetails(taskModel));

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Add a new items requests to an existing procurement task
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="GoodsOrder"></param>
        /// <returns></returns>
        [HttpPut("procurement/add/{taskID}")]
        public async Task<ActionResult<TasksModelWithProcurements>> AddItemsToProcurementTask(int taskID, ICollection<GoodsOrder> GoodsOrder)
        {
            try
            {
                return Ok(await _tasksServices.AddItemsToProcurementTask(taskID, GoodsOrder));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Get All Transfer Type tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet("transfer")]
        public async Task<ActionResult<TasksModelWithTransfer>> GetTasksModelWithTransferTasks()
        {
            try
            {
                return Ok(await _tasksServices.GetAllTransferTasks());
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }

        /// <summary>
        /// Returns Transfer Tasks by ID
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        [HttpGet("transfer/{taskID}")]
        public async Task<ActionResult<TasksModelWithTransfer>> GetTasksModelWithTransferTasksByID(int taskID)
        {
            try
            {
                return Ok(await _tasksServices.GetTasksModelWithTransferTasksByID(taskID));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }


        }


        /// <summary>
        /// Update Transfer Task Subtasks
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        [HttpPut("transfer")]
        public async Task<ActionResult<IEnumerable<TasksEntities_TransferModel>>> UpdateTransferSubTasks(IEnumerable<TransferSubtaskModelForUpdate> taskModel)
        {
            try
            {
                return Ok(await _tasksServices.UpdateTransferSubTasks(taskModel));

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Add a new items requests to an existing transfer task
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="GoodsOrder"></param>
        /// <returns></returns>
        [HttpPut("transfer/{taskID}")]
        public async Task<ActionResult<ReturnTransferTask>> AddItemsToTransferTask(int taskID, GoodsTransfer GoodsOrder)
        {
            try
            {
                return Ok(await _tasksServices.AddItemsToTransferTask(taskID, GoodsOrder));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Create a new procurement task with the given items
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        [HttpPost("procurement")]
        public async Task<ActionResult<TasksModelWithProcurements>> CreateProcurementTask(CreateProcurementModel taskModel)
        {
            try
            {
                return Ok(await _tasksServices.CreateProcurement(taskModel));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Create a new transfer task with the given items
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        [HttpPost("transfer")]
        
        public async Task<ActionResult<IEnumerable<TasksModelWithTransfer>>> CreateTranferTask(CreateTransferModel taskModel)
        {
            try
            {
                return Ok(await _tasksServices.CreateTransfer(taskModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete an inventory task
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        [HttpDelete("{taskID}")]
        public async Task<ActionResult<int>> DeleteInventoryTask(int taskID)
        {
            try
            {
                return Ok(await _tasksServices.DeleteInventoryTask(taskID));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
