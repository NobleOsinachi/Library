using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class CheckOut
    {
        public CheckOut(int id)
        {
            this.Id = id;
        }
        public CheckOut(LibraryAsset libraryAsset, LibraryCard libraryCard, DateTime since)
        {
            this.LibraryAsset = libraryAsset;
            this.LibraryCard = libraryCard;
            this.Since = since;
            this.Until = GetDefaultCheckOutTime(since);
        }

        public int Id { get; private set; }
        [Required]

        public LibraryAsset LibraryAsset { get; set; }
        /// <summary>
        /// Posesses one-on-one relationship with Patron
        /// </summary>
        public LibraryCard LibraryCard { get; set; }
        /// <summary>
        /// When an item was checked out
        /// </summary>
        public DateTime Since { get; set; }
        /// <summary>
        /// When an item is due
        /// </summary>
        public DateTime Until { get; set; }

        private DateTime GetDefaultCheckOutTime(DateTime since)
        {
            return since.AddDays(30);
        }

    }
}