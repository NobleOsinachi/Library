using System;
using System.Collections.Generic;
using Library.Data;
using Library.Models;
using Library.ViewModels;

namespace Library.ViewModels.Catalog
{
    public class AssetDetailModel
    {
        public AssetDetailModel()
        {

        }

        //public AssetDetailModel(int id)
        //{
        //    this.AssetId = id;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id">This is gotten from url route</param>
        ///// <param name="asset"></param>

        //public AssetDetailModel(int id, LibraryAsset asset) : this(id)
        //{
        //    this.Title = asset.Title;
        //    this.Year = asset.Year;
        //    this.Cost = asset.Cost;
        //    this.Status = asset.Status.Name.ToString();// = asset.Status.Name;
        //    this.ImageUrl = asset.ImageUrl;

        //}
        //public AssetDetailModel(int id, LibraryAsset _asset, ILibraryAsset asset) : this(id,_asset)
        //{
        //    this.AuthorOrDirector = asset.GetAuthorOrDirector(id);
        //    this.CurrentLocation = asset.GetCurrentLocation(id).Name;
        //    this.DeweyCallNumber = asset.GetDeweyIndex(id);
        //    this.ISBN = asset.GetIsbn(id);
        //    this.Type = asset.GetType(id);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="asset"></param>
        ///// <param name="CheckOuts">This is passed from the dependency injected service in the constructor</param>
        ///// <param name="currentHolds"></param>
        //public AssetDetailModel(int id, LibraryAsset _asset, ILibraryAsset asset, ICheckOut CheckOuts, IEnumerable<AssetHoldModel> currentHolds) : this(id, _asset, asset)
        //{
        //    this.LatestCheckOut = CheckOuts.GetLatestCheckOut(id);
        //    this.PatronName = CheckOuts.GetCurrentCheckOutPatron(id);
        //    this.CurrentHolds = currentHolds;
        //}

        public string? Description { get; set; }
        public int AssetId { get; set; }
        public string Title { get; set; }
        public string AuthorOrDirector { get; set; }
        public string Type { get; set; }
        public short Year { get; set; }
        public string ISBN { get; set; }
        public string DeweyCallNumber { get; set; }
        public string Status { get; set; } //= null;
        public decimal Cost { get; set; }
        public string GetFormattedCost () => Cost.ToString("0.00");
        
        public string CurrentLocation { get; set; }
        public string ImageUrl { get; set; }
        public string PatronName { get; set; }
        public Models.CheckOut LatestCheckOut { get; set; }

        public IEnumerable<CheckOutHistory> CheckOutHistory { get; set; }
        public IEnumerable<AssetHoldModel>? CurrentHolds { get; set; }
    }


    public class AssetHoldModel
    {
        public AssetHoldModel(int id, ICheckOut CheckOut)
        {
            this.HoldPlaced = CheckOut.GetCurrentHoldPlaced(id);
            this.PatronName = CheckOut.GetCurrentHoldPatronName(id);
        }

        public string PatronName { get; set; }
        public DateTime HoldPlaced { get; set; }
       
    }
}