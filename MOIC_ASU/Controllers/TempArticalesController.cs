using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MOIC_ASU.Models;

namespace MOIC_ASU.Controllers
{
    [Authorize]
    public class TempArticalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TempArticales
        public ActionResult Index()
        {
            return View(db.TempArticales.ToList());
        }

        // GET: TempArticales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempArticale tempArticale = db.TempArticales.Find(id);
            if (tempArticale == null)
            {
                return HttpNotFound();
            }
            return View(tempArticale);
        }
      
        [HttpGet]
        public ActionResult AddArticale(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempArticale tempArticale = db.TempArticales.Find(id);
            if (tempArticale == null)
            {
                return HttpNotFound();
            }
            return View(tempArticale);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddArticale(int id)
        {

            var tempArticale = db.TempArticales.Find(id);
            if (tempArticale == null)
            {
            
                return HttpNotFound();
            }
            if (tempArticale != null)
            {
                Articale table2 = new Articale 
                {
                    ArticaleTitle=tempArticale.ArticaleTitle,
                    ArticaleContent=tempArticale.ArticaleContent,
                    ArticaleCover=tempArticale.ArticaleCover,
                    CategoryId=tempArticale.CategoryId,
                    UserID=tempArticale.UserID
                };
                db.Articales.Add(table2);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Articales");
        }
        // GET: TempArticales/Create

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
        public ActionResult Create(TempArticale tempArticale, HttpPostedFileBase upload)
        {

            if (ModelState.IsValid)
            {

                var ArtId = User.Identity.GetUserId();
                tempArticale.UserID = ArtId;
                //to save Image in the server"Uploads"
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                // to save Image in database
                tempArticale.ArticaleCover = upload.FileName;
                db.TempArticales.Add(tempArticale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CAtegoryName", tempArticale.CategoryId);
            return View(tempArticale);
        }

        // GET: Articales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempArticale tempArticale = db.TempArticales.Find(id);
            if (tempArticale == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CAtegoryName", tempArticale.CategoryId);
            return View(tempArticale);
        }

        // POST: Articales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TempArticale tempArticale, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldpath = Path.Combine(Server.MapPath("~/Uploads"), tempArticale.ArticaleCover); ;
                if (upload != null)
                {
                    //هذة العمليه خاصة بمسح الصورة الجديدة من السيرفر
                    System.IO.File.Delete(oldpath);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                    upload.SaveAs(path);
                    tempArticale.ArticaleCover = upload.FileName;
                }
                db.Entry(tempArticale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CAtegoryName", tempArticale.CategoryId);
            return View(tempArticale);
        }

        // GET: TempArticales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempArticale tempArticale = db.TempArticales.Find(id);
            if (tempArticale == null)
            {
                return HttpNotFound();
            }
            return View(tempArticale);
        }

        // POST: TempArticales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TempArticale tempArticale = db.TempArticales.Find(id);
            db.TempArticales.Remove(tempArticale);
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
