using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hola_Resort.ViewModel
{
    public class VMlogin
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }
    }
}
