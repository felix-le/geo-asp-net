using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thusday_lession1.Models;

namespace Thusday_lession1.Controllers
{
  public class CategoriesController : Controller
  {
    public IActionResult Index()
    {
      // use the Category model to generate a list of 10 mock Category objects

      var categories = new List<Category>();
      for (var i = 1; i < 11; i++)
      {
        categories.Add(new Category { CategoryId = i, Name = "Category " + i.ToString() });
      }
      return View(categories);
    }

    // Add a new method that's call when user click a category from the list displayed
    // method accepts a string value in the url called "category
    public IActionResult Browse(string category)
    {
      if (category == null)
      {
        return RedirectToAction("Index");
      }
      //Store the input parameter inther viewBag to display in the view
      ViewBag.category = category;
      return View();
    }

    // /Categories/Create
    public IActionResult Create()
    {
      return View();
    }
  }
}
