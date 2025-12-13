using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TasksAPI.Models;

namespace TasksAPI.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationUnitModel>> GetLocations();
        Task<IEnumerable<LocationTypesModel>> GetLocationTypess();

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
