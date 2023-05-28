using Hola_Resort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace Hola_Resort.Areas.Admin.Controllers
{
    public class GuestController : Controller
    {
        // GET: Admin/User
        HolaDBDataContext data = new HolaDBDataContext();
        public ActionResult ListGuest()
        {
            var all_guest = from s in data.GuestInformations select s;
            return View(all_guest);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var u = data.GuestInformations.First(m => m.GuestId == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var u = data.GuestInformations.First(m => m.GuestId == id);
            var makhachhang= collection["GuestId"];
            var ten = collection["Name"];
            var email = collection["Email"];
            var phone = collection["PhoneNumber"];
            var CCCD = collection["NationalId"];
            u.GuestId = id;
            u.Name = ten;
            u.Email = email;
            u.PhoneNumber = phone;
            u.NationalId = CCCD;
            UpdateModel(u);
            data.SubmitChanges();
            return RedirectToAction("ListGuest");
        }

        public ActionResult Delete(int id)
        {
            var guest = data.GuestInformations.FirstOrDefault(m => m.GuestId == id);
            if (guest == null)
            {
                return HttpNotFound();
            }

            

            return View(guest);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var guest = data.GuestInformations.FirstOrDefault(m => m.GuestId == id);
            if (guest == null)
            {
                return HttpNotFound();
            }

            // Kiểm tra xem có bất kỳ bản ghi nào trong bảng Booking liên quan đến GuestInformation hay không
            var bookings = data.Bookings.Where(b => b.GuestId == id).ToList();
            if (bookings.Count > 0)
            {
                // Nếu có bản ghi trong bảng Booking liên quan, hãy xóa chúng trước
                foreach (var booking in bookings)
                {
                    data.Bookings.DeleteOnSubmit(booking);
                }
                data.SubmitChanges();
            }

            // Xóa bản ghi gốc trong bảng GuestInformation
            data.GuestInformations.DeleteOnSubmit(guest);
            data.SubmitChanges();

            

            return RedirectToAction("ListGuest");
        }

        public ActionResult Search(int? GuestId, string Name, string Email, string PhoneNumber, string NationalId)
        {
            var guest = SearchGuest(GuestId, Name, Email, PhoneNumber, NationalId);
            return View("Search", guest);
        }

        public List<GuestInformation> SearchGuest(int? GuestId, string Name, string Email, string PhoneNumber, string NationalId)
        {
            using (var dbContext = new HolaDBDataContext()) 
            {
                var query = dbContext.GuestInformations.AsQueryable();

                // Áp dụng các tiêu chí tìm kiếm
                if (GuestId.HasValue && GuestId.Value > 0)
                    query = query.Where(c => c.GuestId == GuestId);

                if (!string.IsNullOrEmpty(Name))
                    query = query.Where(c => c.Name.Contains(Name));

                if (!string.IsNullOrEmpty(Email))
                    query = query.Where(c => c.Email == Email);

                if (!string.IsNullOrEmpty(PhoneNumber))
                    query = query.Where(c => c.PhoneNumber.Contains(PhoneNumber));

                if (!string.IsNullOrEmpty(NationalId))
                    query = query.Where(c => c.NationalId.Contains(NationalId));

                // Thực hiện truy vấn và trả về kết quả
                var results = query.ToList();
                return results;
            }
        }






        public ActionResult Detail(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var u = data.GuestInformations.First(m => m.GuestId == id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }
    }
}