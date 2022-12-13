using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Demo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}