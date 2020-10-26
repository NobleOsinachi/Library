using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Library.Models
{
    public class Patron
    {
        public Patron()
        {

        }
        public Patron(int id)
        {
            Id = id;
        }
        public Patron(string address, DateTime dateOfBirth, string firstName,
            LibraryBranch homeLibraryBranch, string lastName, LibraryCard libraryCard)
        {
            Address = address;
            DateOfBirth = dateOfBirth;
            FirstName = firstName;
            HomeLibraryBranch = homeLibraryBranch;
            LastName = lastName;
            LibraryCard = libraryCard;
        }

        [HiddenInput]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ReadOnly(true)]
        public string FullName => FirstName + " " + LastName;
        public string Address { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public int Age => DateTime.Now.Year - DateOfBirth.Value.Year;

        [DataType(DataType.PhoneNumber)]
        [Phone]
        [DisplayFormat(NullDisplayText = "No phone number given")]

        public string TelephoneNumber { get; set; }

        public virtual LibraryCard LibraryCard { get; set; }
        public virtual LibraryBranch HomeLibraryBranch { get; set; }

        public static string PrintFullName(Patron patron) { return patron != null ? patron.FirstName + " " + patron.LastName : string.Empty; }

        //public string PrintFullName(Patron patron) { return patron != null ? patron.FirstName + " " + patron.LastName : string.Empty; }
    }
}
