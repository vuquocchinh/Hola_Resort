using Hola_Resort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hola_Resort.Areas.Admin.Controllers
{
    public class LoaiPhongController : Controller
    {
        HolaDBDataContext data = new HolaDBDataContext();
        // GET: Admin/LoaiPhong
        public ActionResult ListLPhong()
        {
            var all_roomtype = from s in data.RoomTypes select s;
            return View(all_roomtype);
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var u = data.RoomTypes.First(m => m.RoomTypeId == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var u = data.RoomTypes.First(m => m.RoomTypeId == id);
            var maloaiphong = collection["RoomTypeId"];
            var tenloaiphong = collection["RoomTypeName"];
            var succhua = collection["Capacity"];
            var giatien = collection["PriceDay"];
            u.RoomTypeId = id;
            u.RoomTypeName = tenloaiphong;
            UpdateModel(u);
            data.SubmitChanges();
            return RedirectToAction("ListLPhong");
        }

        public ActionResult Delete(string id)
        {
            var D_RoomType = data.RoomTypes.First(m => m.RoomTypeId == id);
            return View(D_RoomType);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var D_RoomType = data.RoomTypes.Where(m => m.RoomTypeId == id).First();
            data.RoomTypes.DeleteOnSubmit(D_RoomType);
            data.SubmitChanges();
            return RedirectToAction("ListLPhong");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, RoomType u)
        {
            var maloaiphong = collection["RoomTypeId"];
            var tenloaiphong = collection["RoomTypeName"];
            u.RoomTypeId = maloaiphong;
            u.RoomTypeName = tenloaiphong;
            data.RoomTypes.InsertOnSubmit(u);
            data.SubmitChanges();
            return RedirectToAction("ListLPhong");
        }
        public ActionResult Detail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var u = data.RoomTypes.First(m => m.RoomTypeId == id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }
        public ActionResult Search(string roomTypeId, string roomTypeName)
        {
            var roomtype = SearchRoomtypes(roomTypeId, roomTypeName);
            return View("Search", roomtype);
        }
        public List<RoomType> SearchRoomtypes(string roomTypeId, string roomTypeName)
        {
            using (var dbContext = new HolaDBDataContext())
            {
                var query = dbContext.RoomTypes.AsQueryable();

                if (!string.IsNullOrEmpty(roomTypeId))
                {
                    query = query.Where(c => c.RoomTypeId.Contains(roomTypeId));
                }

                if (!string.IsNullOrEmpty(roomTypeName))
                {
                    query = query.Where(c => c.RoomTypeName.Contains(roomTypeName));
                }               

                var results = query.ToList();
                return results;
            }
        }
    }
}