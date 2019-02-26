using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace MvcIdentityBank
{
    public class CustomUser : IdentityUser
    {
        public string SkinColor { get; set; }
        public DateTime LastVisit { get; set; }

        //navigation property
        public virtual ICollection<Account> Accounts { get; set; }
    }
}