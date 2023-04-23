using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hola_Resort.Models;

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
                customer.CustomerId = model.CustomerId;
                customer.FullName = model.FullName;
                customer.Email = model.Email;
                customer.PhoneNumber = model.PhoneNumber;
                customer.Address = model.Address;
                customer.DayofBirt = model.DayofBirt;
                customer.Gender = model.Gender;
                customer.Username = model.Username;
                customer.Password = model.Password;
                // Thêm đối tượng Customer mới vào CSDL
                data.Customers.InsertOnSubmit(customer);
                data.SubmitChanges();
                return RedirectToAction("Home", "Index");
            }
            return View(model);
        }



    }
}