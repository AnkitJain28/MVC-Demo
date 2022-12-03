using MVC_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_Demo.Controllers
{
    public class LoginController : Controller
    {
        
        // GET: Login
        //Returns login view page
        public ActionResult Login()
        {
            return View();
        }

        // Ends current session and redirects to login
        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //Confirms UserName and Password from database and redirects to Books action
        //Otherwise Display "Username or password is incoreect." message on Login page
        public ActionResult Login(LoginModel objUser)
        {
            if (ModelState.IsValid)
            {
                using (Entities db = new Entities())
                {
                    var obj = db.Users.Where(a => a.Name.Equals(objUser.Name) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj == null)
                    {
                        ViewBag.Message = "Username or password is incoreect.";
                    }
                    else
                    {
                        Session["UserID"] = obj.UserId.ToString();
                        Session["UserName"] = obj.Name.ToString();
                        return RedirectToAction("Books","Book");
                    }
                }
            }
            return View(objUser);
        }

        
    }

    

    

}
