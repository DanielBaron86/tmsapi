using AutoMapper;
using TasksAPI.Entities;
using TasksAPI.Models;

namespace TasksAPI.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<Entities.UserEntity, Models.RegisterResource>();
            CreateMap<Entities.UserEntity, Models.UserResource>();

            CreateMap<Entities.Accounts, Models.UserResource>();

            CreateMap<Models.UserResource, Entities.UserEntity>();
            CreateMap<Models.UserResourceForUpdate, Entities.UserEntity>();

            CreateMap<Models.ClientResourceForUpdate, Entities.Accounts>();
            CreateMap<RefreshTokenEntity, RefreshToken>();
            CreateMap<Models.RefreshTokenForUpdate, Entities.RefreshTokenEntity>();

        }
    }
}
