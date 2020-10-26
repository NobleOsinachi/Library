using System.Collections.Generic;

namespace Library.ViewModels.Catalog
{
    public class AssetIndexModel
    {
        public AssetIndexModel()
        {
        }
        public AssetIndexModel(IEnumerable<AssetIndexListingModel> assets)
        {
            this.Assets = assets;
        }
        public IEnumerable<AssetIndexListingModel> Assets { get; set; }
    }
}