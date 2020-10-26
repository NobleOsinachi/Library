using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Library.Models
{
    public class LibraryBranch
    {
        public LibraryBranch()
        {

        }
        public LibraryBranch(int id)
        {
            this.Id = id;
        }
        public LibraryBranch(string name, DateTime openDate)
        {
            this.Name = name;
            this.OpenDate = openDate;
        }
        public LibraryBranch(string address, string name, string telephone, DateTime openDate, string description)
        {
            this.Address = address;
            this.Name = name;
            this.Telephone = telephone;
            this.OpenDate = openDate;
            this.Description = description;
        }

        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Limit Branch Name to 30 characters.")]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Date when the branch was founded
        /// </summary>        
        [DataType(DataType.Date)]
        public DateTime OpenDate { get; set; }

        public virtual IEnumerable<Patron> Patrons { get; set; }
        public virtual IEnumerable<LibraryAsset> LibraryAssets { get; set; }
    }
}