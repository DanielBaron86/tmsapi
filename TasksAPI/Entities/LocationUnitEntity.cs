using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksAPI.Entities
{
    public class LocationTypesInstances : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int LocationTypeID { get; set; }

        [Required]
        public string Adress { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;

        public LocationTypes LocationTypes { get; set; } = default!;

        public ICollection<GoodsTypesInstances>? GoodsTypesInstances { get; }
        public ICollection<TasksEntitiesTransfer>? TasksEntitiesTransfer { get; set; }
        public ICollection<TasksEntitiesProcurements>? TasksEntitiesProcurements { get; set; }
        public ICollection<CashRegisterEntity>? CashRegisterEntity { get; set; }

    }
}
