using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book : LibraryAsset
    {
        public Book():base()
        {

        }
        public Book(int id) : base(id) { }

        public Book(double cost, LibraryBranch location, StatusId status, string title, int year,
            string imageUrl, int numberOfCopies, string isbn, string author, string deweyIndex)
            : base(cost, location, status, title, year, imageUrl, numberOfCopies)
        {
            ISBN = isbn.Replace("-", "");
            Author = author;
            DeweyIndex = deweyIndex;
        }

        [Required]
        [StringLength(maximumLength: 18, MinimumLength = 10)]
        [MinLength(10)]
        [MaxLength(18)]
        public string ISBN { get; set; } //International Standard Book Number


        [Required]
        public string Author { get; set; }

        [Required]
        public string DeweyIndex { get; set; }

        public string RemoveHyphens(string isbn)
        {
            ISBN = isbn;
            return isbn.Replace("-", "");
        }
    }

}
