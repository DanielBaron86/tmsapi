using System.Text.Json.Serialization;

namespace TasksAPI.Models
{
    
    public class GoodBaseTypeModel : BaseModel
    {
       
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public string Manufacturer { get; set; } = default!;
        public int InventoryKey { get; set; }
        
    }

    public class CreateGoodBaseTypeModel
    {
        [JsonRequired]
        public string Description { get; set; } = default!;
        [JsonRequired]
        public string Manufacturer { get; set; } = default!;
        [JsonRequired]
        public int InventoryKey { get; set; }
    }
    
    public class UpdateGoodBaseTypeModel
    {
        [JsonRequired]
        public string Description { get; set; } = default!;
        [JsonRequired]
        public string Manufacturer { get; set; } = default!;
    }

    public class GoodsModels : BaseModel
    {
        public int Id { get; set; }
        public int GoodModelId { get; set; }
        public Decimal Price { get; set; }
        public string SerialNumber { get; set; } = default!;
        public int LocationId { get; set; }
        public int Quantity { get; set; }

        public GoodsStatus Status { get; set; }
        public GoodsTypesModel? GoodsTypes { get; set; }
        public LocationUnitModel? LocationTypesInstances { get; set; }
    }


    public class FulfillModel
    {
        [JsonRequired]
        public int Supplier { get; set; }
        [JsonRequired]
        public int SubTaskId { get; set; }
        [JsonRequired]
        public int UserId { get; set; }
        public ICollection<FulfillGoodsModels>? FulfillGoodsModels { get; set; } = new List<FulfillGoodsModels>();
    }

    public class TransferModel
    {
        [JsonRequired]
        public int SubTaskId { get; set; }
        [JsonRequired]
        public int UserId { get; set; }
        [JsonRequired]
        public int GoodModelId { get; set; }
        public int Quantity { get; set; }
        public ICollection<FulfillGoodsModels>? FulfillGoodsModels { get; set; } = new List<FulfillGoodsModels>();
    }

    public class FulfillGoodsModels
    {   
        [JsonRequired]
        public Decimal Price { get; set; }
        [JsonRequired]
        public string SerialNumber { get; set; } = default!;
        public int Quantity { get; set; }
    }

    public class FulfillTransferTask
    {
        [JsonRequired]
        public int UserId { get; set; }
        public ICollection<string>? fulfillGoodsTransfer { get; set; }
    }

    public class CreateGoodsModels
    {
    
        [JsonRequired]
        public int GoodModelId { get; set; }
        [JsonRequired]
        public Decimal Price { get; set; }
        [JsonRequired]
        public string SerialNumber { get; set; } = default!;
        [JsonRequired]
        public int LocationId { get; set; }
        public int Quantity { get; set; }
        public GoodsStatus Status { get; set; } = GoodsStatus.AVAILABLE;
    }

    public class UpdateGoodsModels
    {
        [JsonRequired]
        public int GoodModelId { get; set; }
        [JsonRequired]
        public Decimal Price { get; set; }
        [JsonRequired]
        public string SerialNumber { get; set; } = default!;
        [JsonRequired]
        public int LocationId { get; set; }
        public int Quantity { get; set; }
        [JsonRequired]
        public GoodsStatus Status { get; set; }
    }

    public struct ReturnGoods
    {
        [JsonRequired]
        public int ClerkId { get; set; }
        [JsonRequired]
        public int ReturnLocation { get; set; }
        public int Quantity { get; set; }
        [JsonRequired]
        public ICollection<int> GoodId { get; set; }
    }

    public class SellGoods
    {
        [JsonRequired]
        public int ClerkId { get; set; }
        [JsonRequired]
        public int StoreLocation { get; set; }
        [JsonRequired]
        public int GoodId { get; set; }
        public int Quantity { get; set; }
        [JsonRequired]
        public Decimal Price { get; set; }
    }
    public class CreateSellGoods
    {
        [JsonRequired]
        public int AccountId { get; set; }
        [JsonRequired]
        public int GoodId { get; set; }
        [JsonRequired]
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
        [JsonRequired]
        public GoodsStatus Status { get; set; }
    }
}
