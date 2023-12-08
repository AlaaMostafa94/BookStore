using BookShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BookShop.Controllers
{
    public class BookController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult GetBooksByClassificationId(int classId)
        {
            List<Book> booksList = context.Books.Where(b => b.ClassificationID == classId).ToList();
            ViewBag.ClassificationName = context.Classifications.FirstOrDefault(c => c.ID == classId).Name;
            return View(booksList);
        }

        [Authorize]
        public ActionResult Download(string ImageName)
        {
            var FileVirtualPath = "~/Uploads/BooksContent/" + ImageName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));

           
        }

        //// GET: Book
        public ActionResult GetAll(int? page)
        {
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            var list = context.Books.ToList().OrderBy(c => c.Title);
            IPagedList<Book> booksPagedList = list.ToPagedList(pageIndex, 10);
            return View(booksPagedList);
        }
        
        public ActionResult GetOne(int id)
        {

            var book = context.Books.FirstOrDefault(b => b.ID == id);
            //List<Comment> comments = book.Comments.ToList();

            //Dictionary<string, string> bookComments = new Dictionary<string, string>();
            //foreach (var item in comments)
            //{
            //    bookComments.Add(item.User.UserName, item.Text);
            //}
            //ViewBag.CommentList = comments;
            return View(book);
        }

        //public ActionResult AddNew()
        //{
        //    ViewBag.classifications = context.Classifications.ToList();
        //    ViewBag.authors = context.Authors.ToList();
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult AddNew(Book book, HttpPostedFileBase bookCover, HttpPostedFileBase bookContent)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (bookCover == null)
        //        {
        //            book.CoverPicture = "images (8).jpg";
        //        }
        //        else if (bookCover.ContentLength > 0)
        //        {
        //            var fileName = Path.GetFileName(bookCover.FileName);
        //            var guid = Guid.NewGuid().ToString();
        //            var path = Path.Combine(Server.MapPath("~/Uploads/BooksCovers"), guid + fileName);

        //            bookCover.SaveAs(path);
        //            string fl = path.Substring(path.LastIndexOf("\\"));
        //            string[] split = fl.Split('\\');
        //            string newpath = split[1];
        //            //string imagepath = newpath; 
        //            book.CoverPicture = newpath;
        //        }


        //        else if (bookContent.ContentLength > 0)
        //        {
        //            var ContentfileName = Path.GetFileName(bookContent.FileName);
        //            var Contentguid = Guid.NewGuid().ToString();
        //            var Contentpath = Path.Combine(Server.MapPath("~/Uploads/BooksContent"), Contentguid + ContentfileName);

        //            bookContent.SaveAs(Contentpath);
        //            string Contentfl = Contentpath.Substring(Contentpath.LastIndexOf("\\"));
        //            string[] Contentsplit = Contentfl.Split('\\');
        //            string Contentnewpath = Contentsplit[1];
        //            //string imagepath = newpath; 
        //            book.Content = Contentnewpath;
        //        }
        //        book.DownloadNumber = 0;
        //        book.UploadDate = DateTime.Today;



        //        context.Books.Add(book);
        //        context.SaveChanges();
        //        return RedirectToAction("GetAll");
        //    }
        //    return View(book);
        //}

        //public ActionResult Edit(int id)
        //{
        //    var book = context.Books.FirstOrDefault(b => b.ID == id);
        //    ViewBag.classifications = context.Classifications.ToList();
        //    ViewBag.authors = context.Authors.ToList();
        //    return View(book);
        //}

        //[HttpPost]
        //public ActionResult Edit(int id, Book newBook, HttpPostedFileBase bookCover, HttpPostedFileBase bookContent)
        //{
        //    var book = context.Books.FirstOrDefault(a => a.ID == id);
        //    if (bookCover == null)
        //    {
        //        book.CoverPicture = context.Books.FirstOrDefault(a => a.ID == id).CoverPicture;//auth.ImagePath;
        //    }
        //    else if (bookCover.ContentLength > 0)
        //    {
        //        var fileName = Path.GetFileName(bookCover.FileName);
        //        var guid = Guid.NewGuid().ToString();
        //        var path = Path.Combine(Server.MapPath("~/Uploads/BooksCovers"), guid + fileName);

        //        bookCover.SaveAs(path);
        //        string fl = path.Substring(path.LastIndexOf("\\"));
        //        string[] split = fl.Split('\\');
        //        string newpath = split[1];
        //        //string imagepath = newpath;
        //        book.CoverPicture = newpath;
        //    }

        //    if (bookContent == null)
        //    {
        //        book.Content = context.Books.FirstOrDefault(a => a.ID == id).Content;//auth.ImagePath;
        //    }
        //    else if (bookContent.ContentLength > 0)
        //    {
        //        var ContentfileName = Path.GetFileName(bookContent.FileName);
        //        var Contentguid = Guid.NewGuid().ToString();
        //        var Contentpath = Path.Combine(Server.MapPath("~/Uploads/BooksContent"), Contentguid + ContentfileName);

        //        bookContent.SaveAs(Contentpath);
        //        string Contentfl = Contentpath.Substring(Contentpath.LastIndexOf("\\"));
        //        string[] Contentsplit = Contentfl.Split('\\');
        //        string Contentnewpath = Contentsplit[1];
        //        //string imagepath = newpath;
        //        book.Content = Contentnewpath;
        //    }

        //    book.AuthorID = newBook.AuthorID;
        //    book.ClassificationID = newBook.ClassificationID;
        //    book.Description = newBook.Description;
        //    book.Title = newBook.Title;

        //    context.SaveChanges();
        //    return RedirectToAction("GetAll");
        //}

        //public JsonResult Delete(int id)
        //{
        //    var book = context.Books.FirstOrDefault(b => b.ID == id);
        //    context.Books.Remove(book);
        //    context.SaveChanges();
        //    return Json(new { status = "Success" });
        //}
    }

    
}