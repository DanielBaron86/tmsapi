using System.Text.Json.Serialization;

namespace TasksAPI.Models
{
    public class GoodsTypesModel : BaseModel
    {
        public int Id { get; set; }

        public int GoodModelId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public GoodBaseTypeModel? GoodModelBaseType { get; set; }

    }


    public class CreateGoodsTypesModel
    {   
        [JsonRequired] 
        public int GoodModelId { get; set; }
        [JsonRequired] 
        public string Name { get; set; } = default!;
        [JsonRequired] 
        public string Description { get; set; } = default!;

    }

    public class UpdateGoodsTypesModel
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

    }

    public class GoodModelBase
    {
    }
}
