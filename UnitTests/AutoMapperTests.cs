using DotNetCoreFood.Models;
using DotNetCoreFood.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SparkyTestHelpers.AutoMapper;
using SparkyTestHelpers.Mapping;

namespace DotNetCoreFood.UnitTests
{
    /// <summary>
    /// <see cref="AutoMapper"/> tests.
    /// </summary>
    [TestClass]
    public class AutoMapperTests : TestsUsingAutomapper
    {
        [TestMethod]
        public void ResturantEditModel_should_map_to_Restaurant()
        {
            MapTester.ForMap<RestaurantEditModel, Restaurant>()
                .IgnoringMember(dest => dest.Id)
                .AssertAutoMappedRandomValues();
        }
    }
}
