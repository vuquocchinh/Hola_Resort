using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hola_Resort.ViewModel
{
    public class VMlogin
    {
        [Required(ErrorMessage = "Please enter your username!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password!")]
        public string Password { get; set; }
    }
}
