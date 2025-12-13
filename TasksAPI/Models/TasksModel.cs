using System.ComponentModel.DataAnnotations.Schema;
using TasksAPI.Entities;

namespace TasksAPI.Models
{
    public class TasksModel : BaseModel
    {

        public int Id { get; set; }

        public TaskTypes TaskType { get; set; }

        public TaskTypesStatus TaskStatus { get; set; }

        public string? Description { get; set; }

        public int userID { get; set; }
    }

    public class TasksModelWithProcurements : TasksModel
    {
        public ICollection<ProcurementsSubtaskModel> TasksEntitiesProcurements { get; set; } = new List<ProcurementsSubtaskModel>();
    }

    public class TasksModelWithTransfer : TasksModel
    {
        public ICollection<TasksEntities_TransferModel> TasksEntitiesTransfer { get; set; } = new List<TasksEntities_TransferModel>();

    }

    public class ReturnTransferTask
    {

        public TasksModelWithTransfer TasksModelWithTransfer { get; set; } = default!;
        public ICollection<RejectedGoodsTransfer> RejectedGoodsTransfer { get; set; } = new List<RejectedGoodsTransfer>();
    }

    public class ReturnFulfillTask
    {
        public ICollection<GoodsModels> GoodsModels { get; set; } = new List<GoodsModels>();
        public ICollection<RejectedProcurementTransfer> RejectedProcurementTransfer { get; set; } = new List<RejectedProcurementTransfer>();
    }

    public class ReturnFulfillTransferTask
    {
        public ICollection<GoodsModels> GoodsModels { get; set; } = new List<GoodsModels>();
        public ICollection<string> RejectedProcurementTransfer { get; set; } = new List<string>();
    }


    public class ProcurementsSubtaskModel
    {
        public int Id { get; set; }
        public int TaskID { get; set; }
        public int GoodTypeID { get; set; }
        public int Location { get; set; }
        public int Quantity { get; set; }
        public int RemainingQuantity { get; set; }
    }


    public class ProcurementsSubtaskModelForUpdate
    {
        public int Id { get; set; }
        public int GoodTypeID { get; set; }
        public int Location { get; set; }
        public int Quantity { get; set; }
        public int RemainingQuantity { get; set; }
    }


    public class TasksEntities_TransferModel
    {
        public int Id { get; set; }
        public int TaskID { get; set; }
        public int GoodID { get; set; }
        public string serialNumber { get; set; } = default!;
        public int FromLocation { get; set; }
        public int ToLocation { get; set; }

        public TaskTypesStatus TaskStatus { get; set; }
    }

    public class TransferSubtaskModelForUpdate
    {
        public int Id { get; set; }
        public int GoodID { get; set; }
        public string serialNumber { get; set; } = default!;
        public int FromLocation { get; set; }
        public int ToLocation { get; set; }

        public TaskTypesStatus TaskStatus { get; set; }
    }

    ///// Creation Models ////

    public class CreateTasksModel
    {
        public TaskTypes TaskType { get; set; }
        public int userID { get; set; }
        public string Description { get; set; } = string.Empty;
    }


    public class UpdateTasksModel
    {
        public TaskTypesStatus TaskStatus { get; set; }
        public string Description { get; set; } = string.Empty;
    }


    public class CreateProcurementModel
    {
        public int UserID { get; set; }
        public string Description { get; set; }
        public ICollection<GoodsOrder> GoodsOrder { get; set; } = new List<GoodsOrder>();
    }



    public class CreateTransferModel
    {
        public int UserID { get; set; }
        public string? Description { get; set; }
        public GoodsTransfer? GoodsTransfer { get; set; }

    }


    ///// Other stuff



    public class GoodsOrder
    {
        public int GoodTypeID { get; set; }
        public int Location { get; set; }
        public int Quantity { get; set; }
    }

    public class GoodsTransfer
    {
        public ICollection<int> GoodID { get; set; }
        public int ToLocation { get; set; }

    }

    public class FulfillGoodsTransfer
    {

        public string serialNumber { get; set; } = default!;

    }

    public class RejectedGoodsTransfer
    {
        public int GoodID { get; set; }
        public string serialNumber { get; set; } = default!;
        public int FromLocation { get; set; }
        public int ToLocation { get; set; }
        public string Reason { get; set; } = String.Empty;
    }

    public class RejectedProcurementTransfer
    {
        public int Location { get; set; }
        public int Supplier { get; set; }
        public int subTaskId { get; set; }
        public string SerialNumber { get; set; } = String.Empty;
        public string Reason { get; set; } = String.Empty;
    }

}
