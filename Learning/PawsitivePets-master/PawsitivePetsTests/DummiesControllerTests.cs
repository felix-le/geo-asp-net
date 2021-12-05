using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PawsitivePets.Controllers;

namespace PawsitivePetsTests
{
    [TestClass]
    public class DummiesControllerTests
    {
        [TestMethod]
        public void IndexLoadsView()
        {
            // arrange - set up any objects / vars needed to call the method we want to test and set up the right conditions
            var controller = new DummiesController();

            // act - execute the method and retrieve a result
            // cast the IActionResult class returned from the method to its child class of ViewResult
            var result = (ViewResult)controller.Index();

            // assert - check if the result is as expected
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexShowsCorrectMessage()
        {
            // arrange - set up any objects / vars needed to call the method we want to test and set up the right conditions
            var controller = new DummiesController();

            // act - execute the method and retrieve a result
            // cast the IActionResult class returned from the method to its child class of ViewResult
            var result = (ViewResult)controller.Index();

            // assert
            Assert.AreEqual("We put a string in here", result.ViewData["Message"]);
        }
    }
}
