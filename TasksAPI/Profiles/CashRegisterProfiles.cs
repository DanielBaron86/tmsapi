using AutoMapper;
using TasksAPI.Entities;
using TasksAPI.Models;

namespace TasksAPI.Profiles
{
    public class CashRegisterProfiles : Profile
    {
        public CashRegisterProfiles()
        {

            CreateMap<CashRegisterEntity, CashRegisterEntityModel>();
            CreateMap<CashRegisterEntityModel, CashRegisterEntity>();
            CreateMap<CashRegisterEntitySessions, CashRegisterEntitySessionsModel>();
            CreateMap<CashRegisterEntitySessionsModel, CashRegisterEntitySessions>();
            CreateMap<StoreCartsEntityDetails, StoreCartsEntityDetailsModel>();
            CreateMap<StoreCartsEntityDetailsModel, StoreCartsEntityDetails>();
            CreateMap<StoreCartsEntity, StoreCartsEntityModel>();
            CreateMap<StoreCartsEntity, StoreCartsEntityModelWithDetails>();
            CreateMap<StoreCartsEntityModel, StoreCartsEntity>();
            CreateMap<CreateCashRegisterEntity, CashRegisterEntity>();

        }
    }
}
