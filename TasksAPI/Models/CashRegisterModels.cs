using System.Text.Json.Serialization;
namespace TasksAPI.Models
{
    public struct CreateCashRegisterEntity
    {
        [JsonRequired]
        public int LocationId { get; set; }
        public string[]? Notes { get; set; }
    }

    public class CashRegisterEntityModel : BaseModel
    {
        public int Id { get; set; }
        [JsonRequired]
        public int LocationId { get; set; }
        public string[]? Notes { get; set; }
    }

    public struct CreateCashRegisterSessionsEntityModel
    {
        [JsonRequired]
        public int AssignedClerk { get; set; }
        [JsonRequired]
        public int CashRegisterId { get; set; }
        public string[]? Notes { get; set; }
    }

    public class UpdateSessionsEntityModel 
    {
        [JsonRequired]
        public int SessionStatus { get; set; } // 1- Open , 2- Closed
        [JsonRequired]
        public int AssignedClerk { get; set; }
        [JsonRequired]
        public int CashRegisterId { get; set; }        
        public DateTime? CloseHour { get; set; }

        public string[]? Notes { get; set; }
    }

    public class CashRegisterEntitySessionsModel : BaseModel {
        [JsonRequired]
        public int SessionStatus { get; set; } // 1- Open , 2- Closed
        [JsonRequired]
        public int AssignedClerk { get; set; }
        [JsonRequired]
        public int CashRegisterId { get; set; }

        public DateTime? OpenHour { get; set; }
        public DateTime? CloseHour { get; set; }

        public string[]? Notes { get; set; }
    }

    public struct CreateRegisterOperationsModel
    {

        [JsonRequired]
        public int OperationType { get; set; } // 1- Sale , 2 - Return
        [JsonRequired]
        public int GoodId { get; set; }
        [JsonRequired]
        public Decimal Price { get; set; }
        public string[]? Notes { get; set; }
    }

    public class StoreCartsEntityDetailsModel : BaseModel
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int OperationType { get; set; } // 1- Sale , 2 - Return
        public int GoodId { get; set; }
        public Decimal Price { get; set; }
        public string[]? Notes { get; set; }
    }


    public struct CreateGoodsCartEntityModel
    {
        public int SessionId { get; set; }
        public int ClientId { get; set; }
        public int Status { get; set; }  // 1 - Open, 2 - Paid
    }
    public class StoreCartsEntityModel : BaseModel
    {        
        public int Id { get; set; }
        public int ClerktId { get; set; }
        public int StoreLocation { get; set; }
        public int ClientId { get; set; }
        public int SessionId { get; set; }
        public int Status { get; set; }  // 1 - Open, 2 - Paid
        public Decimal Total { get; set; }
        public Decimal Paid { get; set; }
        public Decimal Remaining { get; set; }
        
    }

    public class StoreCartsEntityModelWithDetails : StoreCartsEntityModel
    {
        public ICollection<StoreCartsEntityDetailsModel> StoreCartsEntityDetails { get; set; } = new List<StoreCartsEntityDetailsModel>();
    }

    public struct CreateNewCart { 
        [JsonRequired]
        public int ClerkId { get; set; } 
        [JsonRequired]
        public int ClientId { get; set; }
        [JsonRequired]
        public int StoreLocation { get; set; }
    }

}
