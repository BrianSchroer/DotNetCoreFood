using DotNetCoreFood.Models;
using DotNetCoreFood.Pages.Restaurants;
using DotNetCoreFood.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SparkyTestHelpers.AspNetMvc.Core;
using SparkyTestHelpers.Moq;

namespace DotNetCoreFood.UnitTests
{
    /// <summary>
    /// <see cref="EditModel"/> tests.
    /// </summary>
    [TestClass]
    public class EditModelTests
    {
        private Mock<IRestaurantData> _mockRestaurantData;
        private EditModel _editModel;
        private PageModelTester<EditModel> _pageModelTester; 

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRestaurantData = new Mock<IRestaurantData>();
            _editModel = new EditModel(_mockRestaurantData.Object);
            _pageModelTester = new PageModelTester<EditModel>(_editModel);
        }

        [TestMethod]
        public void EditModel_OnGet_should_call_RestaurantData_Get()
        {
            _editModel.OnGet(3);
            _mockRestaurantData.VerifyOneCallTo(x => x.Get(3));
        }

        [TestMethod]
        public void EditModel_OnGet_should_return_Page_with_Restaurant_when_ID_is_found()
        {
            var testRestaurant = new Restaurant();
            _mockRestaurantData.Setup(x => x.Get(Any.Int)).Returns(testRestaurant);

            _pageModelTester
                .Action(x => () => x.OnGet(7))
                .ExpectingModel(m => Assert.AreSame(testRestaurant, m.Restaurant))
                .TestPage();
        }

        [TestMethod]
        public void EditModel_OnGet_should_redirect_to_Home_Index_when_ID_is_not_found()
        {
            _mockRestaurantData.Setup(x => x.Get(Any.Int)).Returns(default(Restaurant));

            _pageModelTester
                .Action(x => () => x.OnGet(666))
                .TestRedirectToAction("Index", "Home");
        }

        [TestMethod]
        public void EditModel_OnPost_should_redirect_to_Details_when_ModelState_IsValid()
        {
            _editModel.Restaurant = new Restaurant { Id = 15 };

            _pageModelTester
                .Action(x => x.OnPost)
                .WhenModelStateIsValidEquals(true)
                .TestRedirectToAction("Details", "Home", new { id = 15 });
        }

        [TestMethod]
        public void EditModel_OnPost_should_redisplay_page_when_ModelState_IsValid_is_false()
        {
            _editModel.Restaurant = new Restaurant { Id = 15 };

            _pageModelTester
                .Action(x => x.OnPost)
                .WhenModelStateIsValidEquals(false)
                .TestPage();
        }
    }
}