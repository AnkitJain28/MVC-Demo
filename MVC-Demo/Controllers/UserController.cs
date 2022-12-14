using MVC_Demo.Common;
using MVC_Demo.DAL;
using MVC_Demo.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;

namespace MVC_Demo.Controllers
{
    public class UserController : Controller
    {
        Entities db = new Entities();
        public readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        // GET: User
        // Returns index view that has Registration Form
        public ActionResult Index()
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

        //Creates new user in database and redirects to Login 
        public ActionResult Create(UserModel model)
        {
            try
            {
                if (db.USERS.Where(u => u.USER_NAME == model.UserName).Any())
                {
                    ViewBag.Message = "This user name is already taken, try with another user name";
                    return View("~/Views/User/Index.cshtml");
                }
                if (ModelState.IsValid)
                {
                    if (!this.IsCaptchaValid("Captcha is not valid"))
                    {
                        return View("~/Views/User/Index.cshtml");

                    }


                    // RegisterDataLayer dal = new RegisterDataLayer();
                    // string message = dal.SignUpUser(model);
                    Password encryptPassword = new Password();
                    USER user = new USER();
                    user.USER_NAME = model.UserName;
                    user.FIRST_NAME = model.FirstName;
                    user.LAST_NAME = model.LastName;
                    user.MOBILE = model.Mobile;
                    user.EMAIL_ID = model.Email;
                    user.GENDER = model.Gender;
                    user.PASSWORD = encryptPassword.EncryptPassword(model.Password);
                    user.IS_ACTIVE = true;
                    db.USERS.Add(user);
                    db.SaveChanges();


                }
                else
                {
                    return View("~/Views/User/Index.cshtml");
                }

                return View();
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