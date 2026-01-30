using System.Text.Json.Serialization;

namespace TasksAPI.Models
{
    public class LocationTypesModel : BaseModel
    {
        public int Id { get; set; }
        [JsonRequired]
        public LocationTypesList LocationType { get; set; }
        public string? Description { get; set; }
    }


    public class CreateLocationTypesModel
    {
        [JsonRequired]
        public LocationTypesList LocationType { get; set; }
        [JsonRequired]
        public string Description { get; set; } = string.Empty;
    }

    public class EditLocationTypesModel
    {
        [JsonRequired]
        public string Description { get; set; } = string.Empty;
    }
}
