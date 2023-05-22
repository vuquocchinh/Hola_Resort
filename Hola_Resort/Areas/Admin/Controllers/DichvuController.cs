using Hola_Resort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hola_Resort.Areas.Admin.Controllers
{
    public class DichvuController : Controller
    {
        HolaDBDataContext data = new HolaDBDataContext();
        // GET: Admin/Dichvu
        public ActionResult ListDV()
        {
            var all_service = from s in data.Services select s;
            return View(all_service);
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var u = data.Services.First(m => m.ServiceId == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var u = data.Services.First(m => m.ServiceId == id);
            var madichvu = collection["ServiceId"];
            var tendichvu = collection["ServiceName"];

            var giatien = collection["PriceService"];
            u.ServiceId = madichvu;
            u.ServiceName = tendichvu;
            UpdateModel(u);
            data.SubmitChanges();
            return RedirectToAction("ListDV");
        }

        public ActionResult Delete(string id)
        {
            var D_Service = data.Services.First(m => m.ServiceId == id);
            return View(D_Service);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var D_Service = data.Services.Where(m => m.ServiceId == id).First();
            data.Services.DeleteOnSubmit(D_Service);
            data.SubmitChanges();
            return RedirectToAction("ListDV");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Service u)
        {
            var madichvu = collection["ServiceId"];
            var tendichvu = collection["ServiceName"];
            u.ServiceId = madichvu;
            u.ServiceName = tendichvu;
            data.Services.InsertOnSubmit(u);
            data.SubmitChanges();
            return RedirectToAction("ListDV");
        }
        public ActionResult Detail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var u = data.Services.First(m => m.ServiceId == id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }
    }
}