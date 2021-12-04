using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todos.Controllers;

namespace TodosTests
{
  [TestClass]
  public class DummiesControllerTests
  {
    [TestMethod]
    public void IndexLoadsView()
    {
      //Arrange - setup any objects / vars needed to call the method we want to test and setup the right conditions

      var controller = new DummiesController();

      //act - execute the method and retrieve a result
      //cast the IActionResult class returned from the method to its child class of ViewResult 
      var result = (ViewResult)controller.Index();

      // assert - check if the result is as expected

      Assert.AreEqual("Index", result.ViewName);

    }
    [TestMethod]
    public void IndexShowsCorrectMessage()
    {
      var controller = new DummiesController();

      //act - execute the method and retrieve a result
      //cast the IActionResult class returned from the method to its child class of ViewResult 
      var result = (ViewResult)controller.Index();

      // assert - check if the result is as expected
      Assert.AreEqual("Hello World", result.ViewData["Message"]);

    }
  }
}
