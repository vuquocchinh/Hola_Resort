using Hola_Resort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var u = data.AdminAccounts.First(m => m.AdminId == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var u = data.Customers.First(m => m.CustomerId == id);
            var ten = collection["FullName"];
            var email = collection["Email"];
            var phone = collection["PhoneNumber"];
            var address = collection["Address"];
            u.CustomerId = id;
            u.FullName = ten;
            u.Email = email;
            u.PhoneNumber = phone;
            u.Address = address;
            UpdateModel(u);
            data.SubmitChanges();
            return RedirectToAction("ListUser");
        }

        public ActionResult Delete(int id)
        {
            var u = data.Customers.First(m => m.CustomerId == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var u = data.Customers.Where(m => m.CustomerId == id).First();
            UpdateModel(u);
            data.SubmitChanges();
            return RedirectToAction("ListUser");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Customer u)
        {
            var ten = collection["FullName"];
            var email = collection["Email"];
            var phone = collection["PhoneNumber"];
            var address = collection["Address"];
            var password = collection["PasswordUser"];
            var gioitinh = collection["Gender"];
            u.FullName = ten;
            u.Email = email;
            u.PhoneNumber = phone;
            u.Address = address;
            u.Password = password;
            u.Gender = gioitinh;
            data.Customers.InsertOnSubmit(u);
            data.SubmitChanges();
            return RedirectToAction("ListUser");
        }
    }
}