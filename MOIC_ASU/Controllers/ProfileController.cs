using MOIC_ASU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Dynamic;

namespace MOIC_ASU.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        public ActionResult MyProfile()
        {
            dynamic dy = new ExpandoObject();
            dy.AllArticales = GetArticales();
            dy.MyInfo = GetMyInfo();
            return View(dy);
        }
        public List<TempArticale> GetArticales()
        {
            var UserId = User.Identity.GetUserId();
            var MyArticales = db.TempArticales.Where(m => m.UserID == UserId).ToList();
            return MyArticales;
        }
        public ApplicationUser GetMyInfo()
        {
            var UserId = User.Identity.GetUserId();
            var MyAccount = db.Users.Where(m => m.Id == UserId).SingleOrDefault();
            return MyAccount;
        }
        // GET: 
        [HttpGet]
        public ActionResult PublishArticales()
        {
            var UserId = User.Identity.GetUserId();
            var myaricale = db.Articales.Where(a => a.UserID == UserId).ToList();
            return View(myaricale);
        }
        [HttpGet]
        public ActionResult NonPublishArticales()
        {
            var UserId = User.Identity.GetUserId();
            var myaricale = db.TempArticales.Where(a => a.UserID == UserId).ToList();
            return View(myaricale);
        }
    }
}