using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.ViewModels.Branch
{
    public class BranchIndexModel //: Models.BranchHour
    {
        public IEnumerable<BranchDetailModel> Branches { get; internal set; }
    }
}
