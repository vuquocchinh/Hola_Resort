using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hola_Resort.Models;
using BCrypt.Net;


namespace Hola_Resort.Controllers
{
    public class UserController : Controller
    {
        HolaDBDataContext data = new HolaDBDataContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        // GET Register
        public ActionResult Register()
        {
            return View(new ViewModel.VMregister());
        }
        // POST Register
        [HttpPost]
        public ActionResult Register(ViewModel.VMregister model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên đăng nhập đã được sử dụng hay chưa
                var checkUsername = data.Customers.Where(x => x.Username == model.Username).FirstOrDefault();
                if (checkUsername != null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã được sử dụng. Vui lòng chọn tên đăng nhập khác.");
                    return View(model);
                }
                // Tạo đối tượng Customer mới
                Customer customer = new Customer();
                customer.FullName = model.FullName;
                customer.Email = model.Email;
                customer.PhoneNumber = model.PhoneNumber;
                customer.Address = model.Address;
                customer.DateofBirth = model.DateofBirth;
                customer.Gender = model.Gender;
                customer.Username = model.Username;
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password, salt);

                customer.Password = hashedPassword;

                // Thêm đối tượng Customer mới vào CSDL
                data.Customers.InsertOnSubmit(customer);
                data.SubmitChanges();
                return RedirectToAction("Login", "User");
            }
            return View(model);
        }

        // GET Login
        public ActionResult Login()
        {
            return View(new ViewModel.VMlogin());
        }

        // POST Login
        [HttpPost]
        public ActionResult Login(ViewModel.VMlogin model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên đăng nhập và mật khẩu có tồn tại trong CSDL hay không
                var customer = data.Customers.Where(x => x.Username == model.Username).FirstOrDefault();
                if (customer != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(model.Password, customer.Password))
                    {
                        // Lưu thông tin đăng nhập vào Local Storage
                        string customerInfo = Newtonsoft.Json.JsonConvert.SerializeObject(customer);
                        Session["CustomerInfo"] = customerInfo;
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Incorrect username or password!");
                TempData["Error"] = "Incorrect username or password!";

                return View(model);
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            // Clear Session
            Session.Abandon();

            // Redirect to Home Page
            return RedirectToAction("Index", "Home");
        }




    }
}