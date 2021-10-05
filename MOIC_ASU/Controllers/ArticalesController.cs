using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MOIC_ASU.Models;

namespace MOIC_ASU.Controllers
{
    public class ArticalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Articales
        public ActionResult Index()
        {
            var articales = db.Articales.Include(a => a.Categories).OrderByDescending(a=>a.Id);
            return View(articales.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Session["ArticaleID"] = id;
            Articale articale = db.Articales.Find(id);
            if (articale == null)
            {
                return HttpNotFound();
            }
            dynamic dy = new ExpandoObject();
            dy.details = ArticaleDetails();
            dy.suggestionarticales = SuggestionArticales();
         
            return View(dy);
        }
        // GET: Articales/Details/5
        public Articale ArticaleDetails()
        {
      
           var id= (int)Session["ArticaleID"];
            Articale articale = db.Articales.Find(id);
            Session["WriterID"] = articale.UserID;
            Session["CategoryID"] = articale.CategoryId;
            
            return articale;
        }
        public List<Articale> SuggestionArticales()
        {
            var writerid = (string)Session["WriterID"];
            var categoryid = (int)Session["CategoryID"];
            var articaleid = (int)Session["ArticaleID"];
            var Sugg = db.Articales.Where(a=>a.UserID==writerid && a.Id != articaleid || a.CategoryId==categoryid &&a.Id!=articaleid).Take(2).OrderByDescending(a => a.Id).ToList();
            return Sugg;
        }


        // GET: Articales/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CAtegoryName");
            return View();
        }

        // POST: Articales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Articale articale,HttpPostedFileBase upload)
        {
           
            if (ModelState.IsValid)
            {

               
                //to save Image in the server"Uploads"
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                // to save Image in database
                articale.ArticaleCover = upload.FileName;
               //لربط المقال بالناشر من خلال اضافة الاي دي الخاص به
                var ArtId = User.Identity.GetUserId();
                articale.UserID = ArtId;
                db.Articales.Add(articale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CAtegoryName", articale.CategoryId);
            return View(articale);
        }

        // GET: Articales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articale articale = db.Articales.Find(id);
            if (articale == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CAtegoryName", articale.CategoryId);
            return View(articale);
        }

        // POST: Articales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Articale articale, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldpath = Path.Combine(Server.MapPath("~/Uploads"), articale.ArticaleCover); ;
                if (upload != null)
                {
                    //هذة العمليه خاصة بمسح الصورة الجديدة من السيرفر
                    System.IO.File.Delete(oldpath);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                    upload.SaveAs(path);
                    articale.ArticaleCover = upload.FileName;
                }
                db.Entry(articale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CAtegoryName", articale.CategoryId);
            return View(articale);
        }

        // GET: Articales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articale articale = db.Articales.Find(id);
            if (articale == null)
            {
                return HttpNotFound();
            }
            return View(articale);
        }

        // POST: Articales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articale articale = db.Articales.Find(id);
            db.Articales.Remove(articale);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
