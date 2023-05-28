using Hola_Resort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hola_Resort.Areas.Admin.Controllers
{
    public class InfoNVController : Controller
    {
        HolaDBDataContext data = new HolaDBDataContext();
        // GET: Admin/InfoNV
        public ActionResult InfoNV()
        {
            var all_NV = from s in data.AdminAccounts select s;
            return View(all_NV);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var u = data.AdminAccounts.First(m => m.AdminId == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var u = data.AdminAccounts.First(m => m.AdminId == id);
            var tennhanvien = collection["FullName"];
            var email = collection["Email"];
            var manhanvien = collection["AdminId"];
            u.FullName = tennhanvien;
            u.Email = email;
            u.AdminId = id;
            UpdateModel(u);
            data.SubmitChanges();
            return RedirectToAction("InfoNV");
        }

        public ActionResult Delete(int id)
        {
            var D_admin = data.AdminAccounts.First(m => m.AdminId == id);
            return View(D_admin);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_info = data.AdminAccounts.Where(m => m.AdminId == id).First();
            data.AdminAccounts.DeleteOnSubmit(D_info);
            data.SubmitChanges();
            return RedirectToAction("InfoNV");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, AdminAccount u)
        {
            var tennhanvien = collection["FullName"];
            var email = collection["Email"];
            
            u.FullName = tennhanvien;
            u.Email = email;
            
            data.AdminAccounts.InsertOnSubmit(u);
            data.SubmitChanges();
            return RedirectToAction("InfoNV");
        }
        
    }
}