using DotNetCoreFood.Services;
using DotNetCoreFood.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SparkyTestHelpers.AspNetMvc.Core;
using SparkyTestHelpers.Moq;

namespace DotNetCoreFood.UnitTests
{
    /// <summary>
    /// <see cref="GreeterViewComponent"/> unit tests.
    /// </summary>
    [TestClass]
    public class GreeterViewComponentTests
    {
        private Mock<IGreeter> _mockGreeter;
        private GreeterViewComponent _component;
        private ViewComponentTester<GreeterViewComponent> _tester;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGreeter = new Mock<IGreeter>();
            _component = new GreeterViewComponent(_mockGreeter.Object);
            _tester = new ViewComponentTester<GreeterViewComponent>(_component);
        }

        [TestMethod]
        public void GreeterViewComponent_Invoke_should_call_Greeter_GetMessageOfTheDay()
        {
            _component.Invoke();
            _mockGreeter.VerifyOneCallTo(x => x.GetMessageOfTheDay());
        }

        [TestMethod]
        public void GreeterViewComponent_Invoke_should_return_greeting()
        {
            _mockGreeter.Setup(x => x.GetMessageOfTheDay()).Returns("Test greeting");

            _tester
                .Invocation(x => x.Invoke)
                .ExpectingViewName("Default")
                .ExpectingModel<string>(x => Assert.AreEqual("Test greeting", x))
                .TestView();
        }
    }
}
