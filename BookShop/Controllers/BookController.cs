using BookShop.DAL;
using BookShop.Repository;
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
        UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());


        //// GET: Book
        public ActionResult GetAll(int? page)
        {
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            var list = _unitOfWork.BookRepo.GetAll().ToList().OrderBy(b => b.Title); 
            IPagedList<Book> booksPagedList = list.ToPagedList(pageIndex, 10);
            return View(booksPagedList);
        }
        
        public ActionResult GetOne(int id)
        {

            var book = _unitOfWork.BookRepo.GetById(id);

            return View(book);
        }




        [Authorize]
        public ActionResult Download(string FileName)
        {
            var FileVirtualPath = _unitOfWork.BookRepo.GetFilePath(FileName);
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));


        }







    }

    
}