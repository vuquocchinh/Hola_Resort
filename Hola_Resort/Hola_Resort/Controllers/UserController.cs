using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hola_Resort.Models;

namespace Hola_Resort.Controllers
{
    public class UserController : Controller
    {
        HolaDBDataContext data = new HolaDBDataContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        // GET Register
        public ActionResult Register()
        {
            return View();
        }
            


    }
}