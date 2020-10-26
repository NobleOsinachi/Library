using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Library.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Identity;

namespace Library.Data
{
    public class LibraryContext : IdentityDbContext
    {
        public LibraryContext() : base() { }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options) { }

        public virtual DbSet<BranchHour> BranchHours { get; set; }
        public virtual DbSet<CheckOut> CheckOuts { get; set; }
        public virtual DbSet<CheckOutHistory> CheckOutHistories { get; set; }
        public virtual DbSet<Hold> Holds { get; set; }
        public virtual DbSet<LibraryBranch> LibraryBranches { get; set; }
        public virtual DbSet<LibraryCard> LibraryCards { get; set; }
        public virtual DbSet<Patron> Patrons { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<LibraryAsset> LibraryAssets { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Genre> Genres{ get; set; }
        public virtual DbSet<Video> Videos { get; set; }

        public virtual DbSet<DerivedClass> DerivedClasses{ get; set; }
        public virtual DbSet<BaseClass> BaseClasses{ get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Status>().ToTable("Status");
            base.OnModelCreating(builder);
            //builder.Entity<BaseClass>().ToTable("NobleOsinachi");//
        }
    }
}
