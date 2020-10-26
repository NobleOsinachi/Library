using System;
using System.Collections.Generic;
using System.Linq;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class CheckOutHistoryService : ICheckOutHistory
    {
        private readonly LibraryContext _context;
        public CheckOutHistoryService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(CheckOutHistory entity)
        {
            _context.Add(entity);
        }

        public void Delete(int id)
        {
            _context.Remove(GetById(id));
        }

        public IEnumerable<CheckOutHistory> GetAll()
        {
            return _context.CheckOutHistories
                .Include(c => c.LibraryCard)
                .Include(c => c.LibraryAsset);
        }

        public CheckOutHistory GetById(int id)
        {
            return GetAll().FirstOrDefault(c => c.Id == id);
        }

        public void Update(CheckOutHistory entity)
        {
            throw new NotImplementedException();
        }
    }
}
