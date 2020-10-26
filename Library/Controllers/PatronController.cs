using System.Linq;
using System.Collections.Generic;
using Library.Data;
using Library.Models;
using Library.ViewModels.Patron;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class PatronController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IPatron _patron;

        public PatronController(LibraryContext context, IPatron patron)
        {
            _context = context;
            _patron = patron;
        }
        public IActionResult Index()
        {
            IEnumerable<PatronDetailModel> patronModel = _patron.GetAll()
                .Select(p => new PatronDetailModel(p) { });
            PatronIndexModel model = new PatronIndexModel() { Patrons = patronModel, };
            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            Patron pat = _patron.GetById(id);

            return View(pat);
        }

        [HttpPost]
        public IActionResult Detail(Patron patron)
        {
            //Color[] cols = new Color[] { Color.Red, Color.White, Color.Orange, Color.Yellow, Color.Blue, Color.Green, Color.Black, Color.Indigo, Color.Violet, Color.Sienna, Color.PaleTurquoise };

            _patron.Update(patron);
            return View();
        }



    }
}