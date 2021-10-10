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
  }
}
