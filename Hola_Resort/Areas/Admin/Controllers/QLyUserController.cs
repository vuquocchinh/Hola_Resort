using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            var D_Customer = data.Customers.First(m => m.CustomerId == id);
            return View(D_Customer);
        }
        [HttpPost]

        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_Customer = data.Customers.Where(m => m.CustomerId == id).First();
            data.Customers.DeleteOnSubmit(D_Customer);
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

        public ActionResult Search(int? customerId, string fullName, string gender, string phoneNumber, string address)
        {
            var customers = SearchCustomers(customerId, fullName, gender, phoneNumber, address);
            return View("Search", customers);
        }

        public List<Customer> SearchCustomers(int? customerId, string fullName, string gender, string phoneNumber, string address)
        {
            using (var dbContext = new HolaDBDataContext()) // Thay thế YourDbContext() bằng lớp DbContext của bạn
            {
                var query = dbContext.Customers.AsQueryable();

                // Áp dụng các tiêu chí tìm kiếm
                if (customerId.HasValue && customerId.Value > 0)
                    query = query.Where(c => c.CustomerId == customerId);

                if (!string.IsNullOrEmpty(fullName))
                    query = query.Where(c => c.FullName.Contains(fullName));

                if (!string.IsNullOrEmpty(gender))
                    query = query.Where(c => c.Gender == gender);

                if (!string.IsNullOrEmpty(phoneNumber))
                    query = query.Where(c => c.PhoneNumber.Contains(phoneNumber));

                if (!string.IsNullOrEmpty(address))
                    query = query.Where(c => c.Address.Contains(address));

                // Thực hiện truy vấn và trả về kết quả
                var results = query.ToList();
                return results;
            }
        }

    }
}