using System.Collections.Generic;
using System.Linq;
using Library.Data;
using Library.ViewModels.Branch;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BranchController : Controller
    {
        private readonly ILibraryBranch _branch;
        public BranchController(ILibraryBranch branch)
        {
            _branch = branch;
        }
        public IActionResult Index()
        {
            var branches = _branch.GetAll()
            .Select(b => new BranchDetailModel(b)
            {
                NumberOfAssets = _branch.GetAssets(b.Id).Count(),
                NumberOfPatrons = _branch.GetPatrons(b.Id).Count(),
  //////////////IsOpen = _branch.IsBranchOpen(b.Id),
});
            BranchIndexModel model = new BranchIndexModel() { Branches = branches };
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            Models.LibraryBranch branch = _branch.GetById(id);//.LibraryBranches.ToList();
            if (branch == null)
                return RedirectToAction("Index");
            BranchDetailModel model = new BranchDetailModel(branch)
            {
                NumberOfAssets = _branch.GetAssets(branch.Id).Count(),
                NumberOfPatrons = _branch.GetPatrons(branch.Id).Count(),
                TotalAssetValue = _branch.GetAssets(branch.Id).Sum(c => c.Cost),
                HoursOpen = _branch.GetBranchHours(branch.Id),
            };
            return View(model);
        }

    }
}