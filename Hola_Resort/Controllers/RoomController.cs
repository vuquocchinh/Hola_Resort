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
            var product = data.Rooms.FirstOrDefault(p => p.RoomId == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}