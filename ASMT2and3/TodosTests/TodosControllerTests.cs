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
using Todos.Models;

namespace TodosTests
{
  [TestClass]
  public class TodosControllerTests
  {
    // Class level vars for use in all tests

    private ApplicationDbContext _context;
    TodosController controller;
    Todo _todo = new Todo();
    List<Todo> todos = new List<Todo>();

    // use this special start up method that runs automatically before every test to do global arrange
    [TestInitialize]
    public void TestInitialize()
    {
      //setup and create in-memory DB
      var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

      _context = new ApplicationDbContext(options);

      // populate in-memory db with 1 todo list and 3 todos

      // create todo list based on Todo List Model
      var todoList = new TodoListModel
      {

        Id = 1000,
        Name = "Mock todo List"
      };

      // add to db with Todo list models
      _context.TodoListModels.Add(todoList);

      // Create To do - based on todos from the line 22

      _todo = new Todo
      {
        Id = 10001,
        TodoName = "Mock todo 1",
        TodoListModelId = 1000,
        DaysTime = 10,
        //add parent ref to Todo List
        TodoList = todoList
      };

      todos.Add(new Todo
      {
        Id = 10001,
        TodoName = "Mock todo 1",
        TodoListModelId = 1000,
        DaysTime = 10,
        //add parent ref to Todo List
        TodoList = todoList
      });

      todos.Add(new Todo
      {
        Id = 10002,
        TodoName = "Mock todo 2",
        TodoListModelId = 1000,
        DaysTime = 8,
        //add parent ref to Todo List
        TodoList = todoList
      });

      todos.Add(new Todo
      {
        Id = 10003,
        TodoName = "Mock todo 3",
        TodoListModelId = 1000,
        DaysTime = 9,
        //add parent ref to Todo List
        TodoList = todoList
      });

      // add each todo

      foreach (var todo in todos)
      {
        _context.Todos.Add(todo);
      }
      _context.SaveChanges();


      // Create controller obj & pass the db
      controller = new TodosController(_context);


    }

    [TestMethod]
    public void IndexLoadsIndexView()
    {
      // Arrange The controller was declared as a global var
      //var controller = new TodosController(_context);

      // Act
      var result = (ViewResult)controller.Index().Result;
      // assert

      Assert.AreEqual("Index", result.ViewName);
    }

    [TestMethod]
    public void IndexReturnsTodos()
    {
      //act
      var result = (ViewResult)controller.Index().Result;

      //assert - check the data returned the same as our todo list

      //Collection Assert > checking a list

      // check order if has any errors
      CollectionAssert.AreEqual(todos, (List<Todo>)result.Model);

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
      var result = (ViewResult)controller.Details(100).Result;

      // assert
      Assert.AreEqual("404", result.ViewName);
    }


    // POST EDIT

    // id != todo.Id
    [TestMethod]
    public void EditToDoIdIsNotEqual()
    {
      // act
      var result = (ViewResult)controller.Edit(100, todos[0]).Result;

      // assert
      Assert.AreEqual("Error", result.ViewName);
    }

    // if ModelState.IsValid is invalid
    [TestMethod]
    public void EditIsNotValid()
    {
      controller.ModelState.AddModelError("key", "error message");

      // act

      var result = (ViewResult)controller.Edit(10001, _todo).Result;

      // assert
      Assert.AreEqual(_todo, (Todo)result.Model);
    }

    // if edit suggest fully => compare the result and data
    [TestMethod]
    public void EditFullyCompare()
    {
      _todo.TodoName = "updated";

      controller.ModelState.AddModelError("key", "error message");

      // act

      var result = (ViewResult)controller.Edit(10001, _todo).Result;

      // assert
      Assert.AreEqual(_todo, (Todo)result.Model);
    }

    /*
     * You need some more tests here and for your first test you called the Details method not the Edit method.  X
     * You also need to test when the model is invalid that the view returned is called Edit, and also add "Edit" as a string parameter to the Edit method.  
     * You also need a test when the model is valid to ensure that the updated pet object is saved to the db.
     */
    [TestMethod]
    public void EditShowDataCorrectValues()
    {
      _todo.TodoName = "";

      controller.ModelState.AddModelError("key", "error message");

      // act

      var result = (ViewResult)controller.Edit(10001, _todo).Result;

      // assert
      Assert.AreEqual("Edit", result.ViewName);
    }
  }
}
