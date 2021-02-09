using System;

namespace Library.Models
{
    public class Hold
    {
        public Hold(int id)
        {
            this.Id = id;
        }
        public Hold(LibraryAsset asset, LibraryCard card, DateTime holdPlaced)
        {
            this.LibraryAsset = asset;
            this.LibraryCard = card;
            this.HoldPlaced = holdPlaced;
        }

        public int Id { get; private set; }
        /// <summary>
        /// Item for which a hold has been requested
        /// </summary>
        public virtual LibraryAsset LibraryAsset { get; set; }
        /// <summary>
        /// LibraryCard that has requested the hold on item
        /// </summary>
        public virtual LibraryCard LibraryCard { get; set; }
        public DateTime HoldPlaced{ get; set; }
    }
}