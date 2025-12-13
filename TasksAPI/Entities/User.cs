using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using TasksAPI.Models;

namespace TasksAPI.Entities
{
    public sealed class User : BaseEntity
    {

        public User() {
            Status = (int)DBEntityStatus.ACTIVE;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        public string FirstName { get; set; } = default!;
        [Required]
        public string LastName { get; set; } = default!;
        [Required]
        public int UserTypeId { get; set; }

        public int Status { get; set; }
        
        public string PasswordSalt { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;

        public UserTypes UserTypes { get; set; } = default!;

        public ICollection<TasksEntities> TasksEntities { get; set; } = default!;
        public ICollection<CashRegisterEntitySessions> CashRegisterEntitySessions { get; set; } = default!;

    }
}
