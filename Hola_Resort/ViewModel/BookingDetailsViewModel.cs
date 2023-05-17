using System;

namespace Hola_Resort.ViewModel
{
    public class BookingDetailsViewModel
    {
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
        
    }
}
