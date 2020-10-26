using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    /// <summary>
    /// Static data that will represent the Name and Description of the various states our asset can have.
    /// </summary>
    //[Table("Status")]
    public class Status
    {
        public Status()
        {
        }
        public Status(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public const string Available = "Available";
        public const string CheckedOut = "Checked Out";
        public const string Lost = "Lost";
        public const string OnHold = "On Hold";
    }
    public enum StatusId
    {
        CheckedOut = 1,
        Available = 2,
        Lost = 3,
        OnHold = 4,
    }
}