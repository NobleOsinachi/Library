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
        public BranchHour(LibraryBranch branchId, int closeTime, DayOfWeek dayOfWeek, int openTime)
        {
            this.Branch = branchId;
            this.DayOfWeek = dayOfWeek;
            this.OpenTime = openTime;
            this.CloseTime = closeTime;

        }
        public int Id { get; private set; }
        public LibraryBranch Branch { get; set; }
        [Range(0, 6)]
        //public int DayOfWeek { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        [Range(0, 23)]
        public int OpenTime { get; set; }
        [Range(0, 23)]
        public int CloseTime { get; set; }
    }
}

