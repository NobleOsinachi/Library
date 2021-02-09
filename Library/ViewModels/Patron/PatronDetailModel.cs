using System;
using Library.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.Patron
{
    public class PatronDetailModel //: Models.Patron
    {

        public PatronDetailModel(Models.Patron p)
        {
            Id = p.Id;
            FirstName = p.FirstName;
            LastName = p.LastName;
            LibraryCardId = p.LibraryCard.Id;
            OverdueFees = p.LibraryCard.Fees;
            HomeLibraryBranch = p.HomeLibraryBranch.Name;
            // this.LibraryCardId = base.LibraryCard.Id;

            Address = p.Address;
            MemberSince = p.LibraryCard.Created;
            TelephoneNumber = p.TelephoneNumber;
        }

        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName =>/*return*/ FirstName + " " + LastName;
        public string Address { get; set; }
        public DateTime MemberSince { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string TelephoneNumber { get; set; }

        public decimal OverdueFees { get; set; }
        public int LibraryCardId { get; set; }
        public string HomeLibraryBranch { get; set; }

        public IEnumerable<Models.CheckOut> AssetsCheckedOut { get; set; } = new List<Models.CheckOut>();
        public IEnumerable<CheckOutHistory> CheckOutHistory { get; set; } = new List<Models.CheckOutHistory>();
        public IEnumerable<Hold> Holds { get; set; } = new List<Models.Hold>();
    }
}
