using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Demo.Controllers
{
    public class BookController : Controller
    {
        Entities db = new Entities();
        

        // To Search input string in Books based on id or Name/Category
        public ActionResult Search(string q, string S)
        {
            var books = from b in db.Books select b;
            int id = Convert.ToInt32(Request["SearchType"]);
            var searchParameter = "Search result for ";

            if (!string.IsNullOrWhiteSpace(q))
            {
                switch (id)
                {
                    case 0:
                        int iQ = int.Parse(q);
                        books = books.Where(b => b.BookId.Equals(iQ));
                        searchParameter += " Id = ' " + q + " '";
                        break;
                    case 1:
                        books = books.Where(b => b.Name.Contains(q) || b.Category.Contains(q));
                        searchParameter += " Title/Category contains ' " + q + " '";
                        break;

                }
            }
            else
            {
                searchParameter += "ALL";
            }
            ViewBag.SearchParameter = searchParameter;
            return View("Books",books);
        }

        // Returns list of books to view if session is on else redirects to login 
        public ActionResult Books()
        {
            if (Session["UserID"] != null)
            { 
        
                return View(db.Books.ToList());
            }
            else
            {
                ViewBag.Message = "Username or password is incorrect.";
                return RedirectToAction("Login");
            }
        }

        // Returns view with book data to edit 
        public ActionResult Edit(int id)
        {

            ViewBag.CategoryList = db.Categories.Select(c => c.CategoryName).ToList();
            var book = db.Books.Where(b => b.BookId == id).FirstOrDefault();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // To edit data and save to database
        public ActionResult Edit(Book book)
        {
           
           
            var bk = db.Books.Where(b => b.BookId == book.BookId).FirstOrDefault();
            bk.Name = book.Name;
            bk.Category = book.Category;
            
            db.SaveChanges();

            return RedirectToAction("Books");
        }

        //redircts to delete view with book data to be deleted
        public ActionResult Delete(int id)
        {
            
            var book = db.Books.Where(b => b.BookId == id).FirstOrDefault();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // Deletes given book from database
        public ActionResult Delete(Book book)
        {
            var bk = db.Books.Where(b => b.BookId == book.BookId).FirstOrDefault();
            db.Books.Remove(bk);
            db.SaveChanges();
            return RedirectToAction("Books");
        }

        //Returns create view
        public ActionResult Create()
        {
            ViewBag.CategoryList = db.Categories.Select(c => c.CategoryName).ToList();
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //Adds a new book to database based on data from create view
        public ActionResult Create(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return RedirectToAction("Books");

        }

    }
}