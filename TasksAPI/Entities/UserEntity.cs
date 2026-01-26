using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Runtime.CompilerServices;
using TasksAPI.Models;

namespace TasksAPI.Entities
{
    public sealed class UserEntity : BaseEntity
    {

        public UserEntity() {
            Status = (int)DbEntityStatus.ACTIVE;
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
        public RefreshTokenEntity RefreshTokenEntity  { get; set; } = default!;

        public ICollection<TasksEntities> TasksEntities { get; set; } = default!;
        public ICollection<CashRegisterEntitySessions> CashRegisterEntitySessions { get; set; } = default!;
        }
    
    public class RefreshTokenEntity :  BaseEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Token { get; set; } = default!;
        [Required]
        public bool Revoked { get; set; } = default!;
        [Required]
        public int userId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public UserEntity UserEntity { get; set; }
    }
}