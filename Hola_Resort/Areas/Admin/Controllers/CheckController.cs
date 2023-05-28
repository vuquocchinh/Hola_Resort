using Hola_Resort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hola_Resort.Areas.Admin.Controllers
{
    public class CheckController : Controller
    {
        HolaDBDataContext data = new HolaDBDataContext();
        // GET: Admin/Check
        [HttpGet]
        public ActionResult CheckRoom(bool? roomStatus)
        {
            using (var dbContext = new HolaDBDataContext())
            {
                var query = dbContext.Rooms.AsQueryable();

                // Kiểm tra trạng thái phòng nếu được chỉ định
                if (roomStatus.HasValue)
                {
                    query = query.Where(r => r.RoomStatus == roomStatus.Value);
                }

                var availableRooms = query.ToList();

                return View(availableRooms);
            }
        }

    }
}