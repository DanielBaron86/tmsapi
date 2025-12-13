using Microsoft.AspNetCore.JsonPatch;
using TasksAPI.Models;

namespace TasksAPI.Interfaces
{
    public interface IUserService
    {
        Task<UserResource> Register(RegisterResource resource, CancellationToken cancellationToken);
        Task<string> Login(LoginResource resource, CancellationToken cancellationToken);

        Task<UserResource> UpdateUser(int userID, UserResourceForUpdate userResource, CancellationToken cancellationToken);
        Task<UserResource> PatchUser(int userID, JsonPatchDocument patchUser, CancellationToken cancellationToken);
        Task<UserResource> GetUserById(int userID);

        Task<IEnumerable<UserResource>> GetUsers();
        Task<bool> DeleteUser(int userID, CancellationToken cancellationToken);
    }
}
