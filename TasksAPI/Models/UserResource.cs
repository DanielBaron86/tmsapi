using System.Runtime.Serialization;
namespace TasksAPI.Models
{
    [DataContract]
    public sealed record UserResource(
        [property: DataMember] int Id,
        [property: DataMember] string Username,
        [property: DataMember] string Email,
        [property: DataMember] string FirstName,
        [property: DataMember] string LastName,
        [property: DataMember] int UserTypeId,
        [property: DataMember] DateTime? CreatedDate,
        [property: DataMember] DateTime? UpdatedDate);

    public sealed record UserResourceForUpdate(string Email, string FirstName, string LastName, int UserTypeId);
    public sealed record ClientResourceForUpdate(string Username, string Email, string FirstName, string LastName);
    public record LoginResponse(string Token, UserResource UserProfile, string refreshToken);

    public class RefreshToken : BaseModel
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public bool Revoked;
        public int UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class RefreshTokenForUpdate : BaseModel
    {
        public string Token { get; set; } = null!;
        public bool Revoked;
        public int UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
    public sealed record RefreshResource(string oldToken, string refreshToken, int userId);
}
