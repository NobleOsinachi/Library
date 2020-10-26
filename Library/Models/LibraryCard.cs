using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
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
        public int Id { get; set; }
        /// <summary>
        /// Any over-due fees that may be associated with the owner
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal Fees { get; set; }
        public DateTime Created { get; set; }
        public virtual IEnumerable<CheckOut> CheckOuts { get; set; }

    }
}