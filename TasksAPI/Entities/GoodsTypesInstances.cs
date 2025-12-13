using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TasksAPI.Entities
{
    public class GoodsTypesInstances : BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int GoodModelId { get; set; }
        [Required]
        public Decimal Price { get; set; }

        [ForeignKey("LocationId")]
        public LocationTypesInstances LocationTypesInstances { get; set; } = default!;
        [Required]
        public int LocationId { get; set; }
        [Required]
        public string serialNumber { get; set; } = default!;
        [Required]
        public int Status { get; set; }

        public GoodsTypes? GoodsTypes { get; set; }
        public TasksEntitiesTransfer? TasksEntitiesTransfer { get; }

    }
}
