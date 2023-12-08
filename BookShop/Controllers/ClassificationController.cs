using BookShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Controllers
{
    public class ClassificationController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Classification
        public ActionResult GetAll()
        {
            var list = context.Classifications.ToList().OrderBy(c=>c.Name);
            return View(list);
        }

        //public ActionResult GetOne(int id)
        //{
        //    var details = context.Classifications.FirstOrDefault(c => c.ID == id);
        //    return View(details);
        //}

        //[HttpGet]
        //public ActionResult AddNew()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AddNew(Classification classification)
        //{
        //    var classificationNames = context.Classifications.Select(c=>c.Name).ToList();
        //    if (!classificationNames.Contains(classification.Name))
        //    {
        //        context.Classifications.Add(classification);
        //        context.SaveChanges();
        //        return RedirectToAction("GetAll");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("Name", "This classification is already exists .");
        //        return View(classification);
        //    }

        //}


        //public ActionResult Edit(int id)
        //{
        //    var objClass = context.Classifications.FirstOrDefault(c => c.ID == id);
        //    return View(objClass);
        //}
        //[HttpPost]
        //public ActionResult Edit(int id,Classification newClassification)
        //{
        //    var obj = context.Classifications.FirstOrDefault(c => c.ID == id);
        //    obj.Name = newClassification.Name;
        //    context.SaveChanges();
        //    return RedirectToAction("GetAll");
        //}
        //[HttpPost]
        //public ActionResult Edit(int id, Classification newClassification)
        //{
        //    var allClassifications = context.Classifications.ToList();
        //    bool isExist = false;
        //    var obj = context.Classifications.FirstOrDefault(c => c.ID == id);
        //    obj.Name = newClassification.Name;
        //    foreach (var item in allClassifications)
        //    {
        //        if (obj.Name == item.Name && obj.ID != item.ID)
        //        {
        //            isExist = true;
        //        }
        //    }
        //    if (isExist == false)
        //    {
        //        context.SaveChanges();
        //        return RedirectToAction("GetAll");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("Name", "This classification is already exists .");
        //        return View(obj);
        //    }

        //}

        //public ActionResult Delete(int id)
        //{
        //    var obj = context.Classifications.FirstOrDefault(c => c.ID == id);
        //    context.Classifications.Remove(obj);
        //    context.SaveChanges();
        //    return Json(new { status = "Success" });
        //}
    }
}