using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Video : LibraryAsset
    {
        public Video() { }
        public Video(int id)
        {
            this.Id = id;
        }
        public Video(string director) 
        {
            this.Director = director;
        }
        public Video(double cost, LibraryBranch location, StatusId status,  string title, short year, string imageUrl, byte numberOfCopies, string director)
            : base (cost, location, status,  title,year, imageUrl, numberOfCopies)
        {
            this.Director = director;
        }
        [Required]
        public string Director { get; set; }
    }
}