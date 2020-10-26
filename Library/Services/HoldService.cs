using System;
using System.Collections.Generic;
using System.Linq;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class HoldService : IHold
    {
        private LibraryContext _context { get; }
        private IPatron _patron { get; }
        private ILibraryAsset _asset { get; }

        public HoldService()
        {

        }
        public HoldService(LibraryContext context, IPatron patron, ILibraryAsset asset):this()
        {
            _context = context;
            _patron = patron;
            _asset = asset;
        }

        public void Add(Hold entity)
        {
            _context.Add(entity);
        }

        public void Delete(int id)
        {
            Hold hold = _context.Holds.FirstOrDefault(x => x.Id == id);
            _context.Remove(hold);
        }

        public IEnumerable<Hold> GetAll()
        {
            return _context.Holds
                .Include(d => d.LibraryCard)
                .Include(d => d.LibraryAsset);
        }

        public Hold GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public string GetCurrentHoldPatronName(int id)
        {
            Hold hold = _context.Holds
                    .Include(h => h.LibraryAsset)
                    .Include(h => h.LibraryCard)
                    .FirstOrDefault(c => c.Id == id);
            if (hold == null)
            {
                return "Invalid Id";
            }
            else
            {
                int cardId = hold.LibraryCard.Id;
                return _context.Patrons
                    .Include(p => p.LibraryCard)
                    .FirstOrDefault(p => p.LibraryCard.Id == cardId)
                    .FullName;
            }
        }

        public DateTime GetCurrentHoldPlaced(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hold> GetCurrentHolds(int id)
        {
            throw new NotImplementedException();
        }

        public Hold GetLatestHold(int assetId)
        {
            throw new NotImplementedException();
        }

        public void Update(Hold entity)
        {
            throw new NotImplementedException();
        }
    }
}
