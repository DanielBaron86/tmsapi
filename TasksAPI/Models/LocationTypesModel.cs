namespace TasksAPI.Models
{
    public class LocationTypesModel : BaseModel
    {
        public int Id { get; set; }
        public LocationTypesList LocationType { get; set; }
        public string? Description { get; set; }
    }


    public class CreateLocationTypesModel
    {
        public LocationTypesList LocationType { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class EditLocationTypesModel
    {
        public string Description { get; set; } = string.Empty;
    }
}
