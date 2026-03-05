using Microsoft.AspNetCore.JsonPatch;
using TasksAPI.Models;
using TasksAPI.Services;

namespace TasksAPI.Interfaces
{
    public interface IClientServices
    {
        Task<(IEnumerable<UserResource>, PaginationMetadata)> GetClients(int pageNumber, int pageSize);
        Task<(IEnumerable<UserResource>, PaginationMetadata)> GetClientsWithConditions(QueryFilters queryFilters);
        Task<UserResource> Register(ClientResource resource, CancellationToken cancellationToken);
        Task<string> Login(LoginResource resource, CancellationToken cancellationToken);

        Task<UserResource> UpdateClient(int userID, ClientResourceForUpdate userResource, CancellationToken cancellationToken);
        Task<UserResource> PatchClient(int userID, JsonPatchDocument patchUser, CancellationToken cancellationToken);
        Task<UserResource> GetClientById(int userID);
        Task<bool> DeleteClient(int userID, CancellationToken cancellationToken);
    }
}
