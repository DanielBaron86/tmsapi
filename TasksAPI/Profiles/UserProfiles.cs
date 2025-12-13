using AutoMapper;
using TasksAPI.Models;

namespace TasksAPI.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<Entities.User, Models.RegisterResource>();
            CreateMap<Entities.User, Models.UserResource>();

            CreateMap<Entities.Accounts, Models.UserResource>();

            CreateMap<Models.UserResource, Entities.User>();
            CreateMap<Models.UserResourceForUpdate, Entities.User>();

            CreateMap<Models.ClientResourceForUpdate, Entities.Accounts>();

        }
    }
}
