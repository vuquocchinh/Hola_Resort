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
            if (!ModelState.IsValid)
            {
                // Xử lý chuỗi nhập vào cho giờ và phút của ngày giờ đầu vào
                string checkinTimeString = viewModel.CheckInDate.ToShortTimeString();
                string[] checkinTimeParts = checkinTimeString.Split(':');
                int checkinHour = Int32.Parse(checkinTimeParts[0]);
                if (viewModel.CheckInDate.ToString("tt") == "PM" && checkinHour != 12)
                {
                    checkinHour += 12;
                }
                int checkinMinute = Int32.Parse(checkinTimeParts[1]);

                // Lưu thông tin vào Session
                Session["checkinDate"] = viewModel.CheckInDate.Date.AddHours(checkinHour).AddMinutes(checkinMinute);
                Session["checkoutDate"] = viewModel.CheckOutDate;
                Session["adults"] = viewModel.NumberOfAdults;
                Session["children"] = viewModel.NumberOfChildrens;

                // Chuyển hướng sang trang RoomType
                return RedirectToAction("Index", "RoomType");
            }

            return View(viewModel);
        }

        public ActionResult BookingDetails(string id)
        {
            // Lấy thông tin phòng dựa trên id
            var room = data.Rooms.FirstOrDefault(r => r.RoomId == id);
            if (room == null)
            {
                return HttpNotFound();
            }

            // Lấy thông tin room type của phòng
            var roomType = data.RoomTypes.FirstOrDefault(rt => rt.RoomTypeId == room.RoomTypeId);
            if (roomType == null)
            {
                return HttpNotFound();
            }

            // Lấy thông tin đặt phòng từ Session
            var checkinDate = (DateTime)Session["checkinDate"];
            var checkoutDate = (DateTime)Session["checkoutDate"];
            var adults = (int)Session["adults"];
            var children = (int)Session["children"];

            // Tạo view model BookingDetailsViewModel
            var viewModel = new BookingDetailsViewModel
            {
                RoomId = room.RoomId,
                RoomTypeName = roomType.RoomTypeName,
                Capacity = roomType.Capacity,
                PriceDay = roomType.PriceDay,
                RoomNumber = room.RoomNumber,
                Description = room.Description,
                CheckInDate = checkinDate,
                CheckOutDate = checkoutDate,
                NumberOfAdults = adults,
                NumberOfChildrens = children
            };

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult BookingDetails(BookingDetailsViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Lưu thông tin booking details vào database
            var bookingDetails = new bookingdetail
            {
                NationalId = viewModel.NationalId,
                PhoneNumber = viewModel.PhoneNumber,
                Name = viewModel.Name,
                Email = viewModel.Email
            };
            data.bookingdetails.InsertOnSubmit(bookingDetails);
            data.SubmitChanges();

            // Lấy thông tin đặt phòng từ Session
            var checkinDate = (DateTime)Session["checkinDate"];
            var checkoutDate = (DateTime)Session["checkoutDate"];
            var adults = (int)Session["adults"];
            var children = (int)Session["children"];

            // Lưu thông tin đặt phòng vào database
                var booking = new Booking
                {
                    RoomId = viewModel.RoomId,
                    CheckInDate = checkinDate,
                    CheckOutDate = checkoutDate,
                    NumberOfRooms = viewModel.NumberOfRooms,
                    NumberOfAdults = adults,
                    NumberOfChildrens = children,
                    TotalPrice = CalculateTotalPrice(viewModel.RoomId, checkinDate, checkoutDate, viewModel.NumberOfRooms),
                    // Thêm các trường khác của booking
                    NationalId = viewModel.NationalId // Gán NationalId
                };

            data.Bookings.InsertOnSubmit(booking);
            data.SubmitChanges();

            // Chuyển hướng đến trang home
            return RedirectToAction("Index", "Home");

            // Hàm tính tổng giá tiền
            decimal CalculateTotalPrice(string roomId, DateTime checkinDateParam, DateTime checkoutDateParam, int numberOfRooms)
            {
                // Code tính tổng giá tiền dựa trên thông tin đặt phòng

                

                var roomType = data.RoomTypes.FirstOrDefault(rt => rt.RoomTypeId == roomId);
                if (roomType != null)
                {
                    TimeSpan duration = checkinDateParam - checkinDateParam;
                    int totalDays = (int)duration.TotalDays;
                    decimal totalPrice = roomType.PriceDay * totalDays * numberOfRooms;
                    return totalPrice;
                }

                return 0;
            }

        }



    }
    }
