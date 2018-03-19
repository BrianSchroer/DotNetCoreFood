using DotNetCoreFood.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SparkyTestHelpers.Scenarios;
using SparkyTestHelpers.DataAnnotations;

namespace DotNetCoreFood.UnitTests
{
    /// <summary>
    /// <see cref="Restaurant"/> validation tests.
    /// </summary>
    [TestClass]
    public class RestaurantValidationTests
    {
        private Restaurant _validRestaurant;

        [TestInitialize]
        public void TestInitialize()
        {
            _validRestaurant = new Restaurant { Name = "TestName", Cuisine = CuisineType.Italian };
        }

        [TestMethod]
        public void Restaurant_with_valid_properties_should_return_no_errors()
        {
            Validation.For(_validRestaurant).ShouldReturn.NoErrors();
        }

        [TestMethod]
        public void Restaurant_Name_should_be_required()
        {
            ForTest.Scenarios(string.Empty, null).TestEach(name =>
                Validation
                    .For(_validRestaurant)
                    .When(x => x.Name = name)
                    .ShouldReturn.RequiredErrorFor(x => x.Name));
        }

        [TestMethod]
        public void Restaurant_Name_has_MaxLength_of_80()
        {
            Validation
                .For(_validRestaurant)
                .When(x => x.Name = new string('x', 81)).ShouldReturn.MaxLengthErrorFor(x => x.Name)
                .When(x => x.Name = "good name").ShouldReturn.NoErrors();
        }
    }
}
