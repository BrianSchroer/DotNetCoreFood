using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using SparkyTestHelpers.AutoMapper;
using SparkyTestHelpers.Mapping;
using DotNetCoreFood.ViewModels;
using DotNetCoreFood.Models;

namespace DotNetCoreFood.UnitTests
{
    /// <summary>
    /// <see cref="AutoMapper"/> tests.
    /// </summary>
    [TestClass]
    public class AutoMapperTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Reset();
            AutoMapperInitializer.Initialize();
            Mapper.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void ResturantEditModel_should_map_to_Restaurant()
        {
            MapTester.ForMap<RestaurantEditModel, Restaurant>()
                .IgnoringMember(dest => dest.Id)
                .AssertAutoMappedRandomValues();
        }
    }
}
