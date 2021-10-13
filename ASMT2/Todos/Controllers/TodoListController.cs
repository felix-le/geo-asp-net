using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todos.Controllers
{
  public class TodoListController : Controller
  {
    public IActionResult Index()
    {

      return View();
    }
  }
}
