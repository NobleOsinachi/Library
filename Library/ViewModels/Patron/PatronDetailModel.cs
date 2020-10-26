using System;
using System.Collections.Generic;

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
        }


        private DateTime _myProperty; public DateTime MyProperty { get => _myProperty; set => _myProperty = value; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string Address { get; set; }
        public DateTime MemberSince { get; set; }

        public string TelephoneNumber { get; set; }

        public decimal OverdueFees { get; set; }
        public int LibraryCardId { get; set; }
        public string HomeLibraryBranch { get; set; }

        public IEnumerable<Models.CheckOut> AssetsCheckedOut { get; set; }
        public IEnumerable<Models.CheckOutHistory> CheckOutHistory { get; set; }
        public IEnumerable<Models.Hold> Holds { get; set; }
    }
}
