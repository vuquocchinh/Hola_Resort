using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hola_Resort.Models;
using Hola_Resort.ViewModel;

namespace Hola_Resort.Controllers
{
    public class HomeController : Controller
    {
        HolaDBDataContext data = new HolaDBDataContext();

        public ActionResult Index()
        {
            var viewModel = new BookingDetailsViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(BookingDetailsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {


                // Lưu thông tin vào Session
                Session["checkinDate"] = viewModel.CheckInDate.Date;
                Session["checkoutDate"] = viewModel.CheckOutDate;
                Session["adults"] = viewModel.NumberOfAdults;
                Session["children"] = viewModel.NumberOfChildrens;

                // Chuyển hướng sang trang RoomType
                return RedirectToAction("Index", "RoomType");
            }

            return View(viewModel);
        }

        

    }
    }
