using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksAPI.Entities
{
    public class CashRegisterEntity : BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("LocationID")]
        public LocationTypesInstances? LocationTypesInstances { get; set; }
        public int LocationID { get; set; }

        internal string? InternalNotes { get; set; } = string.Empty;
        
        public ICollection<CashRegisterEntitySessions>? CashRegisterEntitySessions {  get; set; }

        [NotMapped]
        public string[]? Notes
        {
            get { return InternalNotes == null ? null : JsonConvert.DeserializeObject<string[]>(InternalNotes); }
            set { InternalNotes = JsonConvert.SerializeObject(value); }
        }

    }
}
