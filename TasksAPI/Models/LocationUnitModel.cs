using TasksAPI.Entities;

namespace TasksAPI.Models
{
    public class LocationUnitModel : BaseModel
    {
        public int Id { get; set; }
        public int LocationTypeID { get; set; }
        public string Adress { get; set; } = default!;
        public string Description { get; set; } = default!;
    }

    public class LocationUnitForCreate
    {
        public int LocationTypeID { get; set; }
        public string Adress { get; set; } = default!;
        public string Description { get; set; } = default!;
    }

    public class LocationUnitForUpdate
    {
        public int LocationTypeID { get; set; }
        public string Adress { get; set; } = default!;
        public string Description { get; set; } = default!;
    }


}
