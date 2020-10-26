using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class CheckOutHistory
    {
        public CheckOutHistory(int id)
        {
            this.Id = id;
        }
        public CheckOutHistory(LibraryAsset libraryAsset, LibraryCard libraryCard, DateTime now)
        {
            this.LibraryAsset = libraryAsset;
            this.LibraryCard = libraryCard;
            this.CheckedOut = now;
        }
        public int Id { get; set; }
        [Required]
        public LibraryAsset LibraryAsset { get; set; }
        [Required]
        public LibraryCard LibraryCard { get; set; }
        [Required]
        public DateTime CheckedOut { get; set; }
        public DateTime? CheckedIn { get; set; }

    }
}