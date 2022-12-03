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
        // GET: User
        // Returns index view that has Registration Form
        public ActionResult Index()
        {
            return View();
        }

        //Creates new user in database and redirects to Login 
        public ActionResult Create(UserModel model)
        {
            if (ModelState.IsValid)
            {
                RegisterDataLayer dal = new RegisterDataLayer();
                string message = dal.SignUpUser(model);
            }
            else
            {
                return View("~/Views/User/Index.cshtml");
            }

            return View();
        }
    }
}