using Hola_Resort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hola_Resort.Controllers
{
    public class RoomTypeController : Controller
    {
        // GET: RoomType
        HolaDBDataContext data = new HolaDBDataContext();
        public ActionResult Index()
        {
            return View(data.RoomTypes.ToList());
        }
    }
}