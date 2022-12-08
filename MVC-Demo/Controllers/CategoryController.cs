using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Demo.Controllers
{

    public class CategoryController : Controller
    {
        Entities db = new Entities();


        // To Search input string in Categories based on id or Name
        public ActionResult Search(string q, string S)
        {
            var Cats = from c in db.Categories select c;
            int id = Convert.ToInt32(Request["SearchType"]);
            var searchParameter = "Search result for ";

            try
            {
                if (!string.IsNullOrWhiteSpace(q))
                {
                    switch (id)
                    {
                        case 0:
                            int iQ = int.Parse(q);
                            Cats = Cats.Where(c => c.CategoryId.Equals(iQ));
                            searchParameter += " Id = ' " + q + " '";
                            break;
                        case 1:
                            Cats = Cats.Where(b => b.CategoryName.Contains(q));
                            searchParameter += "Category Name contains ' " + q + " '";
                            break;

                    }
                }
                else
                {
                    searchParameter += "ALL";
                }
                ViewBag.SearchParameter = searchParameter;
                return View("CategoryList", Cats);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("CategoryList", Cats);
            }


        }

        // Returns list of Categories to view if session is active else redirects to login 
        public ActionResult CategoryList()
        {
            if (Session["UserID"] != null)
            {

                return View(db.Categories.ToList());
            }
            else
            {
                ViewBag.Message = "Username or password is incorrect.";
                return RedirectToAction("Login","Login");
            }
        }

        

        //Returns create view
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //Adds a new book to database based on data from create view
        public ActionResult Create(Category cat)
        {
            db.Categories.Add(cat);
            db.SaveChanges();
            return RedirectToAction("CategoryList");

        }

    }
}