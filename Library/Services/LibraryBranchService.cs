using System;
using System.Collections.Generic;
using System.Linq;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
public class LibraryBranchService : ILibraryBranch
{

private readonly LibraryContext _context;
public LibraryBranchService(LibraryContext context)
{
_context = context;
}

public void Add(LibraryBranch newBranch)
{

_context.Add(newBranch);
_context.SaveChanges();
}

public void Delete(int id)
{
throw new NotImplementedException();
}

public IEnumerable<LibraryBranch> GetAll()
{
return _context.LibraryBranches
.Include(c => c.Patrons)
.Include(c => c.LibraryAssets)
.ToList();
}

public IEnumerable<LibraryAsset> GetAssets(int id)
{
return _context.LibraryBranches
.Include(c => c.LibraryAssets)
.FirstOrDefault(c => c.Id == id)
.LibraryAssets
.ToList();
}

public IEnumerable<string> GetBranchHours(int branchId)
{
IEnumerable<BranchHour> hours = _context.BranchHours
.Where(c => c.Branch.Id == branchId)
.OrderBy/*Descending*/(c => c.DayOfWeek);
return DataHelpers.HumanizeBusinessHours(hours);
}

public LibraryBranch GetById(int id)
{
return GetAll().FirstOrDefault(c => c.Id == id);
}

public IEnumerable<Patron> GetPatrons(int branchId)
{
return _context.LibraryBranches
.Include(c => c.Patrons)
.FirstOrDefault(b => b.Id == branchId)
.Patrons;
}

public bool IsBranchOpen(int id)
{
int currentTimeHour = DateTime.Now.Hour;
///In our db, DayOfWeek goes from 1-7, while the enum DayOfWeek goes from 0-6, hence the +1
int currentDayOfWeek = (int)DateTime.Now.DayOfWeek;
IQueryable<BranchHour> hours = _context.BranchHours.Where(h => h.Branch.Id == id);
BranchHour daysHours = hours.FirstOrDefault(h => h.DayOfWeek == (DayOfWeek)currentDayOfWeek);

//return currentTimeHour > daysHours.OpenTime && currentTimeHour > daysHours.CloseTime;
return currentTimeHour < daysHours.CloseTime
&& currentTimeHour > daysHours.OpenTime;
//return true;
}

public void Update(LibraryBranch entity)
{
throw new NotImplementedException();
}
}
}