using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public abstract class LibraryAsset
    {
        private readonly string _defaultImage = "/images/no-cover-available.png";

        public LibraryAsset()
        {
            ImageUrl = ImageChecker(ImageUrl);
        }
        public LibraryAsset(int id)
        {
            Id = id;
        }
        public LibraryAsset(double cost, LibraryBranch location, StatusId status, string title, int year, string imageUrl, int numberOfCopies)
        {
            Cost = Convert.ToDecimal(cost);
            Location = location;
            Status = new Status(status);
            Title = title;
            Year = (short)year;
            NumberOfCopies = numberOfCopies;
            ImageUrl = ImageChecker(imageUrl);
        }

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [MinLength(4),MaxLength(4)]
        [Range(0000, 9999)]
        public short Year { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }
        [DataType(DataType.ImageUrl)]
        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set => _imageUrl = ImageChecker(value);
        }
        [Range(0, short.MaxValue)]
        public int NumberOfCopies { get; set; }

        public LibraryBranch Location { get; set; }

        public string? Description { get; set; }

        //[RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public Genre? Genre { get; set; }
        protected string ImageChecker(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? _defaultImage : value;
        }
    }

    public struct Discriminator
    {
        public const string Book = "Book";
        public const string Video = "Video";
    }
}
