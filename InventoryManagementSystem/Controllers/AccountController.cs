using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagementSystem.Models;
using System.Web.Security;

namespace InventoryManagementSystem.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        //login Controller
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public object Login(user model)
        {
            using (var context = new imsdbEntities())
            {
                bool isValid = context.users.Any(x => x.User_Name == model.User_Name && x.Password == model.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.User_Name, true);
                    return RedirectToAction("Dashboard", "Home");
                }
                ModelState.AddModelError("password", "Invalid Username or Password");
                return View();
            }

        }
        //controller Signup
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(user model)
        {
            using (var context = new imsdbEntities())
            {
                context.users.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }
        //logout Controller
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }


    }
}