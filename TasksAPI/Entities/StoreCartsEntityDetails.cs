using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksAPI.Entities
{
    public class StoreCartsEntityDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("CartId")]
        public StoreCartsEntity? StoreCartsEntity { get; set; }
        public int CartId { get; set; }
        [Required]
        public int OperationType { get; set; } // 1- Sale , 2 - Return , 3 - Others

        public int GoodId { get; set; }
        [Required]
        public Decimal Price { get; set; }

        internal string? InternalNotes { get; set; }

        [NotMapped]
        public string[]? Notes
        {
            get { return InternalNotes == null ? null : JsonConvert.DeserializeObject<string[]>(InternalNotes); }
            set { InternalNotes = JsonConvert.SerializeObject(value); }
        }
    }
}
