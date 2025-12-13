using System.ComponentModel.DataAnnotations.Schema;
using TasksAPI.Entities;

namespace TasksAPI.Models
{
    public struct CreateCashRegisterEntity
    {
        public int LocationID { get; set; }
        public string[]? Notes { get; set; }
    }

    public class CashRegisterEntityModel : BaseModel
    {
        public int Id { get; set; }
        public int LocationID { get; set; }
        public string[]? Notes { get; set; }
    }

    public struct CreateCashRegisterSessionsEntityModel
    {
        public int AssignedClerk { get; set; }
        public int CashRegisterID { get; set; }
        public string[] Notes { get; set; }
    }

    public class UpdateSessionsEntityModel 
    {

        public int SessionStatus { get; set; } // 1- Open , 2- Closed

        public int AssignedClerk { get; set; }

        public int CashRegisterID { get; set; }        
        public DateTime CloseHour { get; set; }

        public string[]? Notes { get; set; }
    }

    public class CashRegisterEntity_SessionsModel : BaseModel {

        public int SessionStatus { get; set; } // 1- Open , 2- Closed

        public int AssignedClerk { get; set; }

        public int CashRegisterID { get; set; }

        public DateTime OpenHour { get; set; }
        public DateTime CloseHour { get; set; }

        public string[]? Notes { get; set; }
    }

    public struct CreateRegisterOperationsModel
    {

        public int OperationType { get; set; } // 1- Sale , 2 - Return
        public int GoodId { get; set; }
        public Decimal Price { get; set; }
        public string[]? Notes { get; set; }
    }

    public class StoreCartsEntity_DetailsModel : BaseModel
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
        public int SessionID { get; set; }
        public int clientId { get; set; }
        public int Status { get; set; }  // 1 - Open, 2 - Paid
    }
    public class StoreCartsEntityModel : BaseModel
    {        
        public int Id { get; set; }
        public int clerktId { get; set; }
        public int storeLocation { get; set; }
        public int clientId { get; set; }
        public int SessionID { get; set; }
        public int Status { get; set; }  // 1 - Open, 2 - Paid
        public Decimal Total { get; set; }
        public Decimal Paid { get; set; }
        public Decimal Remaining { get; set; }
        
    }

    public class StoreCartsEntityModelWithDetails : StoreCartsEntityModel
    {
        public ICollection<StoreCartsEntity_DetailsModel> StoreCartsEntityDetails { get; set; } = new List<StoreCartsEntity_DetailsModel>();
    }

    public struct CreateNewCart { 
        public int clerkId { get; set; } 
        public int clientId { get; set; }
        public int storeLocation { get; set; }
    }

}
