using System;
using System.Collections.Generic;
using System.Text;
using Library.Models;

namespace Library.Data
{
    public interface ILibraryBranch : ICRUD<LibraryBranch>
    {
        IEnumerable<Patron> GetPatrons(int id);
        IEnumerable<LibraryAsset> GetAssets(int id);
        IEnumerable<string> GetBranchHours(int id);
        bool IsBranchOpen(int id);
    }
}
