using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcBlog.Models;
using System.Web.Helpers;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace MvcBlog.Controllers
{
    public class AdminUyelerController : Controller
    {
        private mvcblogEntities db = new mvcblogEntities();

        // GET: /Uyeler/
        public ActionResult Index()
        {
            var uye = db.Uyes.Include(u => u.Yetki);
            return View(uye.OrderByDescending(u=>u.UyeId).ToList());
        }

        // GET: /Uyeler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UyeTablo uyetablo = db.Uyes.Find(id);
            if (uyetablo == null)
            {
                return HttpNotFound();
            }
            return View(uyetablo);
        }

        // GET: /Uyeler/Create
        public ActionResult Create()
        {
            ViewBag.YetkiId = new SelectList(db.Yetkis, "YetkiId", "Yetkisi");
            return View();
        }

        // POST: /Uyeler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UyeId,KullaniciAdi,Email,Sifre,AdSoyad,Foto,YetkiId")] UyeTablo uyetablo)
        {
            if (ModelState.IsValid)
            {
                db.Uyes.Add(uyetablo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.YetkiId = new SelectList(db.Yetkis, "YetkiId", "Yetkisi", uyetablo.YetkiId);
            return View(uyetablo);
        }

        // GET: /Uyeler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UyeTablo uyetablo = db.Uyes.Find(id);
            if (uyetablo == null)
            {
                return HttpNotFound();
            }
            ViewBag.YetkiId = new SelectList(db.Yetkis, "YetkiId", "Yetkisi", uyetablo.YetkiId);
            return View(uyetablo);
        }

        // POST: /Uyeler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UyeId,KullaniciAdi,Email,Sifre,AdSoyad,Foto,YetkiId")] UyeTablo uyetablo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uyetablo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.YetkiId = new SelectList(db.Yetkis, "YetkiId", "Yetkisi", uyetablo.YetkiId);
            return View(uyetablo);
        }

        // GET: /Uyeler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UyeTablo uyetablo = db.Uyes.Find(id);
            if (uyetablo == null)
            {
                return HttpNotFound();
            }
            return View(uyetablo);
        }

        // POST: /Uyeler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UyeTablo uyetablo = db.Uyes.Find(id);
            db.Uyes.Remove(uyetablo);
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
