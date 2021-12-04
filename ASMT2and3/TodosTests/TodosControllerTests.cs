using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todos.Controllers;
using Todos.Data;

namespace TodosTests
{
  [TestClass]
  public class TodosControllerTests
  {
    // Class level vars for use in all tests

    private ApplicationDbContext _context;

    // use this special start up method that runs automatically before every test to do global arrange

    [TestInitialize]
    public void TestInitialize()
    {
      //setup and create in-memory DB
      var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

      _context = new ApplicationDbContext(options);
    }



    [TestMethod]
    public void IndexLoadsIndexView()
    {
      // Arrange
      var controller = new TodosController(_context);

      // Act
      var result = (ViewResult)controller.Index().Result;
      // assert

      Assert.AreEqual("Index", result.ViewName);
    }
  }
}
