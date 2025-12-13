using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksAPI.Entities
{
    public class CashRegisterEntitySessions : BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int SessionStatus { get; set; } // 1- Open , 2- Closed

        [ForeignKey("AssignedClerk")]
        public User? User { get; set; }
        public int AssignedClerk { get; set; }
        [ForeignKey("CashRegisterID")]
        public CashRegisterEntity? CashRegisterEntity { get; set; }
        public int CashRegisterID { get; set; }
        [Required]
        public DateTime OpenHour { get; set; }
        public DateTime CloseHour { get; set; }

        internal string? InternalNotes { get; set; }

        [NotMapped]
        public string[]? Notes
        {
            get { return InternalNotes == null ? null : JsonConvert.DeserializeObject<string[]>(InternalNotes); }
            set { InternalNotes = JsonConvert.SerializeObject(value); }
        }

    }
}
