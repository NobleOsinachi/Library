using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Library.Models
{
    public class DefaultTablesFromIdentityFramework
    {
        private IdentityUserToken<int> IdentityUserToken { get; set; }
        private IdentityUser IdentityUser { get; set; }
        private IdentityUserRole<int> IdentityUserRole { get; set; }
        private IdentityUserLogin<int> IdentityUserLogin { get; set; }
        private IdentityUserClaim<int> IdentityUserClaim { get; set; }
        private IdentityRole IdentityRole { get; set; }
        private IdentityRoleClaim<int> IdentityRoleClaim { get; set; }
    }
    public class UserStores
    {

    }
}
