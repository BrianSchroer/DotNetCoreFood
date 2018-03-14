using AutoMapper;
using SparkyTools.AutoMapper;
using DotNetCoreFood.ViewModels;
using DotNetCoreFood.Models;

namespace DotNetCoreFood.Startup
{
    public static class AutoMapperConfig
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
