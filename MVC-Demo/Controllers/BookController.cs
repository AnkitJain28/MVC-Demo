using MVC_Demo.Models;
using NLog;
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
        public readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        // To Search input string in Books based on id or Name/Category
        public ActionResult Search(string q, string S)
        {
            var books = from b in db.BOOKS select b;
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
               
                int id = Convert.ToInt32(Request["SearchType"]);
                var searchParameter = "Search result for ";

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
                Logger.Error(ex);
                return View("Books", books);
            } 

            
        }

        // Returns list of books to view if session is active else redirects to login 
        public ActionResult Books()
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    return View(db.BOOKS.ToList());
                }
                else
                {
                    ViewBag.Message = "Username or password is incorrect.";
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Logger.Error(ex);
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // Returns view with book data to edit 
        public ActionResult Edit(int id)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                ViewBag.CategoryList = db.CATEGORies.Select(c => c.CATEGORY_NAME).ToList();
                ViewBag.StatusList = db.STATUS.Select(s => s.STATUS_NAME).ToList();
                var book = db.BOOKS.Where(b => b.BOOK_ID == id).FirstOrDefault();
                BookModel Book = new BookModel();
                Book.BookId = book.BOOK_ID;
                Book.BookName = book.BOOK_NAME;
                Book.Category = book.CATEGORY;
                Book.Status = book.STATUS;
                return View(Book);
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

        // To edit data and save to database
        public ActionResult Edit(BookModel Book)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                ViewBag.CategoryList = db.CATEGORies.Select(c => c.CATEGORY_NAME).ToList();
                ViewBag.StatusList = db.STATUS.Select(s => s.STATUS_NAME).ToList();
                if (ModelState.IsValid)
                {
                    var bk = db.BOOKS.Where(b => b.BOOK_ID == Book.BookId).FirstOrDefault();
                    bk.BOOK_NAME = Book.BookName;
                    bk.CATEGORY = Book.Category;
                    bk.STATUS = Book.Status;
                    bk.UPDATED_BY = Session["UserName"].ToString();
                    bk.UPDATE_TIMESTAMP = DateTime.Now;

                    db.SaveChanges();

                    return RedirectToAction("Books");
                }
                return View(Book);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Logger.Error(ex);
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        //redircts to delete view with book data to be deleted
        public ActionResult Delete(int id)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var book = db.BOOKS.Where(b => b.BOOK_ID == id).FirstOrDefault();
                BookModel Book = new BookModel();
                Book.BookId = book.BOOK_ID;
                Book.BookName = book.BOOK_NAME;
                Book.Category = book.CATEGORY;
                Book.Status = book.STATUS;
                return View(Book);
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

        // Deletes given book from database
        public ActionResult Delete(BookModel Book)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var bk = db.BOOKS.Where(b => b.BOOK_ID == Book.BookId).FirstOrDefault();
                bk.UPDATED_BY = Session["UserName"].ToString();
                db.SaveChanges();
                db.BOOKS.Remove(bk);
                db.SaveChanges();
                return RedirectToAction("Books");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Logger.Error(ex);
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        //Returns create view
        public ActionResult Create()
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                ViewBag.CategoryList = db.CATEGORies.Select(c => c.CATEGORY_NAME).ToList();
                ViewBag.StatusList = db.STATUS.Select(s => s.STATUS_NAME).ToList();
                return View();
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

        //Adds a new book to database based on data from create view
        public ActionResult Create(BookModel Book)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                ViewBag.CategoryList = db.CATEGORies.Select(c => c.CATEGORY_NAME).ToList();
                ViewBag.StatusList = db.STATUS.Select(s => s.STATUS_NAME).ToList();
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
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Logger.Error(ex);
                return View("~/Views/Shared/Error.cshtml");
            }

        }

    }
}