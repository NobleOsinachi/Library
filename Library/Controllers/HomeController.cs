using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.ViewModels.Catalog;
using Library.Services;
using Library.ViewModels.CheckOut;
using Library.Data;

namespace Library.Controllers
{

    

    public class HomeController : Controller
    {
               
        private readonly IPatron _patron;
        
        public HomeController(IPatron patron)
        {
            _patron = patron;
        }



        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View(new CheckOutModel());
        }


        [HttpPost]
        public IActionResult Privacy(CheckOutModel model)
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
