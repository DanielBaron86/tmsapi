using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TasksAPI.Models;

namespace TasksAPI.Entities
{
    public class UserTypes : BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserTypeId { get; set; }
        public string Description { get; set; } = default!;

        public ICollection<User> Users { get; set; } = default!;
        public ICollection<Accounts> Accounts { get; set; } = default!;
    }
}
