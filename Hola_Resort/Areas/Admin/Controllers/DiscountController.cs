using Hola_Resort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hola_Resort.Areas.Admin.Controllers
{
    public class DiscountController : Controller
    {
        HolaDBDataContext data = new HolaDBDataContext();
        // GET: Admin/Discount
        public ActionResult Discount()
        {
            var all_discount = from s in data.Discounts select s;
            return View(all_discount);
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var u = data.Discounts.First(m => m.DiscountId == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var u = data.Discounts.First(m => m.DiscountId == id);
            var magiamgia = collection["DiscountId"];
            var tensukien = collection["DiscountName"];
            var ngaybatdau = collection["StartDate"];
            var ngayketthuc = collection["EndDate"];
            u.DiscountId = magiamgia;
            u.DiscountName = tensukien;
            UpdateModel(u);
            data.SubmitChanges();
            return RedirectToAction("Discount");
        }

        public ActionResult Delete(string id)
        {
            var D_RoomType = data.Discounts.First(m => m.DiscountId == id);
            return View(D_RoomType);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var D_RoomType = data.Discounts.Where(m => m.DiscountId == id).First();
            data.Discounts.DeleteOnSubmit(D_RoomType);
            data.SubmitChanges();
            return RedirectToAction("Discount");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Discount u)
        {
            var magiamgia = collection["DiscountId"];
            var tensukien = collection["DiscountName"];
            u.DiscountId = magiamgia;
            u.DiscountName = tensukien;
            data.Discounts.InsertOnSubmit(u);
            data.SubmitChanges();
            return RedirectToAction("Discount");
        }
        public ActionResult Detail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var u = data.Discounts.First(m => m.DiscountId == id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }
    }
}