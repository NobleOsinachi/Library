using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class CheckOutHistory
    {
        public CheckOutHistory(int id)
        {
            Id = id;
        }
        public CheckOutHistory(LibraryAsset libraryAsset, LibraryCard libraryCard)
        {
            LibraryAsset = libraryAsset;
            LibraryCard = libraryCard;
            CheckedOut = DateTime.Now;
        }
        public int Id { get; private set; }
        [Required]
        public LibraryAsset LibraryAsset { get; set; }
        [Required]
        public LibraryCard LibraryCard { get; set; }
        [Required]
        public DateTime CheckedOut { get; set; }
        public DateTime? CheckedIn { get; set; }

    }
}