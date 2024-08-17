using BookShop.DAL;
using BookShop.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        public ActionResult GetAll()
        {
            var authors = _unitOfWork.AuthorRepo.GetAll().OrderBy(a => a.Name).ToList(); 
            return View(authors);
        }

        public ActionResult GetOne(int id)
        {
            var author = _unitOfWork.AuthorRepo.GetById(id); //context.Authors.FirstOrDefault(a => a.ID == id);
            return View(author);
        }





    }
}