namespace TasksAPI.Models
{
    public sealed record UserResource(int Id, string Username, string Email, string FirstName, string LastName, int UserTypeId, DateTime? CreatedDate, DateTime? UpdatedDate);
    public sealed record UserResourceForUpdate(string Username, string Email, string FirstName, string LastName, int UserTypeId);
    public sealed record ClientResourceForUpdate(string Username, string Email, string FirstName, string LastName);

}
