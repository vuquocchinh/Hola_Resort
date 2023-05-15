using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hola_Resort.Models;

namespace Hola_Resort.ViewModel
{
    public class BookingViewModel
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildrens { get; set; }
        
    }

}