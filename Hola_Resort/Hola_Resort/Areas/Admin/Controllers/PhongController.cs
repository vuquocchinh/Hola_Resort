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
    public class PhongController : Controller
    {
        // GET: Admin/User
        HolaDBDataContext data = new HolaDBDataContext();
        public ActionResult ListPhong()
        {
            var all_room = from s in data.Rooms select s;
            return View(all_room);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var u = data.Rooms.First(m => m.RoomId == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var u = data.Rooms.First(m => m.RoomId == id);
            var sophong = collection["RoomNumber"];
            var maloaiphong = collection["RoomTypeId"];
            var trangthai = collection["RoomStatus"];
            var mota = collection["Description"];
            u.RoomId = id;
            u.RoomNumber = sophong;
            u.RoomTypeId = maloaiphong;
            u.Description = mota;
            UpdateModel(u);
            data.SubmitChanges();
            return RedirectToAction("ListPhong");
        }

        public ActionResult Delete(string id)
        {
            var u = data.Rooms.First(m => m.RoomId == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var u = data.Rooms.Where(m => m.RoomId == id).First();

            UpdateModel(u);
            data.SubmitChanges();
            return RedirectToAction("ListPhong");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Room u)
        {
            var maphong = collection["RoomId"];
            var sophong = collection["RoomNumber"];
            var maloaiphong = collection["RoomTypeId"];
            var trangthai = collection["RoomStatus"];
            var mota = collection["Description"];
            u.RoomNumber = sophong;
            u.RoomTypeId = maloaiphong;
            u.Description = mota;
            u.RoomId = maphong;
            data.Rooms.InsertOnSubmit(u);
            data.SubmitChanges();
            return RedirectToAction("ListPhong");
        }
        public ActionResult Detail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var u = data.Rooms.First(m => m.RoomId == id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }
    }
}