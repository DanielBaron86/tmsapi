using Microsoft.AspNetCore.JsonPatch;
using TasksAPI.Models;
using TasksAPI.Services;

namespace TasksAPI.Interfaces
{
    public interface IUserService
    {
        Task<UserResource> Register(RegisterResource resource, CancellationToken cancellationToken);
        Task<LoginResponse> Login(LoginResource resource, CancellationToken cancellationToken);

        Task<UserResource> UpdateUser(int userID, UserResourceForUpdate userResource, CancellationToken cancellationToken);
        Task<UserResource> PatchUser(int userID, JsonPatchDocument patchUser, CancellationToken cancellationToken);
        Task<UserResource> GetUserById(int userID);

        Task< (IEnumerable<UserResource>, PaginationMetadata)> GetUsers(int pageNumber, int pageSize);
        Task< (IEnumerable<UserResource>, PaginationMetadata)>GetUsersWithConditions(QueryFilters queryFilters);
        Task<bool> DeleteUser(int userID, CancellationToken cancellationToken);
        Task<string> RefreshToken(RefreshResource resource, CancellationToken cancellationToken);
        Task<RefreshToken> GetRefreshToken(RefreshResource resource, CancellationToken cancellationToken);
    }
}
