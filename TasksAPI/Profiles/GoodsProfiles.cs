using AutoMapper;
using TasksAPI.Entities;
using TasksAPI.Models;

namespace TasksAPI.Profiles
{
    public class GoodsProfiles : Profile
    {
        public GoodsProfiles()
        {



            CreateMap<GoodsTypesInstances, GoodsModels>();
            CreateMap<GoodsModels, GoodsTypesInstances>();

            

            CreateMap<CreateGoodsModels, GoodsTypesInstances>()
                .ForMember(dest => dest.serialNumber, opt => opt.MapFrom(src => src.serialNumber.ToUpper()));

            CreateMap<UpdateGoodsModels, GoodsTypesInstances>()
                            .ForMember(dest => dest.serialNumber, opt => opt.MapFrom(src => src.serialNumber.ToUpper()));

            CreateMap<GoodsModels, UpdateGoodsModels>();

            CreateMap<GoodsTypes, GoodsTypesModel>();
            CreateMap<CeateGoodsTypesModel, GoodsTypes>();
            CreateMap<UpdateGoodsTypesModel, GoodsTypes>();
            

            CreateMap<CreateSellGoods, AccountsGoodsEntity>();

        }
    }
}
