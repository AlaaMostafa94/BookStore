using BookShop.Models;
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
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult GetAll()
        {
            var authors = context.Authors.OrderBy(a=>a.Name).ToList();
            return View(authors);
        }

        public ActionResult GetOne(int id)
        {
            var author = context.Authors.FirstOrDefault(a => a.ID == id);
            return View(author);
        }
        [HttpGet]
        public ActionResult AddNew()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNew(Author author,HttpPostedFileBase file)
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




                //context.Authors.Add(author);
                //context.SaveChanges();

                //string _FileName = Path.GetFileName(file.FileName);
                //string _path = Path.Combine(Server.MapPath("~/Uploads/AuthorsPhotos"), _FileName);
                //file.SaveAs(_path);
            }

            //else
            //{
            //    
            //}
            context.Authors.Add(author);
            context.SaveChanges();
            return RedirectToAction("GetAll");
            //context.Authors.Add(author);
            //context.SaveChanges();
            //return RedirectToAction("GetAll");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var auth=context.Authors.FirstOrDefault(a => a.ID == id);
            return View(auth);
        }
        [HttpPost]
        public ActionResult Edit(int id,Author newAuthor,HttpPostedFileBase file)
        {
            var auth = context.Authors.FirstOrDefault(a => a.ID == id);
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
                context.SaveChanges();
                return RedirectToAction("GetAll");
        }

        public JsonResult Delete(int id)
        {
            var auth = context.Authors.FirstOrDefault(a => a.ID == id);
            context.Authors.Remove(auth);
            context.SaveChanges();
            return Json(new { status = "Success" });
        }
    }
}