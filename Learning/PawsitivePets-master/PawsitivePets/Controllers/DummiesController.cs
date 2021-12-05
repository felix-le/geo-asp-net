using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawsitivePets.Controllers
{
    public class DummiesController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "We put a string in here";
            return View("Index");
        }
    }
}
