using CaptchaMvc.HtmlHelpers;
using Microsoft.Ajax.Utilities;
using MVC_Demo.Common;
using MVC_Demo.Models;
using NLog;
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
        public readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        // GET: Login
        //Returns login view page
        public ActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Logger.Error(ex);
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // Ends current session and redirects to login
        public ActionResult LogOut()
        {
            try
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Logger.Error(ex);
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //Confirms UserName and Password from database and redirects to Books action
        //Otherwise Display "Username or password is incoreect." message on Login page
        public ActionResult Login(LoginModel objUser)
        {
            try
            {
                if (!this.IsCaptchaValid("Captcha is not valid"))
                {
                    ViewBag.Message = "Captcha do not match.Try again!";
                    return View(objUser);

                }

                if (ModelState.IsValid)
                {
                    Password decryptPassword = new Password();
                    using (Entities db = new Entities())
                    {
                        var obj = db.USERS.Where(a => a.USER_NAME == objUser.UserName).FirstOrDefault();
                        if (obj == null)
                        {
                            ViewBag.Message = "Username is incorrect.";
                        }
                        else if (decryptPassword.DecryptPassword(obj.PASSWORD).Equals(objUser.Password))
                        {
                            Session["UserID"] = obj.USER_ID.ToString();
                            Session["UserName"] = obj.USER_NAME.ToString();
                            return RedirectToAction("Books", "Book");
                        }
                        else
                        {
                            ViewBag.Message = " Password is incorrect.";
                        }
                    }
                }
                return View(objUser);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Logger.Error(ex);
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        
    }

    

    

}
