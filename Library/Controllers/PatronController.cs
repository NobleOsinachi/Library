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
            Patron patron = _patron.GetById(id);
            if (patron == null)
            {
                return RedirectToAction("Index");
            }

            PatronDetailModel model = new PatronDetailModel(patron)
            {
                //LastName = patron.LastName,
                //FirstName = patron.FirstName,
                //Address = patron.Address,
                //HomeLibraryBranch = patron.HomeLibraryBranch.Name,
                //MemberSince = patron.LibraryCard.Created,
                //OverdueFees = patron.LibraryCard.Fees,
                //LibraryCardId = patron.LibraryCard.Id,
                //TelephoneNumber = patron.TelephoneNumber,
                AssetsCheckedOut = _patron.GetCheckOuts(id).ToList() ?? new List<CheckOut>(),
                Holds = _patron.GetHolds(id).ToList() ?? new List<Hold>(),
            };
            return View(model);
        }

        //[HttpPost]
        //public IActionResult Detail(Patron patron)
        //{
        //    //Color[] cols = new Color[] { Color.Red, Color.White, Color.Orange, Color.Yellow, Color.Blue, Color.Green, Color.Black, Color.Indigo, Color.Violet, Color.Sienna, Color.PaleTurquoise };

        //    _patron.Update(patron);
        //    return View();
        //}





    }
}