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
            CreateMap<GoodsTypes, GoodsTypesModel>();
            CreateMap<CreateGoodsTypesModel, GoodsTypes>();
            CreateMap<UpdateGoodsTypesModel, GoodsTypes>();
            CreateMap<CreateSellGoods, AccountsGoodsEntity>();
            CreateMap<GoodModelBaseType, GoodBaseTypeModel>();
            CreateMap<UpdateGoodBaseTypeModel, GoodModelBaseType>();
            CreateMap<CreateGoodBaseTypeModel, GoodModelBaseType>();
            CreateMap<CreateGoodBaseTypeModel, GoodBaseTypeModel>();

        }
    }
}
