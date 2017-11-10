using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBlog.Models;
using System.IO;
using System.Web.Helpers;
using System.ComponentModel.DataAnnotations;

namespace MvcBlog.Controllers
{
    public class UyeController : Controller
    {
        mvcblogEntities db = new mvcblogEntities();
        // GET: /Uye/
        public ActionResult Index(int id)
        {
            var uye = db.Uyes.Where(u => u.UyeId == id).SingleOrDefault();
            if (Convert.ToInt32(Session["uyeid"]) != uye.UyeId)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        public ActionResult Login()
        {           
            return View();
        }
        [HttpPost]
        public ActionResult Login(UyeTablo uye)
        {
            var login = db.Uyes.Where(u => u.KullaniciAdi == uye.KullaniciAdi).SingleOrDefault();
            if (login.KullaniciAdi==uye.KullaniciAdi && login.Sifre==uye.Sifre)
            {
                Session["uyeId"] = login.UyeId;
                Session["kullaniciAdi"] = login.KullaniciAdi;
                Session["yetkiId"] = login.YetkiId;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.Uyari = "Kullanıcı Adı veya Şifreniz hatalıdır.";
                return View();
            }          
        }

        public ActionResult Logout()
        {
            Session["uyeId"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult Create(UyeTablo uye, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto!=null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(150, 150);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    uye.Foto = "/Uploads/UyeFoto/" + newfoto;
                    uye.YetkiId = 4;
                    db.Uyes.Add(uye);
                    db.SaveChanges();
                    Session["uyeId"] = uye.UyeId;
                    Session["kullaniciAdi"] = uye.KullaniciAdi;
                    return RedirectToAction("Index", "Home");                  
                }
                else
                {
                    ModelState.AddModelError("Fotoğraf Eklemeniz Gerekmektedir.", "Fotoğraf Seçiniz.");
                }
            }
            ModelState.Clear();
            ViewBag.mesaj = "Üye kaydınız tamamlandı.";
            return View(uye);
        }

        public ActionResult Edit(int id)
        {
            var uye = db.Uyes.Where(u => u.UyeId == id).SingleOrDefault();
            if (Convert.ToInt32(Session["uyeId"])!=uye.UyeId)
            {
                return HttpNotFound();
            }
            return View(uye);
        }
        [HttpPost]
        public ActionResult Edit(UyeTablo uye, int id, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                var uyes = db.Uyes.Where(u => u.UyeId == id).SingleOrDefault();
                if (Foto!=null)
                {
                     if (System.IO.File.Exists(Server.MapPath(uyes.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(uyes.Foto));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(150, 150);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    uyes.Foto = "/Uploads/UyeFoto/" + newfoto;
                }
                    uyes.AdSoyad = uyes.AdSoyad;
                    uyes.KullaniciAdi = uye.KullaniciAdi;
                    uyes.Sifre = uyes.Sifre;
                    uyes.Email = uyes.Email;
                    db.SaveChanges();
                    Session["kullaniciAdi"] = uyes.KullaniciAdi;
                    return RedirectToAction("Index", "Uye", new { id = uyes.UyeId });
            }
            return View();
        }

        public ActionResult UyeProfil(int id)
        {
            var uye = db.Uyes.Where(u => u.UyeId == id).SingleOrDefault();
            return View(uye);
        }
    }
}