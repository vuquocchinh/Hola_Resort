using Hola_Resort.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace Hola_Resort.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        HolaDBDataContext data = new HolaDBDataContext();

        public ActionResult Room(string id)
        {
            var rooms = data.Rooms.Where(r => r.RoomTypeId == id).ToList();

            return View(rooms);
        }
        public ActionResult Details(string id)
        {
            var room = data.Rooms.FirstOrDefault(r => r.RoomId == id);
            if (room == null)
            {
                return HttpNotFound();
            }

            var roomType = data.RoomTypes.FirstOrDefault(rt => rt.RoomTypeId == room.RoomTypeId);
            if (roomType == null)
            {
                return HttpNotFound();
            }

            var selectedRoom = new
            {
                RoomTypeName = roomType.RoomTypeName,
                Capacity = roomType.Capacity,
                PriceDay = roomType.PriceDay,
                Bed = roomType.Bed,
                RoomNumber = room.RoomNumber,
                Description = room.Description
            };

            Session["SelectedRoom"] = selectedRoom;

            return RedirectToAction("Booking", "Home");
        }



    }
}