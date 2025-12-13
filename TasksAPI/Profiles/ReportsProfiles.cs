using AutoMapper;
using Newtonsoft.Json.Linq;
using TasksAPI.Entities;
using TasksAPI.Models;

namespace TasksAPI.Profiles
{
    public class ReportsProfiles : Profile
    {
        public ReportsProfiles() {
            CreateMap<ReportsEntities, ReportsEntitiesModel>();
            CreateMap<ReportsEntitiesModel, ReportsEntities>();
        }
    }
}