using AutoMapper;
using DotNetCoreFood.Controllers;
using DotNetCoreFood.Models;
using DotNetCoreFood.Services;
using DotNetCoreFood.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SparkyTestHelpers.AspNetCore.Mvc;
using SparkyTestHelpers.Moq;
using System.Linq;

namespace DotNetCoreFood.UnitTests
{
    /// <summary>
    /// <see cref="HomeController"/> tests.
    /// </summary>
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<IRestaurantData> _mockRestaurantData;
        private Mock<IGreeter> _mockGreeter;
        private HomeController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Reset();
            AutoMapperInitializer.Initialize();
            Mapper.AssertConfigurationIsValid();

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

            ViewResult viewResult =
                ControllerActionTester.ForAction(_controller.Index)
                .ExpectingViewName(null)
                .ExpectingModelType<HomeIndexViewModel>()
                .Test<ViewResult>();

            var model = (HomeIndexViewModel)viewResult.Model;
            Assert.AreEqual("Test message", model.CurrentMessage);
            Assert.AreSame(restaurants, model.Restaurants);
        }

        [TestMethod]
        public void Home_Details_should_return_expected_view_and_model_when_id_is_found()
        {
            var restaurant = new Restaurant { Id = 1, Cuisine = CuisineType.Italian, Name = "TestName" };
            _mockRestaurantData.Setup(x => x.Get(1)).Returns(restaurant);

            ViewResult viewResult =
                ControllerActionTester.ForAction(() => _controller.Details(1))
                .ExpectingViewName(null)
                .ExpectingModelType<Restaurant>()
                .Test<ViewResult>();

            Assert.AreSame(restaurant, viewResult.Model);
        }

        [TestMethod]
        public void Home_Details_should_redirect_to_Index_when_id_is_not_found()
        {
            _mockRestaurantData.Setup(x => x.Get(1)).Returns((Restaurant)null);

            RedirectToActionResult result =
                ControllerActionTester.ForAction(() => _controller.Details(1))
                .TestRedirectToAction("Index");
        }

        [TestMethod]
        public void Home_Create_get_should_return_expected_view()
        {
            ControllerActionTester.ForAction(_controller.Create)
                .ExpectingViewName(null)
                .Test<ViewResult>();
        }

        [TestMethod]
        public void Home_Create_post_should_redirect_to_Details_when_ModelState_IsValid()
        {
            ModelStateTestHelper.SetModelStateIsValid(_controller, true);
            _mockRestaurantData.Setup(x => x.Add(Any.InstanceOf<Restaurant>())).Returns(new Restaurant { Id = 123 });

            RedirectToRouteResult result =
                ControllerActionTester.ForAction(() => _controller.Create(new RestaurantEditModel()))
                .TestRedirectToRoute("/Home/Details/123");
        }

        [TestMethod]
        public void Home_Create_post_should_return_same_view_when_ModelState_IsValid_is_false()
        {
            ModelStateTestHelper.SetModelStateIsValid(_controller, false);

            ViewResult viewResult =
                ControllerActionTester.ForAction(() => _controller.Create(new RestaurantEditModel()))
                .ExpectingViewName(null)
                .Test<ViewResult>();
        }
    }
}