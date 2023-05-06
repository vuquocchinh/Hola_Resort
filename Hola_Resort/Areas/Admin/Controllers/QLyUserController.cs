using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hola_Resort.Models;
using PagedList;

namespace Hola_Resort.Areas.Admin.Controllers
{
    public class QLyUserController : Controller
    {
        // GET: Admin/User
        HolaDBDataContext data = new HolaDBDataContext();
        public ActionResult ListUser()
        {
            var all_customer = from s in data.Customers select s;
            return View(all_customer);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var u = data.Customers.First(m => m.CustomerId == id);
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
        public ActionResult Detail(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var u = data.Customers.First(m => m.CustomerId == id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }
    }
}