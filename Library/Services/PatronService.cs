using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class PatronService : IPatron
    {
        private ICheckOut _CheckOuts { get; }

        private LibraryContext _context { get; }
        private ICheckOutHistory _checkOutHistory { get; }

        public PatronService()
        {

        }
        public PatronService(LibraryContext context, ICheckOutHistory CheckOutHistory, ICheckOut CheckOuts)
        {

            _context = context;
            _checkOutHistory = CheckOutHistory;
            _CheckOuts = CheckOuts;
        }
        private IQueryable<Patron> GetAllWithoutNavigationProp() { return _context.Patrons; }

        public void Add(Patron patron)
        {
            if (patron != null)
            {
                _context.Add(patron);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            Patron patron = _context.Patrons.FirstOrDefault(c => c.Id == id);
            _context.Patrons.Remove(patron);
            _context.SaveChanges();
        }

        public IEnumerable<Patron> GetAll()
        {
            return _context.Patrons
            .Include(c => c.LibraryCard)
            .Include(c => c.HomeLibraryBranch);
        }

        public string GetBranchName(int id)
        {
            return GetById(id)

            .HomeLibraryBranch.Name;
        }

        public Patron GetById(int id)
        {
            return GetAll()
            .FirstOrDefault(p => p.Id == id);
        }

        public decimal GetOverdueFees(int id)
        {
            return GetById(id).LibraryCard.Fees;
        }

        public void Update(Patron patron)
        {
            Patron patronInDb = GetById(patron.Id);
            patronInDb = new Patron()
            {
                LastName = patron.LastName,
            };
            _context.Update(patronInDb);
            _context.SaveChanges();
        }

        public IEnumerable<CheckOutHistory> GetCheckOutHistory(int id)
        {
            int cardId = GetLibraryCardId(id);
            return _checkOutHistory.GetAll()
            .Where(c => c.LibraryCard.Id == cardId)
            .OrderByDescending(c => c.CheckedOut);
        }

        private int GetLibraryCardId(int id)
        {
            return GetAllWithoutNavigationProp()
            .Include(c => c.LibraryCard)
            .FirstOrDefault(c => c.Id == id)
            .LibraryCard.Id;
        }

        public IEnumerable<Hold> GetHolds(int patronId)
        {
            int cardId = GetById(patronId).LibraryCard.Id;
            return _context.Holds
            .Include(c => c.LibraryCard)
            .Include(c => c.LibraryAsset)
            .Where(x => x.LibraryCard.Id == cardId)
            .OrderByDescending(x => x.HoldPlaced);
        }

        public IEnumerable<CheckOut> GetCheckOuts(int id)
        {
            int cardId = GetLibraryCardId(id);
            return _CheckOuts.GetAll()
            .Where(c => c.LibraryCard.Id == cardId);
        }
                
    }
}
