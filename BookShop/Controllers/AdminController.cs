using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;
using BookShop.CustomAttributes;
using BookShop.DAL;
using BookShop.Repository;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Ajax.Utilities;

namespace BookShop.Controllers
{


    [CustomAuthorize(Roles ="Admin",LoginPage ="/Account/AdminLogin")]
    public class AdminController : Controller
    {
        UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());



        public AdminController()
        {

        }


        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllClassifications(int? page)
        {
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            var list = _unitOfWork.ClassificationRepo.GetAll(); 
            IPagedList<Classification> classificationsPagedList = list.ToPagedList(pageIndex, 3);
            return View(classificationsPagedList);
        }

        public ActionResult GetClassificationById(int id)
        {
            var details = _unitOfWork.ClassificationRepo.GetById(id); 
            return View(details);
        }

        [HttpGet]
        public ActionResult AddNewClassification()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewClassification(Classification classification)
        {
            Classification searchClassificationName = _unitOfWork.ClassificationRepo.GetByName(classification.Name); 
            if (ModelState.IsValid)
            {
                if (searchClassificationName == null)
                {
                    _unitOfWork.ClassificationRepo.Insert(classification); 
                    _unitOfWork.Save();
                    return RedirectToAction("GetAllClassifications");
                }
                else
                {
                    ModelState.AddModelError("Name", "This Name is already exists");
                    return View(classification);
                }

            }
            
                return View(classification);
            

        }

        public ActionResult EditClassification(int id)
        {
            var objClass = _unitOfWork.ClassificationRepo.GetById(id);
            return View(objClass);
        }

        [HttpPost]
        public ActionResult EditClassification(Classification classification)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ClassificationRepo.Update(classification); 
                _unitOfWork.Save();
                return RedirectToAction("GetAllClassifications");


            }
            else
            {
                return View(classification);
            }


        }

        public ActionResult DeleteClassification(int id)
        {
            IEnumerable<Book> books = _unitOfWork.BookRepo.GetAll().Where(b => b.ClassificationID == id); 
            foreach (var item in books)
            {
                _unitOfWork.BookRepo.Delete(item.ID);
                _unitOfWork.Save();
            }
            _unitOfWork.ClassificationRepo.Delete(id);
            _unitOfWork.Save();
            return Json(new { status = "Success" });
        }

        public ActionResult GetAllAuthors(int? page)


        {
            IEnumerable<Author> AuthorsList = _unitOfWork.AuthorRepo.GetAll(); 
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Author> pagedAuthors = AuthorsList.ToPagedList(pageIndex, 3);
            return View(pagedAuthors);
        }

        public ActionResult GetAuthorById(int id)
        {
            Author author = _unitOfWork.AuthorRepo.GetById(id); 
            return View(author);
        }

        public ActionResult AddNewAuthor()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddNewAuthor(Author author, HttpPostedFileBase file)
        {

            Author searchAuthor = _unitOfWork.AuthorRepo.GetByName(author.Name);

            if (ModelState.IsValid)
            {
                if (searchAuthor == null)
                {
                    _unitOfWork.AuthorRepo.Insert(author,file);
                    _unitOfWork.Save();
                    return RedirectToAction("GetAllAuthors");

                }
                else
                {
                    ModelState.AddModelError("Name", "This author is already exists");
                    return View(author);
                }
            }
            return View(author);

        }


        public ActionResult EditAuthor(int id)
        {
            Author author = _unitOfWork.AuthorRepo.GetById(id); 
            return View(author);
        }


         
        [HttpPost]
        public ActionResult EditAuthor(Author author,int id, HttpPostedFileBase file)
        {


            if (ModelState.IsValid)
            {


                _unitOfWork.AuthorRepo.Update(author,id,file);
                _unitOfWork.Save();
                return RedirectToAction("GetAllAuthors");


            }
            return View(author);
        }


        public JsonResult DeleteAuthor(int id)
        {

            IEnumerable<Book> authorBooks = _unitOfWork.BookRepo.GetAll().Where(b => b.AuthorID == id); 
            foreach (var item in authorBooks)
            {
                _unitOfWork.BookRepo.Delete(item.ID);
                _unitOfWork.Save();

            }
            _unitOfWork.AuthorRepo.Delete(id);
            _unitOfWork.Save();
            return Json(new { status = "Success" });



        }

        public ActionResult GetAllBooks(int? page)
        {
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IEnumerable<Book> booksList = _unitOfWork.BookRepo.GetAll(); 
            IPagedList<Book> pagedBooksList = booksList.ToPagedList(pageIndex, 3);
            return View(pagedBooksList);
        }

        public ActionResult GetBookById(int id)
        {
            Book book = _unitOfWork.BookRepo.GetById(id);
            return View(book);
        }

        public ActionResult AddNewBook()
        {
            ViewBag.authors = _unitOfWork.AuthorRepo.GetAll();
            ViewBag.classifications = _unitOfWork.ClassificationRepo.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult AddNewBook(Book book, HttpPostedFileBase coverPictureFile, HttpPostedFileBase bookContentFile)
        {

            ViewBag.authors = _unitOfWork.AuthorRepo.GetAll(); 
            ViewBag.classifications = _unitOfWork.ClassificationRepo.GetAll(); 

            Book searchBook = _unitOfWork.BookRepo.GetByTitle(book.Title);
            if (ModelState.IsValid)
            {
                if (bookContentFile == null)
                {
                    ModelState.AddModelError("Content", "please, upload the book");
                    return View(book);
                }


                if (searchBook == null)
                {
                    _unitOfWork.BookRepo.Insert(book, coverPictureFile,bookContentFile); 
                    _unitOfWork.Save();
                    return RedirectToAction("GetAllBooks");
                }

                else
                {
                    ModelState.AddModelError("Title", "This book is alraedy exist");
                    return View(book);
                }
            }
            return View(book);


        }


        public ActionResult EditBook(int id)
        {
            ViewBag.authors = _unitOfWork.AuthorRepo.GetAll();
            ViewBag.classifications = _unitOfWork.ClassificationRepo.GetAll();
            var book = _unitOfWork.BookRepo.GetById(id);
            return View(book);
        }

        [HttpPost]
        public ActionResult EditBook(Book book,int id, HttpPostedFileBase coverPictureFile, HttpPostedFileBase bookContentFile)
        {
            ViewBag.authors = _unitOfWork.AuthorRepo.GetAll(); 
            ViewBag.classifications = _unitOfWork.ClassificationRepo.GetAll();
            if (ModelState.IsValid)
            {
                //if (coverPictureFile == null)
                //{
                //    book.CoverPicture = _bookRepository.GetById(book.ID).CoverPicture;
                //}
                //else 
                //{
                //    string newCoverPicturePath = UploadFile(coverPictureFile, "~/Uploads/BooksCovers");
                //    string coverPictureName = GetFileName(newCoverPicturePath);
                //    book.CoverPicture = coverPictureName;
                //}


                //if (bookContentFile == null)
                //{
                //    book.Content = _bookRepository.GetById(book.ID).Content;
                //}
                //else 
                //{
                //    string contentPath = UploadFile(bookContentFile, "~/Uploads/BooksContent");
                //    string contentFileName = GetFileName(contentPath);
                //    book.Content = contentFileName;
                //}
                _unitOfWork.BookRepo.Update(book,id,coverPictureFile,bookContentFile);
                _unitOfWork.Save();
                return RedirectToAction("GetAllBooks");

            }

            return View(book);

        }



        public JsonResult DeleteBook(int id)
        {
            _unitOfWork.BookRepo.Delete(id);
            _unitOfWork.Save();
            return Json(new { status = "Success" });

        }






    }
}