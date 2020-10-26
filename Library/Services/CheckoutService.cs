using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class CheckOutService : ICheckOut
    {
        private readonly LibraryContext _context;
        private readonly ILibraryAsset _asset;
        //private IHold _hold;// = new HoldService();
        private readonly IHold _hold = new HoldService();
        private static DateTime now;//


        public CheckOutService(LibraryContext context, ILibraryAsset asset
            //, IHold hold
            )
        {
            _context = context;
            _asset = asset;
            //_hold = hold;
        }
        public void Add(CheckOut newCheckOut)
        {
            _context.Add(newCheckOut);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<CheckOut>> GetAllAsync()
        {
            return await _context.CheckOuts
                .Include(c => c.LibraryAsset)
                .Include(c => c.LibraryCard)
                .ToListAsync();
        }
        public IEnumerable<CheckOut> GetAll()
        {
            return _context.CheckOuts
                .Include(c => c.LibraryAsset)
                .Include(c => c.LibraryCard);
        }

        public CheckOut GetById(int CheckOutId)
        {
            return GetAll()
                .FirstOrDefault(c => c.Id == CheckOutId);
        }

        public IEnumerable<CheckOutHistory> GetCheckOutHistory(int id)
        {
            return _context.CheckOutHistories
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .Where(h => h.LibraryAsset.Id == id);
        }

        public IEnumerable<Hold> GetCurrentHolds(int id)
        {
            return _context.Holds
                .Include(c => c.LibraryAsset)
                .Where(h => h.LibraryAsset.Id == id);
        }

        public CheckOut GetLatestCheckOut(int assetId)
        {
            return _context.CheckOuts
                .Include(c => c.LibraryAsset)
                .OrderByDescending(c => c.Since)
                .Where(c => c.Id == assetId)
                .FirstOrDefault();
        }

        public string GetCurrentHoldPatronName(int holdId)
        {
            //return "";
            return _hold.GetCurrentHoldPatronName(holdId);
        }

        public DateTime GetCurrentHoldPlaced(int holdId)
        {
            //return new DateTime();
            return _hold.GetById(holdId).HoldPlaced;
        }

        private void UpdateAssetStatus(int assetId, string status)
        {
            //_context.LibraryAssets.FirstOrDefault(a => a.Id == assetId);
            LibraryAsset item = _asset.GetById(assetId);
            if (item != null)
            {
                _context.Update(item);
                item.Status = _context.Statuses.FirstOrDefault(s => s.Name == status);
                _context.SaveChanges();
            }

        }
        public void MarkFound(int assetId)
        {
            now = DateTime.Now;
            UpdateAssetStatus(assetId, Status.Available);
            RemoveExistingCheckOuts(assetId);
            CloseExistingCheckOutHistory(assetId, now);
            _context.SaveChanges();
        }

        public void MarkLost(int assetId)
        {
            UpdateAssetStatus(assetId, Status.Lost);
        }

        public void CheckInItem(int assetId)//, int libraryCardId)
        {
            LibraryAsset item = _asset.GetById(assetId);

            // Remove any existing CheckOuts on the item
            RemoveExistingCheckOuts(assetId);
            // Close any existing CheckOut history
            CloseExistingCheckOutHistory(assetId, now);
            // Look for existing holds on the item
            IQueryable<Hold> currentHolds = _context.Holds
                .Include(c => c.LibraryAsset)
                .Include(c => c.LibraryCard)
                .Where(h => h.LibraryAsset.Id == assetId);

            // If there are holds, CheckOut the item to the librarycard with earliest hold
            if (currentHolds.Any())
            {
                CheckOutToEarliestHold(assetId, currentHolds);
            }
            else
            {
                // Otherwise, update item status to Available
                UpdateAssetStatus(assetId, Status.Available);
            }

            _context.SaveChanges();
        }

        private void CheckOutToEarliestHold(int assetId, IQueryable<Hold> currentHolds)
        {
            Hold earlistHold = currentHolds
                .OrderBy(x => x.HoldPlaced)
                .FirstOrDefault();

            LibraryCard card = earlistHold.LibraryCard;

            //Once the hold has been fulfilled to a CheckOut, we no longer
            //need a hold on it, as it has been checked out to LibraryCard with earliest hold
            _context.Remove(earlistHold);
            _context.SaveChanges();
            CheckOutItem(assetId, card.Id);
        }

        public void CheckOutItem(int assetId, int libraryCardId)
        {
            if (IsCheckedOut(assetId))
            {
                return; //add logic to handle user feedback here
            }

            LibraryAsset item = _context.LibraryAssets.
                FirstOrDefault(a => a.Id == assetId);
            LibraryCard libraryCard = _context.LibraryCards
                .Include(c => c.CheckOuts)
                .FirstOrDefault(c => c.Id == libraryCardId);
            if (libraryCard != null)
            {
                UpdateAssetStatus(assetId, Status.CheckedOut);
                now = DateTime.Now;
                CheckOut CheckOut = new CheckOut(item, libraryCard, now);
                _context.Add(CheckOut);

                /**add a new CheckOut history. This could be handled in a 
                 * shadow table in SQL Server or in a database trigger */
                CheckOutHistory CheckOutHistory = new CheckOutHistory(item, libraryCard, now);

                _context.Add(CheckOutHistory);
                _context.SaveChanges();
            }
            else
            {
                return;
            }
        }

        public bool IsCheckedOut(int assetId)
        {
            return _context.CheckOuts.Where(c => c.LibraryAsset.Id == assetId).Any();
        }

        /// <summary>
        /// Place a hold on an available asset with given id only if it is Available
        /// </summary>
        /// <param name="assetId"></param>
        /// <param name="libraryCardId"></param>
        public void PlaceHold(int assetId, int libraryCardId)
        {
            now = DateTime.Now;
            LibraryAsset asset = _asset.GetById(assetId);
            LibraryCard card = _context.LibraryCards.FirstOrDefault(c => c.Id == libraryCardId);
            if (card != null)
            {
                Hold hold = new Hold(asset, card, now) { };
                if (asset.Status.Name == Status.Available)
                {
                    UpdateAssetStatus(assetId, Status.OnHold);
                }

                _context.Add(hold);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// If item has been found, remove any existing CheckOuts on the item, because the item may have been checked out when it was reported as lost.
        /// </summary>
        /// <param name="assetId"></param>
        private void RemoveExistingCheckOuts(int assetId)
        {
            CheckOut CheckOut = GetAll()
                .FirstOrDefault(c => c.LibraryAsset.Id == assetId);
            if (CheckOut != null) //if there are CheckOuts on this item...
            {
                _context.Remove(CheckOut);
            }
        }

        private void CloseExistingCheckOutHistory(int assetId, DateTime now)
        {
            CheckOutHistory history = _context.CheckOutHistories
                .FirstOrDefault(c => c.LibraryAsset.Id == assetId &&
                /** This checks to ensure it is an open CheckOut*/
                c.CheckedIn == null);
            if (history != null)
            {
                _context.Update(history);
                history.CheckedIn = now;
            }
        }

        public string GetCurrentCheckOutPatron(int assetId)
        {
            CheckOut CheckOut = GetCheckOutByAssetId(assetId);
            if (CheckOut == null)
            {
                return string.Empty;
            }

            int cardId = CheckOut.LibraryCard.Id;

            Patron patron = _context.Patrons
                .Include(p => p.LibraryCard)
                .FirstOrDefault(p => p.LibraryCard.Id == cardId);

            return Patron.PrintFullName(patron);
        }

        public CheckOut GetCheckOutByAssetId(int assetId)
        {
            return GetAll()
                .FirstOrDefault(c => c.LibraryAsset.Id == assetId);
        }
    }
}
