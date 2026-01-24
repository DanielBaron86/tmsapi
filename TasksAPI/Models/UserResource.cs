namespace TasksAPI.Models
{
    public sealed record UserResource(int Id, string Username, string Email, string FirstName, string LastName, int UserTypeId, DateTime? CreatedDate, DateTime? UpdatedDate);
    public sealed record UserResourceForUpdate(string Username, string Email, string FirstName, string LastName, int UserTypeId);
    public sealed record ClientResourceForUpdate(string Username, string Email, string FirstName, string LastName);
    public record LoginResponse(string Token, UserResource UserProfile, string refreshToken);
    
    public class RefreshToken :  BaseModel {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public bool Revoked;
        public int userId { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
    
    public class RefreshTokenForUpdate :  BaseModel{
        public string Token { get; set; } = null!;
        public bool Revoked;
        public int userId { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
    public sealed record RefreshResource(string oldToken, string refreshToken,int userId);
}
