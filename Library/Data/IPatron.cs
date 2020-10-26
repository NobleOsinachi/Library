using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Library.Models;

namespace Library.Data
{
    public interface IPatron : ICRUD<Patron>
    {
        [MethodImpl(MethodImplOptions.ForwardRef)]
        IEnumerable<CheckOutHistory> GetCheckOutHistory(int id);
        IEnumerable<Hold> GetHolds(int id);
        IEnumerable<CheckOut> GetCheckOuts(int id);
        string GetBranchName(int id);
        decimal GetOverdueFees(int id);
    }
}
