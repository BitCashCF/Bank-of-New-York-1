using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MvcIdentityBank.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcIdentityBank.Controllers
{
    public class AccountController : Controller
    {
        //for User managment
        public CustomUserManager UserManager {
            get => HttpContext.GetOwinContext().GetUserManager<CustomUserManager>();
        }

        //for register, login, logout ... & other identity operations
        private IAuthenticationManager AuthManager
        {
            get => HttpContext.GetOwinContext().Authentication;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                //create user without adding to DB
                CustomUser customUser = new CustomUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    SkinColor = model.SkinColor
                };
                //create UserWithIdentity from simple User
                var result = await UserManager.CreateAsync(
                    customUser, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model,string returnUrl)
        {
            ActionResult returnResult = null;
            if (ModelState.IsValid)
            {
                var customUser = await UserManager.FindAsync(model.Email, model.Password);
                if (customUser == null)
                {
                    ModelState.AddModelError("", "Password or Login is INVALID!");
                }
                else
                {
                    var result = await UserManager.CreateIdentityAsync(customUser,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, result);

                    if (String.IsNullOrEmpty(returnUrl))
                        returnResult = RedirectToAction("Index", "Home");
                    //return RedirectToAction("Index", "Home");
                    else
                        returnResult = Redirect(returnUrl);
                        //return Redirect(returnUrl);
                }
            }
            else
            {
                ViewBag.returnUrl = returnUrl;
                returnResult = View(model);
            }
            //return View(model);
            return returnResult;
        }

        [HttpGet]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}