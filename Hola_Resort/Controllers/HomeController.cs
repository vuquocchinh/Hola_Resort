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


                // Lưu thông tin vào db
                var checkinDate = viewModel.CheckInDate;
                var checkoutDate = viewModel.CheckOutDate;
                var adults = viewModel.NumberOfAdults;
                var children = viewModel.NumberOfChildrens;

                // Chuyển hướng sang trang RoomType
                return RedirectToAction("Index", "RoomType");
            }

            return View(viewModel);
        }

        

    }
    }
