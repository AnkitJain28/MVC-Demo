using MVC_Demo.Models;
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
            var books = from b in db.BOOKS select b;
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
                            books = books.Where(b => b.BOOK_ID.Equals(iQ));
                            searchParameter += " Id = ' " + q + " '";
                            break;
                        case 1:
                            books = books.Where(b => b.BOOK_NAME.Contains(q) || b.CATEGORY.Contains(q));
                            searchParameter += " Title/Category contains ' " + q + " '";
                            break;

                    }
                }
                else
                {
                    searchParameter += "ALL";
                }
                ViewBag.SearchParameter = searchParameter;
                return View("Books", books);
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Books", books);
            } 

            
        }

        // Returns list of books to view if session is active else redirects to login 
        public ActionResult Books()
        {
            if (Session["UserID"] != null)
            { 
        
                return View(db.BOOKS.ToList());
            }
            else
            {
                ViewBag.Message = "Username or password is incorrect.";
                return RedirectToAction("Login","Login");
            }
        }

        // Returns view with book data to edit 
        public ActionResult Edit(int id)
        {

            ViewBag.CategoryList = db.CATEGORies.Select(c => c.CATEGORY_NAME).ToList();
            ViewBag.StatusList = db.STATUS.Select(s => s.STATUS_NAME).ToList();
            var book = db.BOOKS.Where(b => b.BOOK_ID == id).FirstOrDefault();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // To edit data and save to database
        public ActionResult Edit(BOOK book)
        {
           
           
            var bk = db.BOOKS.Where(b => b.BOOK_ID == book.BOOK_ID).FirstOrDefault();
            bk.BOOK_NAME = book.BOOK_NAME;
            bk.CATEGORY = book.CATEGORY;
            bk.STATUS = book.STATUS;
            bk.UPDATED_BY = Session["UserName"].ToString();
            bk.UPDATE_TIMESTAMP = DateTime.Now;
            
            db.SaveChanges();

            return RedirectToAction("Books");
        }

        //redircts to delete view with book data to be deleted
        public ActionResult Delete(int id)
        {
            
            var book = db.BOOKS.Where(b => b.BOOK_ID == id).FirstOrDefault();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // Deletes given book from database
        public ActionResult Delete(BOOK book)
        {
            var bk = db.BOOKS.Where(b => b.BOOK_ID == book.BOOK_ID).FirstOrDefault();
            db.BOOKS.Remove(bk);
            db.SaveChanges();
            return RedirectToAction("Books");
        }

        //Returns create view
        public ActionResult Create()
        {
            ViewBag.CategoryList = db.CATEGORies.Select(c => c.CATEGORY_NAME).ToList();
            ViewBag.StatusList = db.STATUS.Select(s => s.STATUS_NAME).ToList();
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //Adds a new book to database based on data from create view
        public ActionResult Create(BookModel Book)
        {
            if (ModelState.IsValid)
            {
                BOOK book = new BOOK();
                book.BOOK_NAME = Book.BookName;
                book.CATEGORY = Book.Category;
                book.STATUS = Book.Status;
                book.CREATED_BY = Session["UserName"].ToString();
                book.CREATE_TIMESTAMP = DateTime.Now;
                db.BOOKS.Add(book);
                db.SaveChanges();
                return RedirectToAction("Books");
            }
            return View("Create");   

        }

    }
}