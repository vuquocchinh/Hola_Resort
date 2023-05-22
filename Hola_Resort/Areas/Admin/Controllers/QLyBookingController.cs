using Hola_Resort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}