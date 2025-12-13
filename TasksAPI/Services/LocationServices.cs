using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TasksAPI.DataBaseContext;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;

namespace TasksAPI.Services
{
    public class LocationServices : ILocationService
    {
        private readonly DatabaseConnectContext _DBContext;
        private readonly IMapper _mapper;

        public LocationServices(DatabaseConnectContext context, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _DBContext = context ?? throw new ArgumentNullException(nameof(context)); ;
        }


        public async Task<IEnumerable<LocationUnitModel>> GetLocations()
        {
            var locations = await _DBContext.LocationTypesInstances.ToListAsync();
            return _mapper.Map<IEnumerable<LocationUnitModel>>(locations);
        }

        public async Task<LocationUnitModel> GetLocationById(int locationID)
        {
            var location = await _DBContext.LocationTypesInstances.FirstOrDefaultAsync(l => l.Id == locationID) ??throw new ArgumentException("Location not found");
            return _mapper.Map<LocationUnitModel>(location);
        }

        public async Task<LocationUnitModel> UpdateLocation(int locationID, LocationUnitForUpdate location)
        {
            var locationToBeUpdated = _DBContext.LocationTypesInstances.FirstOrDefault(l => l.Id == locationID) ??throw new ArgumentException("Location not found");

            _mapper.Map(location, locationToBeUpdated);
            await _DBContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<LocationUnitModel>(locationToBeUpdated);

        }

        public async Task<LocationUnitModel> CreateLocation(LocationUnitForCreate locationUnitModel)
        {
            var location = _mapper.Map<LocationTypesInstances>(locationUnitModel);

            await _DBContext.LocationTypesInstances.AddAsync(location, CancellationToken.None);
            await _DBContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<LocationUnitModel>(location);
        }


        public async Task<bool> DeleteLocation(int locationID)
        {
            var location = _DBContext.LocationTypesInstances.FirstOrDefault(l => l.Id == locationID) ??throw new ArgumentException("Location not found");

            _DBContext.Remove(location);
            await _DBContext.SaveChangesAsync(CancellationToken.None);
            return true;

        }

        public async Task<LocationUnitModel> PatchLocation(int locationID, JsonPatchDocument location)
        {
            var locationToPatch = await _DBContext.LocationTypesInstances.FirstOrDefaultAsync(l => l.Id == locationID) ??throw new ArgumentException("Location not found");

            location.ApplyTo(locationToPatch);
            await _DBContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<LocationUnitModel>(locationToPatch);
        }

        public async Task<IEnumerable<LocationTypesModel>> GetLocationTypess()
        {
            return _mapper.Map<IEnumerable<LocationTypesModel>>(await _DBContext.LocationEntity.ToListAsync());
            
        }

        public async Task<LocationTypesModel> GetLocationTypeById(int locationID)
        {
            var location = await _DBContext.LocationEntity.FirstOrDefaultAsync(l => l.Id == locationID) ?? throw new ArgumentException("Location Type not found");
            return _mapper.Map<LocationTypesModel>(location);
        }

        public async Task<LocationTypesModel> CreateLocationType(CreateLocationTypesModel locationUnitModel)
        {
            var location = _mapper.Map<LocationTypes>(locationUnitModel);

            await _DBContext.LocationEntity.AddAsync(location, CancellationToken.None);
            await _DBContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<LocationTypesModel>(location);
        }

        public async Task<LocationTypesModel> UpdateLocationType(int locationID, EditLocationTypesModel location)
        {
            var locationToBeUpdated = _DBContext.LocationEntity.FirstOrDefault(l => l.Id == locationID) ?? throw new ArgumentException("Location type not found");

            _mapper.Map(location, locationToBeUpdated);
            await _DBContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<LocationTypesModel>(locationToBeUpdated);
            
        }

        public async Task<int> DeleteLocationType(int locationID)
        {
            var location = _DBContext.LocationEntity.FirstOrDefault(l => l.Id == locationID) ?? throw new ArgumentException("Location not found");
            var count = _DBContext.LocationTypesInstances.Where( t=> t.LocationTypeID == locationID).Count();

            if (count <= 0)
            {
                _DBContext.Remove(location);
                return  await _DBContext.SaveChangesAsync(CancellationToken.None);
            }
            else
            {
                throw new ArgumentException($"Location {locationID} has existing instances");
            }

            
            
        }
    }
}
