using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.ViewModels.Patron
{
    public class PatronIndexModel
    {
        public IEnumerable<PatronDetailModel> Patrons{ get; set; }
    }
}
