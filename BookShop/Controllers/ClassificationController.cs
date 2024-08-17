using BookShop.DAL;
using BookShop.Models;
using BookShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Controllers
{
    public class ClassificationController : Controller
    {
        UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());


        
        // GET: Classification
        public ActionResult GetAll()
        {
            var list = _unitOfWork.ClassificationRepo.GetAll().OrderBy(c => c.Name); 
            return View(list);
        }

        public ActionResult GetOne(int id)
        {
            var details = _unitOfWork.ClassificationRepo.GetById(id); 
            return View(details);
        }


    }
}