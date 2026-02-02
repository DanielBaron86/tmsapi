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
                .ForMember(dest => dest.serialNumber, opt => opt.MapFrom(src => src.SerialNumber.ToUpper()));

            CreateMap<UpdateGoodsModels, GoodsTypesInstances>()
                            .ForMember(dest => dest.serialNumber, opt => opt.MapFrom(src => src.SerialNumber.ToUpper()));

            CreateMap<GoodsModels, UpdateGoodsModels>();
            CreateMap<GoodsTypesEntity, GoodsTypesModel>();
            CreateMap<CreateGoodsTypesModel, GoodsTypesEntity>();
            CreateMap<UpdateGoodsTypesModel, GoodsTypesEntity>();
            CreateMap<CreateSellGoods, AccountsGoodsEntity>();
            CreateMap<GoodModelBaseTypeEntity, GoodBaseTypeModel>();
            CreateMap<UpdateGoodBaseTypeModel, GoodModelBaseTypeEntity>();
            CreateMap<CreateGoodBaseTypeModel, GoodModelBaseTypeEntity>();
            CreateMap<CreateGoodBaseTypeModel, GoodBaseTypeModel>();

        }
    }
}
