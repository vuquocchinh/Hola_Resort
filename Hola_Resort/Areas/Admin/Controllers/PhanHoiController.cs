using Hola_Resort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hola_Resort.Areas.Admin.Controllers
{
    public class PhanHoiController : Controller
    {
        HolaDBDataContext data = new HolaDBDataContext();
        // GET: Admin/PhanHoi
        public ActionResult Review()
        {
            var all_review = from s in data.Reviews select s;
            return View(all_review);
        }
        public ActionResult Delete(string id)
        {
            var D_review = data.Reviews.First(m => m.ReviewId == id);
            return View(D_review);
        }
        [HttpPost]

        public ActionResult Delete(string id, FormCollection collection)
        {
            var D_review = data.Reviews.Where(m => m.ReviewId == id).First();
            data.Reviews.DeleteOnSubmit(D_review);
            data.SubmitChanges();
            return RedirectToAction("Review");
        }
        public ActionResult Detail(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var u = data.Customers.First(m => m.CustomerId == id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }
    }
}