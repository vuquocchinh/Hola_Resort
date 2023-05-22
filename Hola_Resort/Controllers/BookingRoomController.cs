using System;
using System.Linq;
using System.Web.Mvc;
using Hola_Resort.Models;
using Hola_Resort.ViewModel;

namespace Hola_Resort.Controllers
{
    public class BookingRoomController : Controller
    {
        HolaDBDataContext data = new HolaDBDataContext();

        // GET: BookingRoom
        public ActionResult Index()
        {
            return View();
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
                RoomTypeId = roomType.RoomTypeId,
                RoomId = room.RoomId,
                RoomTypeName = roomType.RoomTypeName,
                Capacity = roomType.Capacity,
                PriceDay = roomType.PriceDay,
                RoomNumber = room.RoomNumber,
                Description = room.Description,
                CheckInDate = checkinDate,
                CheckOutDate = checkoutDate,
                NumberOfAdults = adults,
                NumberOfChildrens = children,
            };

            // Tính toán số ngày đặt phòng
            TimeSpan totalDays = checkoutDate.Date - checkinDate.Date;
            int numberOfDays = totalDays.Days;

            // Tính toán tổng tiền dựa trên giá tiền và số ngày đặt phòng
            decimal totalPrice = roomType.PriceDay * numberOfDays;
            viewModel.TotalPrice = totalPrice;
            
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BookingDetails(BookingDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            
            // Lưu thông tin khách nhận phòng vào database
            var guestInformation = new GuestInformation
            {
                NationalId = viewModel.NationalId,
                PhoneNumber = viewModel.PhoneNumber,
                Name = viewModel.Name,
                Email = viewModel.Email
            };
            data.GuestInformations.InsertOnSubmit(guestInformation);
            data.SubmitChanges();

            
            // Lấy thông tin đặt phòng từ Session
            var checkinDate = (DateTime)Session["checkinDate"];
            var checkoutDate = (DateTime)Session["checkoutDate"];
            var adults = (int)Session["adults"];
            var children = (int)Session["children"];

            TimeSpan totalDays = checkoutDate.Date - checkinDate.Date;
            int numberOfDays = totalDays.Days;

            // Lấy thông tin phòng dựa trên id
            var room = data.Rooms.FirstOrDefault(r => r.RoomId == viewModel.RoomId);
            if (room == null)
            {
                return HttpNotFound();
            }

            var roomType = data.RoomTypes.FirstOrDefault(rt => rt.RoomTypeId == room.RoomTypeId);
            if (roomType == null)
            {
                return HttpNotFound();
            }

            // Tính toán tổng tiền dựa trên giá tiền và số ngày đặt phòng
            decimal totalPrice = roomType.PriceDay * numberOfDays;
            viewModel.TotalPrice = totalPrice;


            // Lưu thông tin đặt phòng
            var booking = new Booking
            {
                RoomId = viewModel.RoomId,
                CheckInDate = checkinDate,
                CheckOutDate = checkoutDate,
                NumberOfRooms = viewModel.NumberOfRooms,
                NumberOfAdults = adults,
                NumberOfChildrens = children,
                TotalPrice = viewModel.TotalPrice,
                GuestId = guestInformation.GuestId

        };

            data.Bookings.InsertOnSubmit(booking);
            data.SubmitChanges();

            // Chuyển hướng đến trang xác nhận đặt phòng
            return RedirectToAction("Index", "Home");
        }
    }
}
