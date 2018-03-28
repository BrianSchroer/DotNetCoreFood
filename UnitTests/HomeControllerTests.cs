using DotNetCoreFood.Controllers;
using DotNetCoreFood.Models;
using DotNetCoreFood.Services;
using DotNetCoreFood.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SparkyTestHelpers.AspNetMvc.Core;
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
        private HomeController _controller;
        private ControllerTester<HomeController> _controllerTester;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRestaurantData = new Mock<IRestaurantData>();
            _controller = new HomeController(_mockRestaurantData.Object);
            _controllerTester = new ControllerTester<HomeController>(_controller);
        }

        [TestMethod]
        public void Home_Index_should_call_RestaurantData_GetAll()
        {
            _controller.Index();
            _mockRestaurantData.VerifyOneCallTo(x => x.GetAll());
        }

        [TestMethod]
        public void Home_Index_should_return_expected_view_and_model()
        {
            var restaurants = Enumerable.Range(1, 3).Select(i => new Restaurant { Id = i, Name = $"Name{i}" }).ToArray();

            _mockRestaurantData.Setup(x => x.GetAll()).Returns(restaurants);

           _controllerTester 
                .Action(x => x.Index)
                .ExpectingModel<HomeIndexViewModel>(model =>
                {
                    Assert.AreSame(restaurants, model.Restaurants);
                })
                .TestView();
        }

        [TestMethod]
        public void Home_Details_should_return_expected_view_and_model_when_id_is_found()
        {
            var restaurant = new Restaurant { Id = 1, Cuisine = CuisineType.Italian, Name = "TestName" };
            _mockRestaurantData.Setup(x => x.Get(1)).Returns(restaurant);

           _controllerTester 
                .Action(x => () => _controller.Details(1))
                .ExpectingModel<Restaurant>(model => Assert.AreSame(restaurant, model))
                .TestView();
        }

        [TestMethod]
        public void Home_Details_should_redirect_to_Index_when_id_is_not_found()
        {
            _mockRestaurantData.Setup(x => x.Get(1)).Returns((Restaurant)null);

            _controllerTester
                .Action(x => () => _controller.Details(1))
                .TestRedirectToAction("Index");
        }

        [TestMethod]
        public void Home_Create_get_should_return_expected_view()
        {
            _controllerTester.Action(x => x.Create).TestView();
        }

        [TestMethod]
        public void Home_Create_post_should_redirect_to_Details_when_ModelState_IsValid()
        {
            _mockRestaurantData.Setup(x => x.Add(Any.InstanceOf<Restaurant>())).Returns(new Restaurant { Id = 123 });

            _controllerTester
                .Action(x => () => x.Create(new RestaurantEditModel()))
                .WhenModelStateIsValidEquals(true)
                .TestRedirectToRoute("/Home/Details/123");
        }

        [TestMethod]
        public void Home_Create_post_should_return_same_view_when_ModelState_IsValid_is_false()
        {
            _controllerTester 
                .Action(x => () => x.Create(new RestaurantEditModel()))
                .WhenModelStateIsValidEquals(false)
                .TestResult<ViewResult>();
        }
    }
}