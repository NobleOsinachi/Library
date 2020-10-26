using System.ComponentModel.DataAnnotations;
using Library.Data;
using Library.Models;
using Library.Validations;

namespace Library.ViewModels.CheckOut
{
    public class CheckOutModel
    {

        private readonly IPatron _patron;
        public CheckOutModel()
        {

        }
        public CheckOutModel(IPatron patron)
        {
            _patron = patron;
        }
        public CheckOutModel(int id)
        {
            AssetId = id;
        }
        public CheckOutModel(int id, LibraryAsset asset, ICheckOut CheckOuts) : this(id)
        {
            ImageUrl = asset.ImageUrl;
            Title = asset.Title;
            LibraryCardId = string.Empty;// 
            IsCheckedOut = CheckOuts.IsCheckedOut(id);
        }
        public int AssetId { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }

        [Required]
        [ValidLibraryCard]//((PatronService)_patron)]
        //[StringLength(4,ErrorMessage ="Enter at least 4 digits")]
        public string LibraryCardId { get; set; }
        /// <summary>
        /// Number of Patrons who have this item currently on hold
        /// </summary>
        public int HoldCount { get; set; }
        public bool IsCheckedOut { get; set; }
    }
}
