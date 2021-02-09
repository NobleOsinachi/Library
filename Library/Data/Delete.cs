using System;
using System.ComponentModel.DataAnnotations;
using Library.Models;

namespace Library.Data
{

    public abstract class BaseClass
    {
        public int Id { get; private set; }

        public string Description { get; set; }
        public StatusId Status { get; set; }
    }

    public class DerivedClass : BaseClass
    {
        public DerivedClass(DateTime dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
            Age = DateTime.Now.Year - dateOfBirth.Year;
        }
        [Range(int.MinValue, int.MaxValue)]
        public int Age { get; private set; }
        [DataType(DataType.Date)]

        public DateTime DateOfBirth { get; set; }

        /*
         
          List<DerivedClass> derivedClasses = new List<DerivedClass>()
{
new DerivedClass(new DateTime(1998, 03, 25)){Status = StatusId.Available, Description = "Cool calm collected" },
new DerivedClass(DateTime.Parse("1997-03-30")){Status = StatusId.OnHold, Description = "Testting..." },
};
            derivedClasses.ForEach(c => context.Add(c));
            context.SaveChanges();

         */

    }


    public struct StatusStruct //: Models.Status
    {
        public const int Available = (int)StatusId.Available;
        public const int CheckedOut = (int)StatusId.CheckedOut;
        public const int Lost = (int)StatusId.Lost;
        public const int OnHold = (int)StatusId.OnHold;
    }
}