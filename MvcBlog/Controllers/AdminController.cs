using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBlog.Models;
using System.Net;
using System.Xml;
using PagedList;
using PagedList.Mvc;

namespace MvcBlog.Controllers
{
    public class AdminController : Controller
    {
        mvcblogEntities db = new mvcblogEntities();
        // GET: /Admin/
        public ActionResult Index()
        {
            ViewBag.MakaleSayisi = db.Makales.Count();
            ViewBag.UyeSayisi = db.Uyes.Count();
            ViewBag.YorumSayisi = db.Yorums.Count();
            ViewBag.KategoriSayisi = db.Kategoris.Count();
            return View();
        }
	}
}