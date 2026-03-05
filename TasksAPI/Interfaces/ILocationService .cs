using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TasksAPI.Models;
using TasksAPI.Services;

namespace TasksAPI.Interfaces
{
    public interface ILocationService
    {

        Task<(IEnumerable<LocationUnitModel>, PaginationMetadata)> GetLocations(int pageNumber, int pageSize);
        Task<(IEnumerable<LocationUnitModel>, PaginationMetadata)> GetLocationsWithConditions(QueryFilters queryFilters);

        Task<(IEnumerable<LocationTypesModel>, PaginationMetadata)> GetLocationTypess(int pageNumber, int pageSize);


        Task<LocationUnitModel> GetLocationById(int locationID);
        Task<LocationTypesModel> GetLocationTypeById(int locationID);

        Task<LocationUnitModel> CreateLocation(LocationUnitForCreate locationUnitModel);
        Task<LocationTypesModel> CreateLocationType(CreateLocationTypesModel locationUnitModel);


        Task<LocationUnitModel> UpdateLocation(int locationID, LocationUnitForUpdate location);
        Task<LocationTypesModel> UpdateLocationType(int locationID, EditLocationTypesModel location);


        Task<LocationUnitModel> PatchLocation(int locationID, JsonPatchDocument location);

        Task<bool> DeleteLocation(int locationID);
        Task<int> DeleteLocationType(int locationID);

    }
}
