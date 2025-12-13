namespace TasksAPI.Models
{
    public class GoodsTypesModel : BaseModel
    {
        public int Id { get; set; }

        public int GoodModelId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

    }


    public class CeateGoodsTypesModel
    {
        public int GoodModelId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

    }

    public class UpdateGoodsTypesModel
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

    }
}
