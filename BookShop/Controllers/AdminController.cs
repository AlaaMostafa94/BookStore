using BookShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;
using BookShop.CustomAttributes;

namespace BookShop.Controllers
{
    [CustomAuthorize(Roles ="Admin",LoginPage ="/Account/AdminLogin")]
    public class AdminController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllClassifications(int? page)
        {
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            var list = context.Classifications.ToList().OrderBy(c => c.Name);
            IPagedList<Classification> classificationsPagedList = list.ToPagedList(pageIndex,3);
            return View(classificationsPagedList);
        }

        public ActionResult GetClassificationById(int id)
        {
            var details = context.Classifications.FirstOrDefault(c => c.ID == id);
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
            var classificationNames = context.Classifications.Select(c => c.Name).ToList();
            if (ModelState.IsValid)
            {
                if (!classificationNames.Contains(classification.Name))
                {
                    context.Classifications.Add(classification);
                    context.SaveChanges();
                    return RedirectToAction("GetAllClassifications");
                }
                else
                {
                    ModelState.AddModelError("Name", "This classification is already exists .");
                    return View(classification);
                }
            }
            return View(classification);


        }

        public ActionResult EditClassification(int id)
        {
            var objClass = context.Classifications.FirstOrDefault(c => c.ID == id);
            return View(objClass);
        }
   
        [HttpPost]
        public ActionResult EditClassification(int id, Classification newClassification)
        {
            var allClassifications = context.Classifications.ToList();
            bool isExist = false;
            var obj = context.Classifications.FirstOrDefault(c => c.ID == id);
            if (ModelState.IsValid)
            {
                obj.Name = newClassification.Name;

                foreach (var item in allClassifications)
                {
                    if (obj.Name == item.Name && obj.ID != item.ID)
                    {
                        isExist = true;
                    }
                }
                if (isExist == false)
                {
                    context.SaveChanges();
                    return RedirectToAction("GetAllClassifications");
                }
                else
                {
                    ModelState.AddModelError("Name", "This classification is already exists .");
                    return View(obj);
                }
            }
            return View(newClassification);


        }

        public ActionResult DeleteClassification(int id)
        {
            var obj = context.Classifications.FirstOrDefault(c => c.ID == id);
            var books = context.Books.Where(x => x.ClassificationID == id).ToList();
            foreach(var item in books)
            {
                context.Books.Remove(item);
                context.SaveChanges();
            }
            context.Classifications.Remove(obj);
            context.SaveChanges();
            return Json(new { status = "Success" });
        }

        public ActionResult GetAllAuthors(int? page)
        {
            List<Author> AuthorsList = context.Authors.ToList();
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Author> pagedAuthors = AuthorsList.ToPagedList(pageIndex, 3);
            return View(pagedAuthors);
        }

        public ActionResult GetAuthorById(int id)
        {
            Author author = context.Authors.FirstOrDefault(a => a.ID == id);
            return View(author);
        }

        public ActionResult AddNewAuthor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewAuthor(Author author,HttpPostedFileBase file)
        {
            if (file == null)
            {
                author.ImagePath = "images.png";
            }
            else if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var guid = Guid.NewGuid().ToString();
                var path = Path.Combine(Server.MapPath("~/Uploads/AuthorsPhotos"), guid + fileName);

                file.SaveAs(path);
                string fl = path.Substring(path.LastIndexOf("\\"));
                string[] split = fl.Split('\\');
                string newpath = split[1];
                //string imagepath = newpath; 
                author.ImagePath = newpath;

            }
            var AuthorsNames = context.Authors.Select(c => c.Name).ToList();

            if (ModelState.IsValid)
            {
                if (!AuthorsNames.Contains(author.Name))
                {
                    context.Authors.Add(author);
                    context.SaveChanges();
                    return RedirectToAction("GetAllAuthors");

                }
                else
                {
                    ModelState.AddModelError("", "This author is already exists");
                    return View(author);
                }
            }
            return View(author);

        }

        public ActionResult EditAuthor(int id)
        {
            Author author = context.Authors.FirstOrDefault(a => a.ID == id);
            return View(author);
        }

        [HttpPost]
        public ActionResult EditAuthor(int id,Author newAuthor,HttpPostedFileBase file)
        {
            var allAuthors = context.Authors.ToList();
            bool isExist = false;
            var auth = context.Authors.FirstOrDefault(a => a.ID == id);
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    auth.ImagePath = context.Authors.FirstOrDefault(a => a.ID == id).ImagePath;//auth.ImagePath;
                }
                else if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var guid = Guid.NewGuid().ToString();
                    var path = Path.Combine(Server.MapPath("~/Uploads/AuthorsPhotos"), guid + fileName);

                    file.SaveAs(path);
                    string fl = path.Substring(path.LastIndexOf("\\"));
                    string[] split = fl.Split('\\');
                    string newpath = split[1];
                    //string imagepath = newpath;
                    auth.ImagePath = newpath;
                }


                auth.About = newAuthor.About;
                //    //auth.ImagePath = newAuthor.ImagePath;
                auth.Name = newAuthor.Name;
                foreach (var item in allAuthors)
                {
                    if (auth.Name == item.Name && auth.ID != item.ID)
                    {
                        isExist = true;
                    }
                }

                if (isExist == false)
                {
                    context.SaveChanges();
                    return RedirectToAction("GetAllAuthors");
                }
                else
                {
                    ModelState.AddModelError("Name", "This author is already exists .");
                    return View(auth);
                }

            }
            return View(newAuthor);
        }


        public JsonResult DeleteAuthor(int id)
        {
            var auth = context.Authors.FirstOrDefault(a => a.ID == id);
            List<Book> authorBooks = context.Books.Where(b => b.AuthorID == id).ToList();
            foreach (var item in authorBooks)
            {
                context.Books.Remove(item);
                context.SaveChanges();
            }
            context.Authors.Remove(auth);
            context.SaveChanges();
            return Json(new { status = "Success" });
        }

        public ActionResult GetAllBooks(int? page)
        {
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            List<Book> booksList = context.Books.ToList();
            IPagedList<Book> pagedBooksList = booksList.ToPagedList(pageIndex, 3);
            return View(pagedBooksList);
        }

        public ActionResult GetBookById(int id)
        {
            var book = context.Books.FirstOrDefault(b => b.ID == id);
            return View(book);
        }

        public ActionResult AddNewBook()
        {
            ViewBag.authors = context.Authors.ToList();
            ViewBag.classifications = context.Classifications.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddNewBook(Book book,HttpPostedFileBase coverPictureFile,HttpPostedFileBase bookContentFile)
        {
            if (coverPictureFile == null)
            {
                book.CoverPicture = "images (8).jpg";
            }
            else if (coverPictureFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(coverPictureFile.FileName);
                var guid = Guid.NewGuid().ToString();
                var path = Path.Combine(Server.MapPath("~/Uploads/BooksCovers"), guid + fileName);

                coverPictureFile.SaveAs(path);
                string fl = path.Substring(path.LastIndexOf("\\"));
                string[] split = fl.Split('\\');
                string newpath = split[1];
                //string imagepath = newpath; 
                book.CoverPicture = newpath;

            }

            if (bookContentFile == null)
            {
                book.Content = null;
            }
            else if (bookContentFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(bookContentFile.FileName);
                var guid = Guid.NewGuid().ToString();
                var path = Path.Combine(Server.MapPath("~/Uploads/BooksContent"), guid + fileName);

                bookContentFile.SaveAs(path);
                string fl = path.Substring(path.LastIndexOf("\\"));
                string[] split = fl.Split('\\');
                string newpath = split[1];
                //string imagepath = newpath; 
                book.Content = newpath;

            }
            ViewBag.authors = context.Authors.ToList();
            ViewBag.classifications = context.Classifications.ToList();
            book.DownloadNumber = 0;
            book.UploadDate = DateTime.Today;
            var allBooks = context.Books.Select(b => b.Title).ToList();
            if (ModelState.IsValid)
            {
                if (book.Content == null)
                {
                    ModelState.AddModelError("Content", "please, upload the book");
                    return View(book);
                }


                if (!allBooks.Contains(book.Title))
                {
                    context.Books.Add(book);
                    context.SaveChanges();
                    return RedirectToAction("GetAllBooks");
                }

                else
                {
                    ModelState.AddModelError("Title", "This book is alraedy exist");
                    return View(book);
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(book);


        }


        public ActionResult EditBook(int id)
        {
            ViewBag.authors = context.Authors.ToList();
            ViewBag.classifications = context.Classifications.ToList();
            var book = context.Books.FirstOrDefault(b => b.ID == id);
            return View(book);
        }

        [HttpPost]
        public ActionResult EditBook(int id,Book newBook, HttpPostedFileBase coverPictureFile, HttpPostedFileBase bookContentFile)
        {
            var allBooks = context.Books.ToList();
            bool isExist = false;
            ViewBag.authors = context.Authors.ToList();
            ViewBag.classifications = context.Classifications.ToList();
            var book = context.Books.FirstOrDefault(b => b.ID == id);
            if (ModelState.IsValid)
            {
                if (coverPictureFile == null)
                {
                    book.CoverPicture = context.Books.FirstOrDefault(a => a.ID == id).CoverPicture;//auth.ImagePath;
                }
                else if (coverPictureFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(coverPictureFile.FileName);
                    var guid = Guid.NewGuid().ToString();
                    var path = Path.Combine(Server.MapPath("~/Uploads/BooksCovers"), guid + fileName);

                    coverPictureFile.SaveAs(path);
                    string fl = path.Substring(path.LastIndexOf("\\"));
                    string[] split = fl.Split('\\');
                    string newpath = split[1];
                    //string imagepath = newpath;
                    book.CoverPicture = newpath;
                }


                if (bookContentFile == null)
                {
                    book.Content = context.Books.FirstOrDefault(a => a.ID == id).Content;//auth.ImagePath;
                }
                else if (bookContentFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(bookContentFile.FileName);
                    var guid = Guid.NewGuid().ToString();
                    var path = Path.Combine(Server.MapPath("~/Uploads/BooksContent"), guid + fileName);

                    bookContentFile.SaveAs(path);
                    string fl = path.Substring(path.LastIndexOf("\\"));
                    string[] split = fl.Split('\\');
                    string newpath = split[1];
                    //string imagepath = newpath;
                    book.Content = newpath;
                }

                book.Title = newBook.Title;
                //    //auth.ImagePath = newAuthor.ImagePath;
                book.Description = newBook.Description;
                book.AuthorID = newBook.AuthorID;
                book.ClassificationID = newBook.ClassificationID;
                foreach (var item in allBooks)
                {
                    if (book.Title == item.Title && book.ID != item.ID)
                    {
                        isExist = true;
                    }
                }
                if (isExist == false)
                {
                    context.SaveChanges();
                    return RedirectToAction("GetAllBooks");
                }
                else
                {
                    ModelState.AddModelError("Title", "This book is already exists .");
                    return View(book);
                }
            }

            return View(newBook);

        }
        public JsonResult DeleteBook(int id)
        {
            var book = context.Books.FirstOrDefault(b => b.ID == id);
            context.Books.Remove(book);
            context.SaveChanges();
            return Json(new { status = "Success" });

        }
    }
}