using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace MvcIdentityBank
{
    public class CustomContext : IdentityDbContext<CustomUser>
    {
        public CustomContext() : base("CustomConnectionString") { }

        public static CustomContext Create()
        {
            return new CustomContext();
        }

        public virtual DbSet<Account> Accounts { get; set; }
    }
}