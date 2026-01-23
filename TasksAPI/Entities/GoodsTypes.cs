using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TasksAPI.Models;

namespace TasksAPI.Entities
{


    public class GoodsTypes : BaseEntity
    {


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public int GoodModelId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        public ICollection<GoodsTypesInstances> GoodsTypesInstances { get; set; } = default!;
        public ICollection<TasksEntitiesProcurements>? TasksEntitiesProcurements { get; set; } = default!;
        public GoodModelBaseType GoodModelBaseType { get; set; }
    }


    public class GoodModelBaseType : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int GoodModelBaseTypeId { get; set; }
        public string Description { get; set; } = default!;
        public string Manufacturer { get; set; } = default!;
        public ICollection<GoodsTypes> GoodsTypesList { get; set; } = default!;
    }
}
