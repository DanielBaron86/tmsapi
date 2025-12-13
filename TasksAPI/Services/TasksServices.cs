using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TasksAPI.DataBaseContext;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;

namespace TasksAPI.Services
{
    public class TasksServices : ITasksService
    {
        private readonly DatabaseConnectContext _DBContext;
        private readonly IMapper _mapper;
        private readonly IGoodsServices _goodsService;

        public TasksServices(DatabaseConnectContext context, IMapper mapper, IGoodsServices goodsService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _DBContext = context ?? throw new ArgumentNullException(nameof(context));
            _goodsService = goodsService ?? throw new ArgumentNullException(nameof(goodsService));
        }

        public async Task<IEnumerable<TasksModel>> GetAllTasks(int? taskType, int? taskStatus)
        {

            if (taskType != null && taskStatus == null)
            {
                return _mapper.Map<IEnumerable<TasksModel>>(await _DBContext.TasksEntities.Where(t => (int)t.TaskType == taskType).ToListAsync());
            }

            if (taskStatus != null && taskType == null)
            {
                return _mapper.Map<IEnumerable<TasksModel>>(await _DBContext.TasksEntities.Where(t => (int)t.TaskStatus == taskStatus).ToListAsync());
            }

            if (taskStatus != null && taskType != null)
            {
                return _mapper.Map<IEnumerable<TasksModel>>(await _DBContext.TasksEntities.Where(t => (int)t.TaskType == taskType).Where(t => (int)t.TaskStatus == taskStatus).ToListAsync());
            }

            return _mapper.Map<IEnumerable<TasksModel>>(await _DBContext.TasksEntities.ToListAsync());
        }

        public async Task<TasksModel> GetTasksByID(int taskID)
        {
            var t = await _DBContext.TasksEntities.FirstOrDefaultAsync(t => t.Id == taskID) ??throw new ArgumentException("Task not found");

            return _mapper.Map<TasksModel>(t);
        }

        public async Task<TasksModel> UpdateTask(int taskID, UpdateTasksModel taskToUpdate)
        {
            var task = await _DBContext.TasksEntities.FirstOrDefaultAsync(t => t.Id == taskID) ??throw new ArgumentException("Task not found");

            _mapper.Map(taskToUpdate, task);
            await _DBContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<TasksModel>(task);
        }

        public async Task<TasksModelWithProcurements> CreateProcurement(CreateProcurementModel taskModel)
        {
            _= _DBContext.Users
                .Where( t => t.UserTypeId != (int)EnumTypes.CLIENT && t.Status == (int)DBEntityStatus.ACTIVE)
                .FirstOrDefault( t => t.Id == taskModel.UserID) ?? throw new ArgumentException($"User {taskModel.UserID} not found or inactive");

            var newTaskToCreate = new CreateTasksModel { Description = taskModel.Description, TaskType = TaskTypes.PROCUREMENT, userID = taskModel.UserID };
            var createdTask = await CreateTask(newTaskToCreate);


            foreach (var singleGood in taskModel.GoodsOrder)
            {

                var newProcurementTask = new TasksEntitiesProcurements { TaskID = createdTask.Id, GoodTypeID = singleGood.GoodTypeID, Quantity = singleGood.Quantity, RemainingQuantity = singleGood.Quantity, Location = singleGood.Location };

                await _DBContext.TasksEntitiesProcurements.AddAsync(newProcurementTask);
                await _DBContext.SaveChangesAsync(CancellationToken.None);
            }
            var result = await _DBContext.TasksEntities.Include(t => t.TasksEntitiesProcurements).FirstOrDefaultAsync(t => t.Id == createdTask.Id);
            return _mapper.Map<TasksModelWithProcurements>(result);
        }

        public async Task<TasksModel> CreateTask(CreateTasksModel newTask)
        {
            var createEntity = new TasksEntities { TaskType = newTask.TaskType, Description = newTask.Description, userID = newTask.userID };

            await _DBContext.TasksEntities.AddAsync(createEntity);
            await _DBContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<TasksModel>(createEntity);
        }

        public async Task<TasksModelWithProcurements> GetTasksModelWithProcurementsTasksByID(int taskID)
        {
            var t = await _DBContext.TasksEntities.Include(t => t.TasksEntitiesProcurements).FirstOrDefaultAsync(t => t.Id == taskID);
            if (t == null || t.TaskType != TaskTypes.PROCUREMENT)
            {
               throw new ArgumentException("Task not found");
            }
            return _mapper.Map<TasksModelWithProcurements>(t);
        }

        public async Task<TasksModelWithTransfer> GetTasksModelWithTransferTasksByID(int taskID)
        {
            var t = await _DBContext.TasksEntities.Include(t => t.TasksEntitiesTransfer).FirstOrDefaultAsync(t => t.Id == taskID);
            if (t == null || t.TaskType != TaskTypes.TRANSFER)
            {
               throw new ArgumentException("Task not found");
            }
            return _mapper.Map<TasksModelWithTransfer>(t);
        }

        public async Task<TasksModelWithProcurements> AddItemsToProcurementTask(int taskID, ICollection<GoodsOrder> GoodsOrder)
        {
            var taskToUpdate = await _DBContext.TasksEntities.FirstOrDefaultAsync(t => t.Id == taskID);
            if (taskToUpdate == null || taskToUpdate.TaskType != TaskTypes.PROCUREMENT)
            {
               throw new ArgumentException("Task not found");
            }

            foreach (var order in GoodsOrder)
            {
                await _DBContext.TasksEntitiesProcurements.AddAsync(new TasksEntitiesProcurements { TaskID = taskID, GoodTypeID = order.GoodTypeID, Quantity = order.Quantity, RemainingQuantity = order.Quantity, Location=order.Location});
                await _DBContext.SaveChangesAsync(CancellationToken.None);
            }

            return _mapper.Map<TasksModelWithProcurements>(await _DBContext.TasksEntities.Include(t => t.TasksEntitiesProcurements).FirstOrDefaultAsync(t => t.Id == taskID));
        }

        public async Task<ReturnTransferTask> CreateTransfer(CreateTransferModel taskModel)
        {
            _ = _DBContext.Users
                .Where(t => t.UserTypeId != (int)EnumTypes.CLIENT && t.Status == (int)DBEntityStatus.ACTIVE)
                .FirstOrDefault(t => t.Id == taskModel.UserID) ?? throw new ArgumentException($"User {taskModel.UserID} not found or inactive");

            var newTaskToCreate = new CreateTasksModel { TaskType = TaskTypes.TRANSFER, userID = taskModel.UserID, Description = taskModel.Description };
            var createdTask = await CreateTask(newTaskToCreate);
            var returnValue = await AddItemsToTransferTask(createdTask.Id, taskModel.GoodsTransfer, false);
            return returnValue;
        }

        public async Task<ReturnTransferTask> AddItemsToTransferTask(int taskID, GoodsTransfer GoodsOrder, bool checkTask = true)
        {
            var rejected = new List<RejectedGoodsTransfer>();


            var taskToUpdate = await _DBContext.TasksEntities.Where(t => t.TaskType == TaskTypes.TRANSFER)
                    .FirstOrDefaultAsync(t => t.Id == taskID)??throw new ArgumentException("Task not found");

            var existingTransfers = _DBContext.TasksEntitiesTransfer.Where(t => GoodsOrder.GoodID.Contains(t.GoodID))
                                                        .Where(t => t.TaskStatus == TaskTypesStatus.OPEN || t.TaskStatus == TaskTypesStatus.PENDING)
                                                        .Select(t => t.GoodID).Distinct()
                                                        .ToList();

            foreach (var exists in existingTransfers)
            {
                rejected.Add(new RejectedGoodsTransfer { GoodID = exists, ToLocation = GoodsOrder.ToLocation, Reason = "A transfer task for that items already exists" });
            }
            foreach (var order in GoodsOrder.GoodID.Except(existingTransfers))
            {
                var transferedItem = await _goodsService.GetGoodById(order);


                if (transferedItem == null)
                {
                    rejected.Add(new RejectedGoodsTransfer { GoodID = order, ToLocation = GoodsOrder.ToLocation, Reason = "Item not found" });
                }
                else
                {
                    var allowedStatus = new[] { GoodsStatus.AVAILABLE, GoodsStatus.PENDING, GoodsStatus.IN_TRANSIT };
                    if (!allowedStatus.Contains(transferedItem.Status))
                    {
                        rejected.Add(new RejectedGoodsTransfer { GoodID = order, serialNumber = transferedItem.serialNumber, ToLocation = GoodsOrder.ToLocation, Reason = "Item In wrong status" });
                    }
                    else
                    {

                        await _goodsService.CreateMovementHistory(order, 0, 0, (int)GoodsStatus.IN_TRANSIT, taskToUpdate.userID); 
                        
                        var markForTransfer = new JsonPatchDocument();
                        markForTransfer.Replace("Status", (int)GoodsStatus.IN_TRANSIT);
                        await _goodsService.PatchGood(order, markForTransfer);
                        
                        await _DBContext.TasksEntitiesTransfer.AddAsync(new TasksEntitiesTransfer { TaskID = taskID, GoodID = order, FromLocation = transferedItem.LocationId, ToLocation = GoodsOrder.ToLocation, TaskStatus = TaskTypesStatus.PENDING, serialNumber = transferedItem.serialNumber });
                        await _DBContext.SaveChangesAsync(CancellationToken.None);
                    }
                }


            }
            var result = _mapper.Map<TasksModelWithTransfer>(await _DBContext.TasksEntities.Include(t => t.TasksEntitiesTransfer).FirstOrDefaultAsync(t => t.Id == taskID));
            var returnValue = new ReturnTransferTask { TasksModelWithTransfer = result, RejectedGoodsTransfer = rejected };

            return returnValue;
        }

        public async Task<IEnumerable<ProcurementsSubtaskModel>> UpdateProcurementTaskDetails(IEnumerable<ProcurementsSubtaskModelForUpdate> taskModel)
        {
            var idList = new List<int>();

            foreach (var task in taskModel)
            {
                var taskToBeUpdated = await _DBContext.TasksEntitiesProcurements.FirstOrDefaultAsync(t => t.Id == task.Id);
                if (taskToBeUpdated == null)
                {
                   throw new ArgumentException("Task not found");

                }
                _mapper.Map(task, taskToBeUpdated);
                await _DBContext.SaveChangesAsync(CancellationToken.None);
                idList.Add(taskToBeUpdated.Id);
            }


            return _mapper.Map<IEnumerable<ProcurementsSubtaskModel>>(await _DBContext.TasksEntitiesProcurements.Where(t => idList.Contains(t.Id)).ToListAsync());
        }

        public async Task<IEnumerable<TasksModelWithProcurements>> GetAllProcurementTasks()
        {
            return _mapper.Map<IEnumerable<TasksModelWithProcurements>>(await _DBContext.TasksEntities.Where(t => t.TaskType == TaskTypes.PROCUREMENT).Include(t => t.TasksEntitiesProcurements).ToListAsync());
        }

        public async Task<IEnumerable<TasksModelWithTransfer>> GetAllTransferTasks()
        {
            return _mapper.Map<IEnumerable<TasksModelWithTransfer>>(await _DBContext.TasksEntities.Where(t => t.TaskType == TaskTypes.TRANSFER).Include(t => t.TasksEntitiesTransfer).ToListAsync());
        }

        public async Task<IEnumerable<TasksEntities_TransferModel>> UpdateTransferSubTasks(IEnumerable<TransferSubtaskModelForUpdate> taskModel)
        {
            var idList = new List<int>();

            foreach (var task in taskModel)
            {
                var taskToBeUpdated = await _DBContext.TasksEntitiesTransfer.FirstOrDefaultAsync(t => t.Id == task.Id);
                if (taskToBeUpdated == null)
                {
                   throw new ArgumentException("Task not found");

                }
                _mapper.Map(task, taskToBeUpdated);
                await _DBContext.SaveChangesAsync(CancellationToken.None);
                idList.Add(taskToBeUpdated.Id);
            }


            return _mapper.Map<IEnumerable<TasksEntities_TransferModel>>(await _DBContext.TasksEntitiesTransfer.Where(t => idList.Contains(t.Id)).ToListAsync());
        }

        public async Task<ReturnFulfillTask> FullfillProcurement(int taskID, IEnumerable<FulfillModel> FulfilProcurements)
        {
            var task = await _DBContext.TasksEntities
                            .Where(t => t.TaskStatus == TaskTypesStatus.OPEN || t.TaskStatus == TaskTypesStatus.PENDING)
                            .FirstOrDefaultAsync(t => t.Id == taskID) ??throw new ArgumentException("Task not found"); ;
            var rejected = new List<RejectedProcurementTransfer>();
            var itemList = new List<GoodsModels>();
            var SNL = new List<string>();

            var existingItems = new List<string>();
            foreach (var fulfilment in FulfilProcurements)
            {
                foreach (var item in fulfilment.FulfillGoodsModels)
                {
                    existingItems.Add(item.serialNumber.ToUpper());
                }
            }

            var existingTransfers = _DBContext.GoodsTypesInstances.Where(t => existingItems.Contains(t.serialNumber))
                                                    .Where(t => t.Status != (int)GoodsStatus.DELETED || t.Status != (int)GoodsStatus.LOST)
                                                    .Select(t => t.serialNumber).Distinct()
                                                    .ToList();

            await ProcessItemToAdd(taskID, FulfilProcurements, rejected, itemList, SNL, existingTransfers);

            CheckAndUpdateProcurementTaskStatus(taskID, task);

            return new ReturnFulfillTask { GoodsModels = itemList, RejectedProcurementTransfer = rejected };
        }

        private async Task ProcessItemToAdd(int taskID, IEnumerable<FulfillModel> FulfilProcurements, List<RejectedProcurementTransfer> rejected, List<GoodsModels> itemList, List<string> SNL, List<string> existingTransfers)
        {
            foreach (var fulfilment in FulfilProcurements)
            {
                var subTask = await _DBContext.TasksEntitiesProcurements
                    .Where(t => t.TaskID == taskID)
                    .FirstOrDefaultAsync(t => t.Id == fulfilment.subTaskId);

                if (subTask == null)
                {
                    rejected.Add(HandleRejectedProcurementTransfer(0, fulfilment.Supplier, fulfilment.subTaskId, "", $"Procurement substask {fulfilment.subTaskId} not found"));
                }


                if (subTask != null && (fulfilment.FulfillGoodsModels.Count > (subTask.RemainingQuantity)))
                {
                    rejected.Add(HandleRejectedProcurementTransfer(subTask.Location, fulfilment.Supplier, fulfilment.subTaskId, "", $"List of items is larger than requested quantity"));
                }
                else if (subTask != null)
                {
                    int countAddedItems = 0;
                    countAddedItems = await AddNewProccesedItems(rejected, itemList, SNL, existingTransfers, fulfilment, subTask, countAddedItems);
                    if (countAddedItems > 0)
                    {
                        var updateTask = new ProcurementsSubtaskModelForUpdate { Id = subTask.Id, GoodTypeID = subTask.GoodTypeID, Location = subTask.Location, Quantity = subTask.Quantity, RemainingQuantity = subTask.RemainingQuantity - countAddedItems };
                        await UpdateProcurementTaskDetails(new[] { updateTask });
                    }
                }
            }
        }

        private async Task<int> AddNewProccesedItems(List<RejectedProcurementTransfer> rejected, List<GoodsModels> itemList, List<string> SNL, List<string> existingTransfers, FulfillModel fulfilment, TasksEntitiesProcurements? subTask, int countAddedItems)
        {
            foreach (var item in fulfilment.FulfillGoodsModels)
            {
                if (!SNL.Contains(item.serialNumber.ToUpper()) && !existingTransfers.Contains(item.serialNumber.ToUpper()))
                {
                    try
                    {
                        var addedItem = await _goodsService.CreateGood(new CreateGoodsModels
                        {
                            GoodModelId = subTask.GoodTypeID,
                            Price = item.Price,
                            serialNumber = item.serialNumber.ToUpper(),
                            LocationId = subTask.Location,
                            Status = GoodsStatus.AVAILABLE
                        });

                        await _goodsService.CreateMovementHistory(addedItem.Id, fulfilment.Supplier, addedItem.LocationId, (int)GoodsStatus.AVAILABLE, fulfilment.userId);
                        SNL.Add(item.serialNumber.ToUpper());
                        itemList.Add(addedItem);
                        countAddedItems++;
                    }
                    catch (Exception ex)
                    {
                        rejected.Add(HandleRejectedProcurementTransfer(subTask.Location, fulfilment.Supplier, fulfilment.subTaskId, item.serialNumber.ToUpper(), ex.Message));
                    }
                }
                else
                {
                    rejected.Add(HandleRejectedProcurementTransfer(subTask.Location, fulfilment.Supplier, fulfilment.subTaskId, item.serialNumber.ToUpper(), "Duplicate serial number"));
                }


            }

            return countAddedItems;
        }

        public async Task<ReturnFulfillTransferTask> FulfillTranfer(int taskID, FulfillTransferTask fulfillGoodsTransfer)
        {
            var transferTask = _DBContext.TasksEntities.FirstOrDefault(t => t.Id == taskID) ??throw new ArgumentException("Task not found");
            if (transferTask.TaskStatus != TaskTypesStatus.OPEN && transferTask.TaskStatus != TaskTypesStatus.PENDING)
            {
               throw new ArgumentException($"Task status {transferTask.TaskStatus} - already proccessed");
            }



            List<string> rejected;
            var existing = new List<string>();

            var itemList = await _DBContext.TasksEntitiesTransfer.Where(t => t.TaskID == transferTask.Id)
                                                     .Where(t => t.TaskStatus == TaskTypesStatus.PENDING || t.TaskStatus == TaskTypesStatus.OPEN)
                                                     .ToListAsync(CancellationToken.None);

            foreach (var item in itemList)
            {

                if (fulfillGoodsTransfer.fulfillGoodsTransfer.Contains(item.serialNumber))
                {
                    var patchItem = new JsonPatchDocument();
                    patchItem.Replace("locationId", item.ToLocation);
                    patchItem.Replace("Status", (int)GoodsStatus.AVAILABLE);

                    var itemToPatch = await _DBContext.GoodsTypesInstances.FirstOrDefaultAsync(g => g.Id == item.GoodID) ??throw new ArgumentException("Item not found");
                    await _goodsService.CreateMovementHistory(itemToPatch.Id, itemToPatch.LocationId, item.ToLocation, (int)GoodsStatus.AVAILABLE, fulfillGoodsTransfer.userID);

                    patchItem.ApplyTo(itemToPatch);
                    existing.Add(item.serialNumber);


                    var patchTranfer = new JsonPatchDocument();
                    patchTranfer.Replace("TaskStatus", TaskTypesStatus.COMPLETE);
                    var task = _DBContext.TasksEntitiesTransfer.FirstOrDefault(t => t.Id == item.Id);
                    patchTranfer.ApplyTo(task);



                    await _DBContext.SaveChangesAsync(CancellationToken.None);
                }
            }
            CheckAndUpdateTransferTaskStatus(taskID);
            var movedItems = _mapper.Map<IEnumerable<GoodsModels>>(_DBContext.GoodsTypesInstances.Where(t => existing.Contains(t.serialNumber)).Where(t => t.Status == (int)GoodsStatus.AVAILABLE).ToList());
            rejected = fulfillGoodsTransfer.fulfillGoodsTransfer.Except(existing).ToList();
            return new ReturnFulfillTransferTask { GoodsModels = (ICollection<GoodsModels>)movedItems, RejectedProcurementTransfer = rejected };

        }

        private void CheckAndUpdateProcurementTaskStatus(int taskID, TasksEntities task)
        {
            var tasksRemaining = _DBContext.TasksEntitiesProcurements.Where(t => t.RemainingQuantity > 0)
                                                        .Where(t => t.TaskID == taskID)
                                                        .ToList();
            var patch = new JsonPatchDocument();
            if (tasksRemaining.Count > 0)
            {
                patch.Replace("TaskStatus", TaskTypesStatus.OPEN);
            }
            else
            {
                patch.Replace("TaskStatus", TaskTypesStatus.COMPLETE);
            }

            patch.ApplyTo(task);
            _DBContext.SaveChanges();
        }
        private void CheckAndUpdateTransferTaskStatus(int TaskID)
        {

            var patchTranfer = new JsonPatchDocument();
            var tasksRemaining = _DBContext.TasksEntitiesTransfer.Where(t => t.TaskID == TaskID)
                                                     .Where(t => t.TaskStatus == TaskTypesStatus.PENDING || t.TaskStatus == TaskTypesStatus.OPEN)
                                                     .ToList();

            if (tasksRemaining.Count > 0)
            {
                patchTranfer.Replace("TaskStatus", TaskTypesStatus.OPEN);
            }
            else
            {
                patchTranfer.Replace("TaskStatus", TaskTypesStatus.COMPLETE);
            }

            var main_task = _DBContext.TasksEntities.FirstOrDefault(t => t.Id == TaskID);
            patchTranfer.ApplyTo(main_task);
            _DBContext.SaveChanges();



        }
        private static RejectedProcurementTransfer HandleRejectedProcurementTransfer(int location, int Supplier, int subTaskId, string SN, string Reason)
        {
            var rejectedItem = new RejectedProcurementTransfer
            {
                Location = location,
                Supplier = Supplier,
                subTaskId = subTaskId,
                SerialNumber = SN,
                Reason = Reason
            };
            return rejectedItem;
        }

        public async  Task<int> DeleteInventoryTask(int taskID)
        {
            var task = _DBContext.TasksEntities.Where( t => t.TaskStatus == TaskTypesStatus.PENDING).FirstOrDefault(t => t.Id == taskID) ?? throw new ArgumentException($"Task not found {nameof(taskID)}, or started procesing");
            if(task.TaskType == TaskTypes.PROCUREMENT)
            {
                var subtasks =_DBContext.TasksEntitiesProcurements.Where(t => t.TaskID == task.Id).ToList();
                await DeleteProcurementSubtask(subtasks);
            }
            if (task.TaskType == TaskTypes.TRANSFER)
            {
                var subtasks = _DBContext.TasksEntitiesTransfer.Where(t => t.TaskID == task.Id).ToList();
                await DeleteTransferSubtask(subtasks);

            }
            
            _DBContext.Remove(task);
            return await _DBContext.SaveChangesAsync(CancellationToken.None);
        }

        private async Task<int> DeleteProcurementSubtask(IEnumerable<TasksEntitiesProcurements> list)
        {
            _DBContext.RemoveRange(list);
           return await _DBContext.SaveChangesAsync(CancellationToken.None);
        }

        private async Task<int> DeleteTransferSubtask(IEnumerable<TasksEntitiesTransfer> list)
        {
            foreach (var item in list)
            {
                var patchDocument = new JsonPatchDocument();
                patchDocument.Replace("Status",(int)GoodsStatus.AVAILABLE);
                await _goodsService.PatchGood(item.GoodID, patchDocument);
                await _goodsService.CreateMovementHistory(item.GoodID,0,0,(int)GoodsStatus.AVAILABLE,0);
                _DBContext.Remove(item);
            }
           return await _DBContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}


