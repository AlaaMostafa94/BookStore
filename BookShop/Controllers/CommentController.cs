using BookShop.DAL;
using BookShop.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Controllers
{
    public class CommentController : Controller
    {
        UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());

        // GET: Comment
        [HttpPost]
        public ActionResult AddComment(int bookId, string commentText)
        {

            Comment comment = new Comment()
            {
                BookID = bookId,
                Date = DateTime.Today,
                Text = commentText,
                ApplicationUserId = User.Identity.GetUserId()
            };
           _unitOfWork.CommentRepo.AddComment(comment);
            _unitOfWork.Save();
            //context.Comments.Add(comment);
            //context.SaveChanges();
            return RedirectToAction("GetOne", "Book", new { id = bookId });

        }
    }
}