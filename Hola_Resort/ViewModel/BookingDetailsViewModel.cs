using System;

namespace Hola_Resort.ViewModel
{
    public class BookingDetailsViewModel
    {
        public string RoomTypeId { get; set; }
        public string RoomId { get; set; }
        public string RoomTypeName { get; set; }
        public int Capacity { get; set; }
        public decimal PriceDay { get; set; }
        public int Bed { get; set; }
        public string RoomNumber { get; set; }
        public string Description { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildrens { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfRooms { get; set; }
        public string NationalId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal TotalPrice { get; set; }
        public int GuestId { get; set; }
    }
}
