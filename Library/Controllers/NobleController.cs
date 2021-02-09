using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Library.Data;
using Library.Models;
using Library.ViewModels.Patron;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class NobleController : Controller
    {
        [HttpGet] public ActionResult Index() { return View(); }
    }
}