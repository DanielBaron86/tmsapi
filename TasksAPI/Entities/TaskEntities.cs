using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TasksAPI.Models;

namespace TasksAPI.Entities
{
    public class TasksEntities : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public TaskTypes TaskType { get; set; }
        [Required]
        public TaskTypesStatus TaskStatus { get; set; } = TaskTypesStatus.PENDING;
        [Required]
        public int CreatorId { get; set; }
        [Required]
        public string Description { get; set; } =string.Empty;

        public ICollection<TasksEntitiesProcurements>? TasksEntitiesProcurements { get; set; }
        public ICollection<TasksEntitiesTransfer>? TasksEntitiesTransferList { get; set; }
        public UserEntity? Creator { get; set; }
        public string? UserName {get; set;}

    }

    public class TasksEntitiesProcurements : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("TaskID")]
        public TasksEntities? TasksEntities { get; set; }
        public int TaskID { get; set; }

        public GoodsTypesEntity? GoodsTypes { get; set; }
  
        public LocationTypesInstances? LocationTypesInstances { get; set; }
        [Required]
        public int Location { get; set; }
        [Required]
        public int GoodTypeID { get; set; }
        [Required]
        public string GoodType { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int RemainingQuantity { get; set; } = 0;
    }

    public class TasksEntitiesTransfer : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("TaskID")]
        public TasksEntities? TasksEntities { get; set; }
        public int TaskID { get; set; }

        [ForeignKey("GoodID")]
        public GoodsTypesInstances? GoodsTypesInstances { get; set; }
        public int GoodID { get; set; }
        public string serialNumber { get; set; } = default!;

        public LocationTypesInstances LocationTypesInstances { set; get; } = default!;
        [Required]
        public int FromLocation { get; set; }
        [Required]
        public string FromLocationName { get; set; }
        [Required]
        public int ToLocation { get; set; }
        [Required]
        public string ToLocationName { get; set; }
        [Required]
        public TaskTypesStatus TaskStatus { get; set; } = TaskTypesStatus.PENDING;
    }
}


