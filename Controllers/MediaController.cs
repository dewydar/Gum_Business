using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GymBusiness.Models;
using GymBusiness.Models.ViewModels;

namespace GymBusiness.Controllers
{
    [Authorize]
    public class MediaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Media
        public ActionResult Index()
        {
            var media = db.Media.Include(m => m.Coach).Include(m => m.Player);
            return View(media.ToList());
        }

        // GET: Media/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = db.Media.Find(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }
        

        // GET: Media/Create
        public ActionResult Create()
        {
            ViewBag.Coach_id = new SelectList(db.Users, "Id", "Email");
            ViewBag.Player_id = new SelectList(db.Players, "ID", "Full_Name");
            return View();
        }

        // POST: Media/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( MediaVM media)
        {
            if (ModelState.IsValid)
            {
                Media m = new Media();
                string path = Server.MapPath("~/images/");
                media.Media_Path.SaveAs(path + media.Media_Path.FileName);
                m.Media_Path = media.Media_Path.FileName;
                m.Coach_id = media.Coach_id;
                m.Player_id = media.Player_id;
                db.Media.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Coach_id = new SelectList(db.Users, "Id", "Email", media.Coach_id);
            ViewBag.Player_id = new SelectList(db.Players, "ID", "Full_Name", media.Player_id);
            return View(media);
        }

        // GET: Media/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = db.Media.Find(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            ViewBag.Coach_id = new SelectList(db.Users, "Id", "Email", media.Coach_id);
            ViewBag.Player_id = new SelectList(db.Players, "ID", "Full_Name", media.Player_id);
            return View(media);
        }

        // POST: Media/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Media_Path,Player_id,Coach_id")] Media media)
        {
            if (ModelState.IsValid)
            {
                db.Entry(media).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Coach_id = new SelectList(db.Users, "Id", "Email", media.Coach_id);
            ViewBag.Player_id = new SelectList(db.Players, "ID", "Full_Name", media.Player_id);
            return View(media);
        }

        // GET: Media/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = db.Media.Find(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }

        // POST: Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Media media = db.Media.Find(id);
            db.Media.Remove(media);
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
