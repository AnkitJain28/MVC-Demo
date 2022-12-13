using MVC_Demo.Common;
using MVC_Demo.DAL;
using MVC_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Demo.Controllers
{
    public class UserController : Controller
    {
        Entities db = new Entities();
        // GET: User
        // Returns index view that has Registration Form
        public ActionResult Index()
        {
            return View();
        }

        //Creates new user in database and redirects to Login 
        public ActionResult Create(UserModel model)
        {

            if (db.USERS.Where(u => u.USER_NAME == model.UserName).Any())
            {
                ViewBag.Message = "This user name is already taken, try with another user name";
                return View("~/Views/User/Index.cshtml");
            }
            if (ModelState.IsValid)
            {
                
                
                    // RegisterDataLayer dal = new RegisterDataLayer();
                    // string message = dal.SignUpUser(model);
                    Password encryptPassword = new Password();
                    USER user= new USER();
                    user.USER_NAME = model.UserName;
                    user.FIRST_NAME= model.FirstName;
                    user.LAST_NAME= model.LastName;
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
    }
}