namespace TasksAPI.Models
{
    public class GoodsModels : BaseModel
    {

        public int Id { get; set; }
        public int GoodModelId { get; set; }
        public Decimal Price { get; set; }
        public string serialNumber { get; set; } = default!;
        public int LocationId { get; set; }

        public GoodsStatus Status { get; set; }
    }


    public class FulfillModel
    {
        public int Supplier { get; set; }
        public int subTaskId { get; set; }
        public int userId { get; set; }
        public ICollection<FulfillGoodsModels> FulfillGoodsModels { get; set; } = new List<FulfillGoodsModels>();
    }

    public class TransferModel
    {
        public int subTaskId { get; set; }
        public int userId { get; set; }
        public int GoodModelId { get; set; }
        public ICollection<FulfillGoodsModels> FulfillGoodsModels { get; set; } = new List<FulfillGoodsModels>();
    }

    public class FulfillGoodsModels
    {
        public Decimal Price { get; set; }
        public string serialNumber { get; set; } = default!;
    }

    public class FulfillTransferTask
    {
        public int userID { get; set; }
        public ICollection<string>? fulfillGoodsTransfer { get; set; }
    }

    public class CreateGoodsModels
    {

        public int GoodModelId { get; set; }
        public Decimal Price { get; set; }
        public string serialNumber { get; set; } = default!;
        public int LocationId { get; set; }
        public GoodsStatus Status { get; set; } = GoodsStatus.AVAILABLE;
    }

    public class UpdateGoodsModels
    {
        public int GoodModelId { get; set; }
        public Decimal Price { get; set; }
        public string serialNumber { get; set; } = default!;
        public int LocationId { get; set; }
        public GoodsStatus Status { get; set; }
    }

    public struct ReturnGoods
    {
        public int clerkId { get; set; }
        public int returnLocation { get; set; }
        public ICollection<int> goodID { get; set; }
    }

    public class SellGoods
    {
        public int clerkId { get; set; }
        public int storeLocation { get; set; }
        public int goodID { get; set; }
        public Decimal price { get; set; }
    }
    public class CreateSellGoods
    {
        public int AccountId { get; set; }
        public int GoodId { get; set; }
        public Decimal price { get; set; }

        public GoodsStatus Status { get; set; }
    }
}
