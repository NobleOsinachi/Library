using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Models
{
    public class BranchHour
    {
        public BranchHour(int id)
        {
            this.Id = id;

        }
        public BranchHour(LibraryBranch branchId, byte closeTime, byte dayOfWeek, byte openTime)
        {
            this.Branch = branchId;
            this.DayOfWeek = dayOfWeek;
            this.OpenTime = openTime;
            this.CloseTime = closeTime;

        }
        public int Id { get; set; }
        public LibraryBranch Branch { get; set; }
        [Range(0, 6)]
        public byte DayOfWeek { get; set; }
        [Range(0, 23)]
        public byte OpenTime { get; set; }
        [Range(0, 23)]
        public byte CloseTime { get; set; }
    }
}

