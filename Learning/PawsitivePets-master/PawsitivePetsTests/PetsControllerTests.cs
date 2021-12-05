using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PawsitivePets.Controllers;
using PawsitivePets.Data;
using PawsitivePets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsitivePetsTests
{
  [TestClass]
  public class PetsControllerTests
  {
    // class level vars for use in all tests
    private ApplicationDbContext _context;
    PetsController controller;
    List<Pet> pets = new List<Pet>();
    Pet _pet = new Pet();
    // use this special start up method that runs automatically before every test to do global arrange
    [TestInitialize]
    public void TestInitialize()
    {
      // set up and create in-memory db
      var options = new DbContextOptionsBuilder<ApplicationDbContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString())
          .Options;
      _context = new ApplicationDbContext(options);

      // populate in-memory db with 1 category and 3 pets
      var category = new Category
      {
        CategoryId = 1000,
        Name = "Mock Category"
      };
      _context.Categories.Add(category);
      _pet = new Pet
      {
        PetId = 28,
        Name = "Mock update pet",
        Age = 10,
        Price = 100,
        CategoryId = 1000,
        Category = category,
        Photo = null,
      };
      pets.Add(new Pet
      {
        PetId = 28,
        Name = "Lassie",
        Age = 10,
        Price = 100,
        CategoryId = 1000,
        Category = category,
        Photo = null,
      });

      pets.Add(new Pet
      {
        PetId = 16,
        Name = "Albert",
        Age = 2,
        Price = 1000,
        CategoryId = 1000,
        Category = category,
        Photo = null,
      });

      pets.Add(new Pet
      {
        PetId = 49,
        Name = "Sophie",
        Age = 1,
        Price = 80,
        CategoryId = 1000,
        Category = category,
        Photo = null,
      });

      foreach (var pet in pets)
      {
        _context.Pets.Add(pet);
      }
      _context.SaveChanges();

      // create controller obj & pass the db
      controller = new PetsController(_context);
    }

    [TestMethod]
    public void IndexLoadsIndexView()
    {
      // act
      var result = (ViewResult)controller.Index().Result;

      // assert
      Assert.AreEqual("Index", result.ViewName);
    }

    [TestMethod]
    public void IndexReturnsPets()
    {
      // act
      var result = (ViewResult)controller.Index().Result;

      // assert - is the data that gets returned the same as our list of pets in the in-memory db?
      CollectionAssert.AreEqual(pets.OrderBy(p => p.Category.Name).ThenBy(p => p.Name).ToList(), (List<Pet>)result.Model);
    }

    // Details Tests
    [TestMethod]
    public void DetailsNullIdLoads404()
    {
      // act
      var result = (ViewResult)controller.Details(null).Result;

      // assert
      Assert.AreEqual("404", result.ViewName);
    }

    [TestMethod]
    public void DetailsInvalidIdLoads404()
    {
      // act
      var result = (ViewResult)controller.Details(7).Result;

      // assert
      Assert.AreEqual("404", result.ViewName);
    }

    [TestMethod]
    public void DetailsValidIdLoadsDetailsView()
    {
      // act
      var result = (ViewResult)controller.Details(16).Result;

      // assert
      Assert.AreEqual("Details", result.ViewName);
    }
      //Edit (POST)
    // if id != pet.PetId
    [TestMethod]
    public void DetailsValidIdReturnsPet()
    {
      // act
      var result = (ViewResult)controller.Details(16).Result;

      // assert
      Assert.AreEqual(pets[1], result.Model);
    }

    //Edit (POST)

    [TestMethod]
    public void DbUpdateConcurrencyException_EditToDoIdIsNotEqual()
    {
      controller.ModelState.AddModelError("key", "error message");

      // act

      var result = (ViewResult)controller.Edit(28, pets[0], null).Result;

      // assert
      Assert.AreEqual(pets[0], (Pet)result.Model);
    }

  }
}
