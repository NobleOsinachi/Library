using System;
using System.Collections.Generic;
using Library.Models;

namespace Library.Data
{
    public interface IHold : ICRUD<Hold>
    {
        Hold GetLatestHold(int assetId);
        IEnumerable<Hold> GetCurrentHolds(int id);
        string GetCurrentHoldPatronName(int id);
        DateTime GetCurrentHoldPlaced(int id);
    }
}
