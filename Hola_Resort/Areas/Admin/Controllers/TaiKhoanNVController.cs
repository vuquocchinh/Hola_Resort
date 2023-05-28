using Hola_Resort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hola_Resort.Areas.Admin.Controllers
{
    public class TaiKhoanNVController : Controller
    {
        HolaDBDataContext data = new HolaDBDataContext();
        // GET: Admin/TaiKhoanNV
        public ActionResult ListTK()
        {
            var all_AdminAccount = from s in data.AdminAccounts select s;
            return View(all_AdminAccount);
        }
        

        public ActionResult Delete(int id)
        {
            var D_admin = data.AdminAccounts.First(m => m.AdminId == id);
            return View(D_admin);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_admin = data.AdminAccounts.Where(m => m.AdminId == id).First();
            data.AdminAccounts.DeleteOnSubmit(D_admin);
            data.SubmitChanges();
            return RedirectToAction("ListTK");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, AdminAccount u)
        {
            var manhanvien = collection["AdminId"];
            var tentaikhoan = collection["Username"];
            var matkhau = collection["Password"];
            var email = collection["Email"];
            var tennhanvien = collection["FullName"];
            u.Username = tentaikhoan;
            u.Password = matkhau;
            u.Email = email;
            u.FullName = tennhanvien;
            data.AdminAccounts.InsertOnSubmit(u);
            data.SubmitChanges();
            return RedirectToAction("ListTK");
        }
    }
}