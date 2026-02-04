using System.Text.Json.Serialization;
namespace TasksAPI.Models
{
    public class TasksModel : BaseModel
    {

        public int Id { get; set; }
        [JsonRequired]
        public TaskTypes TaskType { get; set; }
        [JsonRequired]
        public TaskTypesStatus TaskStatus { get; set; }

        public string? Description { get; set; }
        [JsonRequired]
        public int CreatorId { get; set; }
        [JsonRequired]
        public string UserName { get; set; }
    }

    public class TasksModelWithProcurements : TasksModel
    {
        public ICollection<ProcurementsSubtaskModel>? TasksEntitiesProcurements { get; set; } = new List<ProcurementsSubtaskModel>();
    }

    public class TasksModelWithTransfer : TasksModel
    {
        public ICollection<TasksEntities_TransferModel>? TasksEntitiesTransferList { get; set; } = new List<TasksEntities_TransferModel>();

    }

    public class ReturnTransferTask
    {

        [JsonRequired]
        public TasksModelWithTransfer TasksModelWithTransfer { get; set; } = default!;
        public ICollection<RejectedGoodsTransfer>? RejectedGoodsTransfer { get; set; } = new List<RejectedGoodsTransfer>();
    }

    public class ReturnFulfillTask
    {
        [JsonRequired]
        public ICollection<GoodsModels> GoodsModels { get; set; } = new List<GoodsModels>();
        public ICollection<RejectedProcurementTransfer>? RejectedProcurementTransfer { get; set; } = new List<RejectedProcurementTransfer>();
    }

    public class ReturnFulfillTransferTask
    {
        [JsonRequired]
        public ICollection<GoodsModels> GoodsModels { get; set; } = new List<GoodsModels>();
        public ICollection<string>? RejectedProcurementTransfer { get; set; } = new List<string>();
    }


    public class ProcurementsSubtaskModel
    {
        public int Id { get; set; }
        [JsonRequired]
        public int TaskId { get; set; }
        [JsonRequired]
        public int GoodTypeId { get; set; }
        [JsonRequired]
        public string GoodType { get; set; }
        [JsonRequired]
        public int Location { get; set; }
        [JsonRequired]
        public int Quantity { get; set; }
        [JsonRequired]
        public int? RemainingQuantity { get; set; }
    }


    public class ProcurementsSubtaskModelForUpdate
    {
        public int Id { get; set; }
        [JsonRequired]
        public int GoodTypeId { get; set; }
        [JsonRequired]
        public string GoodType { get; set; }
        [JsonRequired]
        public int Location { get; set; }
        [JsonRequired]
        public int Quantity { get; set; }
        public int? RemainingQuantity { get; set; }
    }


    public class TasksEntities_TransferModel
    {
        public int Id { get; set; }
        [JsonRequired]
        public int TaskId { get; set; }
        [JsonRequired]
        public int GoodId { get; set; }
        [JsonRequired]
        public string SerialNumber { get; set; } = default!;
        [JsonRequired]
        public int FromLocation { get; set; }
        [JsonRequired]
        public string FromLocationName { get; set; }
        [JsonRequired]
        public int ToLocation { get; set; }
        [JsonRequired]
        public string ToLocationName { get; set; }

        public TaskTypesStatus? TaskStatus { get; set; }
    }

    public class TransferSubtaskModelForUpdate
    {
        public int Id { get; set; }
        [JsonRequired]
        public int GoodId { get; set; }
        [JsonRequired]
        public string SerialNumber { get; set; } = default!;
        [JsonRequired]
        public int FromLocation { get; set; }
        [JsonRequired]
        public string FromLocationName { get; set; }
        [JsonRequired]
        public int ToLocation { get; set; }
        [JsonRequired]
        public string ToLocationName { get; set; }

        public TaskTypesStatus? TaskStatus { get; set; }
    }

    ///// Creation Models ////

    public class CreateTasksModel
    {
        [JsonRequired]
        public TaskTypes TaskType { get; set; }
        [JsonRequired]
        public int UserId { get; set; }
        [JsonRequired]
        public string UserName { get; set; }
        [JsonRequired]
        public string Description { get; set; } = string.Empty;
    }


    public class UpdateTasksModel
    {
        [JsonRequired]
        public TaskTypesStatus TaskStatus { get; set; }
        [JsonRequired]
        public string Description { get; set; } = string.Empty;
    }


    public class CreateProcurementModel
    {
        [JsonRequired]
        public int CreatorId { get; set; }
        [JsonRequired]
        public string UserName { get; set; }
        [JsonRequired]
        public string Description { get; set; }
        public ICollection<GoodsOrder>? GoodsOrder { get; set; } = new List<GoodsOrder>();
    }



    public class CreateTransferModel
    {
        [JsonRequired]
        public int CreatorId { get; set; }
        [JsonRequired]
        public string UserName { get; set; }
        [JsonRequired]
        public string? Description { get; set; }
        public GoodsTransfer? GoodsTransfer { get; set; }

    }


    ///// Other stuff



    public class GoodsOrder
    {
        [JsonRequired]
        public int GoodTypeId { get; set; }
        [JsonRequired]
        public string GoodType { get; set; }
        [JsonRequired]
        public int Location { get; set; }
        [JsonRequired]
        public int Quantity { get; set; }
    }

    public class GoodsTransfer
    {
        [JsonRequired]
        public ICollection<int> GoodId { get; set; }
        [JsonRequired]
        public int ToLocation { get; set; }

    }

    public class FulfillGoodsTransfer
    {

        public string SerialNumber { get; set; } = default!;

    }

    public class RejectedGoodsTransfer
    {
        public int GoodId { get; set; }
        public string SerialNumber { get; set; } = default!;
        public int FromLocation { get; set; }
        public int ToLocation { get; set; }
        public string Reason { get; set; } = String.Empty;
    }

    public class RejectedProcurementTransfer
    {
        public int Location { get; set; }
        public int Supplier { get; set; }
        public int SubTaskId { get; set; }
        public string SerialNumber { get; set; } = String.Empty;
        public string Reason { get; set; } = String.Empty;
    }

}
