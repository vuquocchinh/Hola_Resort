using Hola_Resort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hola_Resort.Areas.Admin.Controllers
{
    public class HoaDonController : Controller
    {
        // GET: Admin/HoaDon
        HolaDBDataContext data = new HolaDBDataContext();
        public ActionResult ListHD()
        {
            var all_invoice = from s in data.Invoices select s;
            return View(all_invoice);
        }
        public ActionResult Delete(string id)
        {
            var D_invoice = data.Invoices.First(m => m.InvoiceId == id);
            return View(D_invoice);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var D_invoice = data.Invoices.Where(m => m.InvoiceId == id).First();
            data.Invoices.DeleteOnSubmit(D_invoice);
            data.SubmitChanges();
            return RedirectToAction("ListHD");
        }


        public ActionResult Detail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var u = data.Invoices.First(m => m.InvoiceId == id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }
    }
}
