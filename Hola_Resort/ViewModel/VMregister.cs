using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Hola_Resort.ViewModel
{
    public class VMregister
    {
        [Key]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Họ tên không được bỏ trống.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email không được bỏ trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được bỏ trống.")]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được bỏ trống.")]
        public string Address { get; set; }

        public DateTime? DayofBirt { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Tên đăng nhập phải có độ dài từ 6 đến 30 ký tự.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có độ dài từ 6 đến 30 ký tự.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,30}$", ErrorMessage = "Mật khẩu phải có ít nhất 1 ký tự viết hoa, 1 ký tự đặc biệt và 1 số.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu không được bỏ trống.")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không trùng khớp.")]
        public string ConfirmPassword { get; set; }
    }
}