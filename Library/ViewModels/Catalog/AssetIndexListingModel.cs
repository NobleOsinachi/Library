using System.ComponentModel.DataAnnotations;
using Library.Data;
using Library.Models;

namespace Library.ViewModels.Catalog
{
    public class AssetIndexListingModel //: LibraryAsset
    {
        public AssetIndexListingModel(LibraryAsset result)
        {
            Id = result.Id;
            ImageUrl = result.ImageUrl;
            Title = result.Title;
        }

        public AssetIndexListingModel(LibraryAsset result, ILibraryAsset assets) : this(result)
        {
            //this.Id = result.Id;
            //this.ImageUrl = result.ImageUrl;
            //this.Title = result.Title;
            AuthorOrDirector = assets.GetAuthorOrDirector(result.Id);
            DeweyCallNumber = assets.GetDeweyIndex(result.Id);
            Type = assets.GetType(result.Id);
        }

        public int Id { get; private set; }
        [DataType(DataType.ImageUrl)]

        public string ImageUrl { get; set; }
        [Required]
        public string Title { get; set; }
        public string AuthorOrDirector { get; set; }
        public string Type { get; set; }
        public string DeweyCallNumber { get; set; }
        public short NumberOfCopies { get; set; }

        //public LibraryBranch Location { get; set; }

    }
}