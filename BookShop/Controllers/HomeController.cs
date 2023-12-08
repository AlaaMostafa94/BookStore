using BookShop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Controllers
{
    public class HomeController : Controller
    {
        //public void AddRole()
        //{
        //    ApplicationDbContext context = new ApplicationDbContext();
        //    RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
        //    RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(store);
        //    manager.Create(new IdentityRole("Member"));
        //}
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}