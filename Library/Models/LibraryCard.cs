using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    /// <summary>
    /// It provides a layer of abstraction between the assets a Patron checks out and the Patron himself.
    /// It is also used as the object that contains information about overdue fees and assets that are checked out to a particular patron who has the relationship with the card
    /// </summary>
    public class LibraryCard
    {
        public LibraryCard()
        {

        }
        public LibraryCard(int id)
        {
            this.Id = id;

        }
        public LibraryCard(DateTime created, double fees)
        {
            this.Created = created;
            this.Fees = Convert.ToDecimal(fees);
        }
        public int Id { get; private set; }
        /// <summary>
        /// Any over-due fees that may be associated with the owner
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal Fees { get; set; }
        public DateTime Created { get; set; }
        public virtual IEnumerable<CheckOut> CheckOuts { get; set; }

    }
}