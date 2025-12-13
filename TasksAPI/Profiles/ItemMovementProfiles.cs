using AutoMapper;
using TasksAPI.Entities;
using TasksAPI.Models;

namespace TasksAPI.Profiles
{
    public class ItemMovementProfiles : Profile
    {
        public ItemMovementProfiles()
        {
            CreateMap<CreateItemMovementModel, ItemMovementEntity>();
            CreateMap<ItemMovementEntity, ItemMovementModel>();
        }
    }
}
