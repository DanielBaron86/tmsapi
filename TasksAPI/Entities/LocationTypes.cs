using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TasksAPI.Entities
{
    public class LocationTypes : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int LocationType { get; set; }
        public string? Description { get; set; }

        public ICollection<LocationTypesInstances>? LocationTypesInstances { get; set; }
    }
}
