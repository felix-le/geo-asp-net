using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thusday_lession1.Controllers
{
  public class CategoriesController : Controller
  {
    public IActionResult Index()
    {
      return View();
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
  }
}
