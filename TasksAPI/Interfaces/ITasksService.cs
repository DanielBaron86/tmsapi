using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TasksAPI.Entities;
using TasksAPI.Models;

namespace TasksAPI.Interfaces
{
    public interface ITasksService
    {

        public Task<IEnumerable<TasksModel>> GetAllTasks(int? taskType, int? taskStatus);
        public Task<IEnumerable<TasksModelWithProcurements>> GetAllProcurementTasks();
        public Task<IEnumerable<TasksModelWithTransfer>> GetAllTransferTasks();
        public Task<TasksModel> GetTasksByID(int taskID);
        Task<TasksModel> UpdateTask(int taskID, UpdateTasksModel taskToUpdate);
        public Task<TasksModelWithProcurements> GetTasksModelWithProcurementsTasksByID(int taskID);
        public Task<TasksModelWithTransfer> GetTasksModelWithTransferTasksByID(int taskID);

        public Task<TasksModel> CreateTask(CreateTasksModel newTask);

        public Task<TasksModelWithProcurements> CreateProcurement(CreateProcurementModel taskModel);

        public Task<ReturnTransferTask> CreateTransfer(CreateTransferModel taskModel);

        Task<TasksModelWithProcurements> AddItemsToProcurementTask(int taskID, ICollection<GoodsOrder> GoodsOrder);
        Task<ReturnTransferTask> AddItemsToTransferTask(int taskID, GoodsTransfer GoodsOrder, bool checkTask = true);

        Task<IEnumerable<ProcurementsSubtaskModel>> UpdateProcurementTaskDetails(IEnumerable<ProcurementsSubtaskModelForUpdate> taskModel);
        Task<IEnumerable<TasksEntities_TransferModel>> UpdateTransferSubTasks(IEnumerable<TransferSubtaskModelForUpdate> taskModel);
        Task<ReturnFulfillTask> FullfillProcurement(int taskID, IEnumerable<FulfillModel> FulfilProcurements);
        Task<ReturnFulfillTransferTask> FulfillTranfer(int taskID, FulfillTransferTask fulfillGoodsTransfer);
        Task<int> DeleteInventoryTask(int taskID);
    }
}
