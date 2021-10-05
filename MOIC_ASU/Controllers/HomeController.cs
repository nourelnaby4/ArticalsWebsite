using Microsoft.AspNet.Identity;
using MOIC_ASU.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MOIC_ASU.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        

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
        #region
        [HttpGet]
        public ActionResult WriterPage()
        {
            
            dynamic dy = new ExpandoObject();
            dy.AllArticales = GetArticales();
            dy.MyInfo = GetMyInfo();
            return View(dy);
        }

        public List<Articale> GetArticales()
        {
            var MyId = (string)Session["ID"];
            var Writer = db.Articales.Where(u=>u.UserID == MyId).ToList();
           // var WriterArticales = db.Articales.Where(a => a.UserID == WriterId.ToString()).ToList();
            return Writer;
        }
        public ApplicationUser GetMyInfo()
        {
            var MyId = (string)Session["ID"];
             var Writer = db.Users.Find(MyId);
        
          
           // var WriterInfo = db.Users.Where(m => m.Id == Writer.UserID).SingleOrDefault();
            
            //var WriterInfo = db.Articales.Where(u => u.User.Id == u.UserID).FirstOrDefault();
            return Writer;
        }
        #endregion
        #region
        public ActionResult Index()
        {
            dynamic dy = new ExpandoObject();
            dy.articales = Articales();

            return View(dy);
        }
        public List<Articale> Articales()
        {
            var NewArticales = db.Articales.Take(4).OrderByDescending(a=>a.Id).ToList();
            return NewArticales;

        }

        #endregion
        [HttpGet]
        public ActionResult EditHome()
        {

            return View();
        }
        [HttpPost]
        public ActionResult EditHomepost()
        {

            return View();
        }

    }
}