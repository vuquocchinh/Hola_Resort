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

        [Required(ErrorMessage = "Name cannot be left blank.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "email cannot be left blank.")]
        [EmailAddress(ErrorMessage = "Invalid email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number cannot be empty!")]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Invalid phone number!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address cannot be left empty!")]
        public string Address { get; set; }

        public DateTime? DateofBirth { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "Username cannot be left blank!")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 30 characters long!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password cannot be left blank!")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 30 characters long!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,30}$", ErrorMessage = "Password must contain at least 1 uppercase letter, 1 special character, and 1 number!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password cannot be left blank")]
        [Compare("Password", ErrorMessage = "Confirm password does not match!")]
        public string ConfirmPassword { get; set; }
    }
}