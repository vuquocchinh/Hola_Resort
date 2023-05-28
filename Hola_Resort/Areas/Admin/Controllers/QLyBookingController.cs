using Hola_Resort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hola_Resort.Areas.Admin.Controllers
{
    public class QLyBookingController : Controller
    {
        HolaDBDataContext data = new HolaDBDataContext();
        // GET: Admin/QLyBooking
        public ActionResult Index()
        {
            var all_booking = from s in data.Bookings select s;
            return View(all_booking);
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
        public ActionResult Delete(int id)
        {
            var D_booking = data.Bookings.First(m => m.BookingId == id);
            return View(D_booking);
        }
        [HttpPost]

        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_booking = data.Bookings.Where(m => m.BookingId == id).First();
            data.Bookings.DeleteOnSubmit(D_booking);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}