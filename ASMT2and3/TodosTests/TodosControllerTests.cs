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
  }
}
