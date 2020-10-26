using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public abstract class LibraryAsset
    {
        private readonly string _defaultImage = "/images/no-cover-available.png";

        public LibraryAsset()
        {
            this.ImageUrl = ImageChecker(ImageUrl);
        }
        public LibraryAsset(int id)
        {
            this.Id = id;
        }
        public LibraryAsset(double cost, LibraryBranch location, StatusId status, string title, int year, string imageUrl, byte numberOfCopies)
        {
            this.Cost = Convert.ToDecimal(cost);
            this.Location = location;
            this.Status = new Status((int)status);
            this.Title = title;
            this.Year = (short)year;
            this.NumberOfCopies = numberOfCopies;
            this.ImageUrl = ImageChecker(imageUrl);
        }

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(4)]
        [MinLength(4)]
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
        public byte NumberOfCopies { get; set; }

        public LibraryBranch Location { get; set; }

        public string? Description { get; set; }

        //[RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public Genre? Genre { get; set; }
        protected string ImageChecker(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? _defaultImage : value;
        }
    }
}
