using AutoMapper;
using SparkyTools.AutoMapper;
using DotNetCoreFood.ViewModels;
using DotNetCoreFood.Models;

namespace DotNetCoreFood
{
    public static class AutoMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<RestaurantEditModel, Restaurant>()
                    .IgnoreMember(dest => dest.Id);
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
