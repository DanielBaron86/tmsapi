namespace TasksAPI.Models
{
    public sealed record RegisterResource(string Username, string Email, string Password, string FirstName, string LastName, int UserTypeId);
    public sealed record ClientResource(string Username, string Email, string Password, string FirstName, string LastName);

}
