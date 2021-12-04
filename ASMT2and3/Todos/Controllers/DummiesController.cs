using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todos.Controllers
{
  public class DummiesController : Controller
  {
    public IActionResult Index()
    {
      ViewData["Message"] = "Hello World";
      return View("Index");
    }
  }
}
