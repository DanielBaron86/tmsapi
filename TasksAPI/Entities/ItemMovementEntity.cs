using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksAPI.Entities
{
    public class ItemMovementEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int goodId { get; set; }

        public int FromLocation { get; set; }
        public int ToLocation { get; set; }

        public int FromStatus { get; set; }
        public int ToStatus { get; set; }

        public int UserId { get; set; }
    }
}
