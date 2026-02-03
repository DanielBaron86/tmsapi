using System.Text.Json.Serialization;

namespace TasksAPI.Models
{
    public class LocationUnitModel : BaseModel
    {
        public int Id { get; set; }
        [JsonRequired]
        public int LocationTypeId { get; set; }
        public string Address { get; set; } = default!;
        public string Description { get; set; } = default!;
        public LocationTypesModel LocationTypesEntity { get; set; }
    }

    public class LocationUnitForCreate
    {
        [JsonRequired]
        public int LocationTypeId { get; set; }
        public string Address { get; set; } = default!;
        public string Description { get; set; } = default!;
    }

    public class LocationUnitForUpdate
    {
        [JsonRequired]
        public int LocationTypeId { get; set; }
        public string Address { get; set; } = default!;
        public string Description { get; set; } = default!;
    }


}
