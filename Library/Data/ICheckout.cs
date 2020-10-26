using System;
using System.Collections.Generic;
using Library.Models;

namespace Library.Data
{
    public interface ICheckOut
    {

        void Add(CheckOut newCheckOut);
        IEnumerable<CheckOut> GetAll();
        CheckOut GetById(int CheckOutId);
        CheckOut GetLatestCheckOut(int assetId);
        CheckOut GetCheckOutByAssetId(int assetIid);

        IEnumerable<CheckOutHistory> GetCheckOutHistory(int id);
        IEnumerable<Hold> GetCurrentHolds(int id);

        /// <summary>
        /// Get the current patron that has an item checked out
        /// </summary>
        string GetCurrentCheckOutPatron(int assetId);
        string GetCurrentHoldPatronName(int id);
        bool IsCheckedOut(int assetId);
        DateTime GetCurrentHoldPlaced(int id);

        void MarkLost(int assetId);
        void MarkFound(int assetId);
        void CheckInItem(int assetId);//, int libraryCardId);
        void CheckOutItem(int assetId, int libraryCardId);
        void PlaceHold(int assetId, int libraryCardId);
    }
}
