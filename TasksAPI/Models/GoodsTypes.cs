using System.Text.Json.Serialization;

namespace TasksAPI.Models
{
    public class GoodsTypesModel : BaseModel
    {
        public int Id { get; set; }

        public int GoodBaseId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public GoodBaseTypeModel? GoodModelBaseTypeEntity { get; set; }
        public int InventoryKey { get; set; }

    }


    public class CreateGoodsTypesModel
    {
        [JsonRequired]
        public int GoodBaseId { get; set; }
        [JsonRequired]
        public string Name { get; set; } = default!;
        [JsonRequired]
        public string Description { get; set; } = default!;
        [JsonRequired]
        public int InventoryKey { get; set; }

    }

    public class UpdateGoodsTypesModel
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

    }
}
