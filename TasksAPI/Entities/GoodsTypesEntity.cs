using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TasksAPI.Models;

namespace TasksAPI.Entities
{


    public class GoodsTypesEntity : BaseEntity
    {


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public int GoodBaseId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int InventoryKey { get; set; }

        public ICollection<GoodsTypesInstances> GoodsTypesInstances { get; set; } = default!;
        public ICollection<TasksEntitiesProcurements>? TasksEntitiesProcurements { get; set; } = default!;
        public GoodModelBaseTypeEntity GoodModelBaseTypeEntity { get; set; }
    }


    public class GoodModelBaseTypeEntity : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public string Manufacturer { get; set; } = default!;
        public ICollection<GoodsTypesEntity> GoodsTypesList { get; set; } = default!;
    }
}
