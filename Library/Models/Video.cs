using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Video : LibraryAsset
    {
        public Video() : base() { }
        public Video(int id) : base(id) { }
        public Video(string director)
        {
            this.Director = director;
        }
        public Video(double cost, LibraryBranch location, StatusId status, string title, short year, string imageUrl, int numberOfCopies, string director)
            : base(cost, location, status, title, year, imageUrl, numberOfCopies)
        {
            this.Director = director;
        }
        [Required]
        public string Director { get; set; }
    }
}