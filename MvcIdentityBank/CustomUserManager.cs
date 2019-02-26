using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;

namespace MvcIdentityBank
{
    public class CustomUserManager : UserManager<CustomUser>
    {
        public CustomUserManager(IUserStore<CustomUser> store) : base(store)
        {
        }

        public static CustomUserManager Create(IdentityFactoryOptions<CustomUserManager> ops, IOwinContext ctx)
        {
            CustomContext cc = ctx.Get<CustomContext>();
            UserStore<CustomUser> userStore = new UserStore<CustomUser>(cc);
            CustomUserManager manager = new CustomUserManager(userStore);
            return manager;
        }
    }
}