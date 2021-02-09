using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.Branch
{
    public class BranchDetailModel
    {
        public BranchDetailModel() { }
        public BranchDetailModel(Models.LibraryBranch branch)
        {
            Id = branch.Id;
            Name = branch.Name;
            Address = branch.Address;
            Telephone = branch.Telephone;
            OpenDate = branch.OpenDate.ToString("yyyy-MM-dd");
            ImageUrl = branch.ImageUrl;
        }
        public int Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public String OpenDate { get; set; }
        [Phone, DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }
        public bool IsOpen { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// We can read from Patrons table and get count
        /// </summary>
        public int NumberOfPatrons { get; set; }
        public int NumberOfAssets { get; set; }
        public decimal TotalAssetValue { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<string> HoursOpen { get; set; }
    }
}