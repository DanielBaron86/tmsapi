using AutoMapper;
using TasksAPI.Entities;
using TasksAPI.Models;

namespace TasksAPI.Profiles
{
    public class LocationsProfiles : Profile
    {
        public LocationsProfiles()
        {
            CreateMap<LocationTypesInstances, LocationUnitModel>();
            CreateMap<LocationUnitModel, LocationTypesInstances>();
            CreateMap<LocationUnitForCreate, LocationTypesInstances>();
            CreateMap<LocationUnitForUpdate, LocationTypesInstances>();

            CreateMap<LocationTypesModel, LocationTypes>();
            CreateMap<LocationTypes, LocationTypesModel>();
            CreateMap<CreateLocationTypesModel, LocationTypes>();
            CreateMap<EditLocationTypesModel, LocationTypes>();
            

        }
    }
}
