using AutoMapper;
using DotNetCoreFood.Startup;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCoreFood.UnitTests
{
    /// <summary>
    /// Base class for test classes using AutoMapper
    /// </summary>
    public class TestsUsingAutomapper
    {
        [TestInitialize]
        public void InitializeAutoMapper()
        {
            Mapper.Reset();
            AutoMapperConfig.Initialize();
            Mapper.AssertConfigurationIsValid();
        }
    }
}
