using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hola_Resort.Models;

namespace Hola_Resort.Controllers
{
    public class SearchController : Controller
    {
        HolaDBDataContext data = new HolaDBDataContext();

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchRooms(FormCollection form)
        {
            // Lấy thông tin đặt phòng từ biểu mẫu
            DateTime checkInDate = Convert.ToDateTime(form["CheckInDate"]);
            DateTime checkOutDate = Convert.ToDateTime(form["CheckOutDate"]);
            int numberOfAdults = Convert.ToInt32(form["NumberOfAdults"]);
            int numberOfChildrens = Convert.ToInt32(form["NumberOfChildrens"]);
            int numberOfRooms = Convert.ToInt32(form["numberOfRooms"]);

            // Tính tổng số khách
            int totalGuests = numberOfAdults + numberOfChildrens;

            int numberOfBeds = 0;
            if (numberOfChildrens <= (numberOfAdults / 2))
            {
                numberOfBeds = (int)Math.Round((double)numberOfAdults / 2, MidpointRounding.ToEven);
            }
            else
            {
                numberOfBeds = numberOfAdults;
            }

            // Truy vấn cơ sở dữ liệu để tìm các phòng phù hợp
            var rooms = from r in data.RoomTypes
                        where r.Capacity >= numberOfBeds &&
                              !(from b in data.Bookings
                                where b.RoomId == r.RoomTypeId &&
                                      (checkInDate >= b.CheckInDate && checkInDate < b.CheckOutDate ||
                                       checkOutDate > b.CheckInDate && checkOutDate <= b.CheckOutDate ||
                                       checkInDate <= b.CheckInDate && checkOutDate >= b.CheckOutDate)
                                select b).Any()
                        select r;

            // Tính số lượng phòng cần đặt
            numberOfRooms = (int)Math.Ceiling((double)totalGuests / rooms.First().Capacity);

            // Trả về danh sách phòng phù hợp tìm thấy và số lượng phòng cần đặt
            ViewBag.NumberOfRooms = numberOfRooms;
            return View("Search", rooms);
        }
    }
}
