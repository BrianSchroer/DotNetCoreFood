using DotNetCoreFood.Controllers;
using DotNetCoreFood.Models;
using DotNetCoreFood.Services;
using DotNetCoreFood.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SparkyTestHelpers.AspNetCore;
using SparkyTestHelpers.Moq;
using System.Linq;

namespace DotNetCoreFood.UnitTests
{
    /// <summary>
    /// <see cref="HomeController"/> tests.
    /// </summary>
    [TestClass]
    public class HomeControllerTests : TestsUsingAutomapper
    {
        private Mock<IRestaurantData> _mockRestaurantData;
        private Mock<IGreeter> _mockGreeter;
        private HomeController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRestaurantData = new Mock<IRestaurantData>();
            _mockGreeter = new Mock<IGreeter>();
            _controller = new HomeController(_mockRestaurantData.Object, _mockGreeter.Object);

            ModelStateTestHelper.SetModelStateIsValid(_controller, true);
        }

        [TestMethod]
        public void Home_Index_should_call_RestaurantData_GetAll()
        {
            _controller.Index();
            _mockRestaurantData.VerifyOneCallTo(x => x.GetAll());
        }

        [TestMethod]
        public void Home_Index_should_call_Greeter_GetMessageOfTheDay()
        {
            _controller.Index();
            _mockGreeter.VerifyOneCallTo(x => x.GetMessageOfTheDay());
        }

        [TestMethod]
        public void Home_Index_should_return_expected_view_and_model()
        {
            var restaurants = Enumerable.Range(1, 3).Select(i => new Restaurant { Id = i, Name = $"Name{i}" }).ToArray();

            _mockRestaurantData.Setup(x => x.GetAll()).Returns(restaurants);
            _mockGreeter.Setup(x => x.GetMessageOfTheDay()).Returns("Test message");

            ControllerActionTester
                .ForAction(_controller.Index)
                .ExpectingModel<HomeIndexViewModel>(model =>
                {
                    Assert.AreEqual("Test message", model.CurrentMessage);
                    Assert.AreSame(restaurants, model.Restaurants);
                })
                .TestViewResult();
        }

        [TestMethod]
        public void Home_Details_should_return_expected_view_and_model_when_id_is_found()
        {
            var restaurant = new Restaurant { Id = 1, Cuisine = CuisineType.Italian, Name = "TestName" };
            _mockRestaurantData.Setup(x => x.Get(1)).Returns(restaurant);

            ControllerActionTester
                .ForAction(() => _controller.Details(1))
                .ExpectingModel<Restaurant>(model => Assert.AreSame(restaurant, model))
                .TestViewResult();
        }

        [TestMethod]
        public void Home_Details_should_redirect_to_Index_when_id_is_not_found()
        {
            _mockRestaurantData.Setup(x => x.Get(1)).Returns((Restaurant)null);

            ControllerActionTester
                .ForAction(() => _controller.Details(1))
                .TestRedirectToAction("Index");
        }

        [TestMethod]
        public void Home_Create_get_should_return_expected_view()
        {
            ControllerActionTester.ForAction(_controller.Create).TestResult<ViewResult>();
        }

        [TestMethod]
        public void Home_Create_post_should_redirect_to_Details_when_ModelState_IsValid()
        {
            ModelStateTestHelper.SetModelStateIsValid(_controller, true);
            _mockRestaurantData.Setup(x => x.Add(Any.InstanceOf<Restaurant>())).Returns(new Restaurant { Id = 123 });

            ControllerActionTester
                .ForAction(() => _controller.Create(new RestaurantEditModel()))
                .TestRedirectToRoute("/Home/Details/123");
        }

        [TestMethod]
        public void Home_Create_post_should_return_same_view_when_ModelState_IsValid_is_false()
        {
            ModelStateTestHelper.SetModelStateIsValid(_controller, false);

            ControllerActionTester
                .ForAction(() => _controller.Create(new RestaurantEditModel()))
                .TestResult<ViewResult>();
        }
    }
}