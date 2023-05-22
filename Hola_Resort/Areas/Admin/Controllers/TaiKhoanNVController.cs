using Hola_Resort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hola_Resort.Areas.Admin.Controllers
{
    public class TaiKhoanNVController : Controller
    {
        HolaDBDataContext data = new HolaDBDataContext();
        // GET: Admin/TaiKhoanNV
        public ActionResult ListTK()
        {
            var all_AdminAccount = from s in data.AdminAccounts select s;
            return View(all_AdminAccount);
        }
    }
}