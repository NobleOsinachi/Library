using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.Services;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class BranchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}