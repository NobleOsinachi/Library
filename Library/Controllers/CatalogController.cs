using System.Collections.Generic;
using System.Linq;
using Library.Data;
using Library.Models;
using Library.ViewModels.Catalog;
using Library.ViewModels.CheckOut;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryAsset _assets { get; }
        private ICheckOut _CheckOuts { get; }
        public CatalogController(ILibraryAsset assets, ICheckOut CheckOuts)
        {
            _assets = assets;
            _CheckOuts = CheckOuts;
        }


        [AcceptVerbs("GET")]
        public IActionResult Index()
        {
            /**
             * We don't call the LibraryAssetService directly as that would tightly couple our code.
             * Rather, we take advantage of the dependency injection that .NET Core gives us.
             * To the ConfigureServices method in the Startup, add
             * services.AddScoped<ILibraryAsset, LibraryAssetService>();
             * This way, the LibraryAssetService will be injected into the controller anytime it 
             * requests our ILibraryAsset interface.
             */

            IEnumerable<LibraryAsset> assetModels = _assets.GetAll();

            /// <summary>
            /// Date when the branch was founded
            /// </summary>

            IEnumerable<AssetIndexListingModel> listingResult = assetModels
                    .Select(result => new AssetIndexListingModel(result, _assets)
                    {
                        AuthorOrDirector = _assets.GetAuthorOrDirector(result.Id),
                        DeweyCallNumber = _assets.GetDeweyIndex(result.Id),
                        Type = _assets.GetType(result.Id),
                    });

            AssetIndexModel model = new AssetIndexModel(listingResult);

            //var model = new AssetIndexModel(){Assets = _assets.GetAll().Select(result => new AssetIndexListingModel(result, _assets))};

            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            LibraryAsset asset = _assets.GetById(id);
            if (asset != null)
            {
                IEnumerable<AssetHoldModel> currentHolds = _CheckOuts.GetCurrentHolds(id)
                    .Select(c => new AssetHoldModel(c.Id, _CheckOuts));


                ///move this to an overloaded  constructor
                AssetDetailModel model = new AssetDetailModel()
                {
                    Description = asset.Description,
                    AssetId = id,
                    Title = asset.Title,
                    Year = asset.Year,
                    Cost = asset.Cost,
                    Status = asset.Status.Name,
                    ImageUrl = asset.ImageUrl,
                    AuthorOrDirector = _assets.GetAuthorOrDirector(id),
                    DeweyCallNumber = _assets.GetDeweyIndex(id),
                    Type = _assets.GetType(id),
                    CheckOutHistory = _CheckOuts.GetCheckOutHistory(id),
                    ISBN = _assets.GetIsbn(id),
                    LatestCheckOut = _CheckOuts.GetLatestCheckOut(id),
                    PatronName = _CheckOuts.GetCurrentCheckOutPatron(id),
                    CurrentLocation = _assets.GetCurrentLocation(id).Name,
                    CurrentHolds = currentHolds,
                };
                //return model == null ? View() : View(model);
                return View(model);

            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(LibraryAsset asset)
        {
            return View();
        }

        [HttpGet]
        public IActionResult MarkLost(int assetId)
        {
            _CheckOuts.MarkLost(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpGet]
        public IActionResult MarkFound(int assetId)
        {
            _CheckOuts.MarkFound(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpGet]
        //[NonAction]
        public IActionResult CheckOut(int id)
        {
            LibraryAsset asset = _assets.GetById(id);
            CheckOutModel model = new CheckOutModel(id, asset, _CheckOuts);
            return View(model);
        }

        [HttpGet]
        public IActionResult CheckIn(int id)
        {
            if (ModelState.IsValid)
            {
                _CheckOuts.CheckInItem(id);//, libraryCardId);
                return RedirectToAction("Detail", routeValues: new { id });
            }
            return RedirectToAction("CheckOut", new { id = id });
        }

        [HttpGet]
        public IActionResult Hold(int id)
        {
            LibraryAsset asset = _assets.GetById(id);
            CheckOutModel model = new CheckOutModel(id, asset, _CheckOuts)
            {
                HoldCount = _CheckOuts.GetCurrentHolds(id).Count(),
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult PlaceCheckOut(int assetId, int libraryCardId)
        {
            if (ModelState.IsValid)
            {
                _CheckOuts.CheckOutItem(assetId, libraryCardId);
                return RedirectToAction("Detail", new { id = assetId });
            }
            return RedirectToAction("CheckOut", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceHold(int assetId, int libraryCardId)
        {
            if (ModelState.IsValid)
            {
                _CheckOuts.PlaceHold(assetId, libraryCardId);
                return RedirectToAction("Detail", new { id = assetId });
            }
            return RedirectToAction("Hold", new { id = assetId });
        }
    }
}