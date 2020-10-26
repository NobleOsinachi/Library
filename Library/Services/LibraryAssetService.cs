using System;
using System.Linq;
using System.Collections.Generic;
using Library;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
//using Library.Data;

namespace Library.Services
{
    public class LibraryAssetService : ILibraryAsset //, IDisposable
    {
        private readonly LibraryContext _context;
        public LibraryAssetService(LibraryContext context)
        {
            _context = context;
            //_context = new LibraryContext();            
        }

        public void Add(LibraryAsset newAsset)
        {
            //_context.LibraryAssets.Add(newAsset);
            _context.Add(newAsset);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {            
            return _context.LibraryAssets
                .Include(c => c.Status)
                .Include(c => c.Location)
                ;
        }

        public LibraryAsset GetById(int id)
        {
            //return _context.LibraryAssets.Find(id);
            return GetAll()
               .FirstOrDefault(a => a.Id == id);
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            /*
            return _context.LibraryAssets
                .FirstOrDefault(c => c.Id == id)
                .Location;
             */
            return GetById(id).Location;
        }

        public string GetDeweyIndex(int id)
        {
            bool isBook = _context.LibraryAssets.OfType<Book>().Where(a => a.Id == id).Any();
            if (_context.Books.Any(b => b.Id == id) && isBook == true)
                return _context.Books.FirstOrDefault(b => b.Id == id).DeweyIndex;
            return string.Empty;
        }

        public string GetIsbn(int id)
        {
            if (_context.Books.Any(b => b.Id == id))
                return _context.Books.FirstOrDefault(b => b.Id == id).ISBN;
            return string.Empty;
        }

        public string GetTitle(int id)
        {
            return GetById(id).Title;
        }

        public string GetType(int id)
        {
            bool isBook = _context.LibraryAssets
                .OfType<Book>().Where(b => b.Id == id).Any();
            return isBook ? Discriminator.Book : Discriminator.Video;
        }


        public string GetAuthorOrDirector(int id)
        {
            bool isBook = _context.LibraryAssets.OfType<Book>()
                .Where(a => a.Id == id).Any();
            bool isVideo = _context.LibraryAssets.OfType<Video>()
                .Where(a => a.Id == id).Any();
            return isBook ?
                _context.Books.FirstOrDefault(book => book.Id == id).Author :
                _context.Videos.FirstOrDefault(video => video.Id == id).Director ??
                "Unknown";
        }

        public CheckOut GetCheckOutByAssetId(int assetIid)
        {
            return new CheckOut(assetIid);// _context.CheckOuts;
        }

        //public void Dispose(){this._context.Dispose();}
    }
}
