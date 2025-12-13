using AutoMapper;
using TasksAPI.Entities;
using TasksAPI.Models;

namespace TasksAPI.Profiles
{
    public class CashRegisterProfiles : Profile
    {
        public CashRegisterProfiles() {
            
            CreateMap<CashRegisterEntity, CashRegisterEntityModel>();
            CreateMap<CashRegisterEntityModel,CashRegisterEntity>();

            CreateMap<CashRegisterEntitySessions, CashRegisterEntity_SessionsModel>();
            CreateMap<CashRegisterEntity_SessionsModel, CashRegisterEntitySessions>();

            

            CreateMap<StoreCartsEntityDetails, StoreCartsEntity_DetailsModel>();
            CreateMap<StoreCartsEntity_DetailsModel, StoreCartsEntityDetails>();



            CreateMap<StoreCartsEntity, StoreCartsEntityModel>();
            CreateMap<StoreCartsEntity, StoreCartsEntityModelWithDetails>();
            CreateMap<StoreCartsEntityModel, StoreCartsEntity> ();
            
        }
    }
}
