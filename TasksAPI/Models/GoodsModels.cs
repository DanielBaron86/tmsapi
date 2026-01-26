using System.Text.Json.Serialization;

namespace TasksAPI.Models
{
    
    public class GoodBaseTypeModel : BaseModel
    {
       
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public string Manufacturer { get; set; } = default!;
        
    }
    public class GoodsModels : BaseModel
    {

        public int Id { get; set; }
        public int GoodModelId { get; set; }
        public Decimal Price { get; set; }
        public string SerialNumber { get; set; } = default!;
        public int LocationId { get; set; }

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
        public int SubTaskId { get; set; }
        public int UserId { get; set; }
        public int GoodModelId { get; set; }
        public ICollection<FulfillGoodsModels>? FulfillGoodsModels { get; set; } = new List<FulfillGoodsModels>();
    }

    public class FulfillGoodsModels
    {
        public Decimal Price { get; set; }
        public string SerialNumber { get; set; } = default!;
    }

    public class FulfillTransferTask
    {
        public int UserId { get; set; }
        public ICollection<string>? fulfillGoodsTransfer { get; set; }
    }

    public class CreateGoodsModels
    {

        public int GoodModelId { get; set; }
        public Decimal Price { get; set; }
        public string SerialNumber { get; set; } = default!;
        public int LocationId { get; set; }
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
        [JsonRequired]
        public GoodsStatus Status { get; set; }
    }

    public struct ReturnGoods
    {
        public int ClerkId { get; set; }
        public int ReturnLocation { get; set; }
        public ICollection<int> GoodId { get; set; }
    }

    public class SellGoods
    {
        public int ClerkId { get; set; }
        public int StoreLocation { get; set; }
        public int GoodId { get; set; }
        public Decimal Price { get; set; }
    }
    public class CreateSellGoods
    {
        public int AccountId { get; set; }
        public int GoodId { get; set; }
        public Decimal Price { get; set; }

        public GoodsStatus Status { get; set; }
    }
}
