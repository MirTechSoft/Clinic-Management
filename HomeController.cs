using Clinic_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace Clinic_Management.Controllers
{
    public class HomeController : Controller
    {
        private myContext _mc;
        private IWebHostEnvironment _env;
        public HomeController(myContext context, IWebHostEnvironment en)
        {
            _mc = context;
            _env = en;
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("name") != null)
            {
                ViewBag.na = HttpContext.Session.GetString("name");
            }
            var serv = _mc.tbl_feedback.ToList();
            return View(serv);
        }

        //Register & Login & Forget

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            _mc.tbl_users.Add(user);
            _mc.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string uemail,string upass)
        {
            var row = _mc.tbl_users.FirstOrDefault(u => u.Email == uemail);
            if(row != null && row.Password == upass)
            {
                HttpContext.Session.SetString("name", row.Name);
                HttpContext.Session.SetInt32("userId", row.Id);
                HttpContext.Session.SetString("img", row.Image);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Incorrect Email or Password";
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                HttpContext.Session.Remove("name");
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }

        public IActionResult Forget()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Forget(string uemail)
        {
            var row = _mc.tbl_users.FirstOrDefault(u => u.Email == uemail);
            if( row !=null)
            {
                return RedirectToAction("Newpassword", new { id = row.Id });
            }
            else
            {
                ViewBag.msg = "Email Does not Exist";
            }
            return View();
        }

        public IActionResult Newpassword(int Id)
        {
            ViewBag.id = Id;
            return View();
        }

        [HttpPost]
        public IActionResult Newpassword(int Id,string pass,string cpass)
        {
            var row = _mc.tbl_users.FirstOrDefault(u => u.Id == Id);
            if (row != null) 
            {
                if(pass == cpass)
                {
                    row.Password = pass;
                    _mc.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.msg = "Password Does not Match";
                }
            }
            else
            {
                ViewBag.msg = "Record Not Found";
            }
                return View();
        }

        //About

        public IActionResult About()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.na = HttpContext.Session.GetString("name");
            }
            return View();
        }

        //Services

        public IActionResult Services()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.na = HttpContext.Session.GetString("name");
            }
            var serv = _mc.tbl_services.ToList();
            return View(serv);
        }

        //Department

        public IActionResult Department()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.na = HttpContext.Session.GetString("name");
            }
            var depart = _mc.tbl_department.ToList();
            return View(depart);
        }

        public IActionResult Ldepartment(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.na = HttpContext.Session.GetString("name");
            }
            var item = _mc.tbl_department.Find(Id);
            return View(item);
        }

        //FAQ's

        public IActionResult FAQ()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.na = HttpContext.Session.GetString("name");
            }
            var std = _mc.tbl_faqs.ToList();
            return View(std);
        }

        //Medicines

        public IActionResult Medicines()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.na = HttpContext.Session.GetString("name");
            }
            var std = _mc.tbl_medicines.ToList();
            return View(std);
        }

        public IActionResult Dmedicines(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.na = HttpContext.Session.GetString("name");
            }
            var item = _mc.tbl_medicines.Find(Id);
            return View(item);
        }

        //Apparatus

        public IActionResult Apparatus()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.na = HttpContext.Session.GetString("name");
            }
            var std = _mc.tbl_apparatus.ToList();
            return View(std);
        }

        public IActionResult Dapparatus(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.na = HttpContext.Session.GetString("name");
            }
            var item = _mc.tbl_apparatus.Find(Id);
            return View(item);
        }

        //Contact us

        public IActionResult Contact()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Contact(Contact_us contact)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
                _mc.tbl_contact_us.Add(contact);
                _mc.SaveChanges();
                TempData["success"] = "Thank you! Your message has been sent successfully. We will get back to you shortly.";
                return RedirectToAction("Contact");
            }
            else
            {
                TempData["msg"] = "Please login first";
                return RedirectToAction("Contact");
            }

        }

        //Staff

        public IActionResult Staff()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var serv = _mc.tbl_staff.ToList();
            ViewBag.dep = _mc.tbl_department.ToList();
            ViewBag.staff = serv;
            return View(serv);
        }

        //Feedback

        public IActionResult Feedback()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Feedback(Feedback feedback)
        {

            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
                _mc.tbl_feedback.Add(feedback);
                _mc.SaveChanges();
                TempData["success"] = "Thank you for your feedback";
                return RedirectToAction("Feedback");
            }
            else
            {
                TempData["msg"] = "Please login first";
                return RedirectToAction("Feedback");
            }
        }

        //Profile Picture
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetInt32("userId"); // Make sure to store ID in session at login
            var user = _mc.tbl_users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Profile(User model, IFormFile ImageFile)
        {
            var user = _mc.tbl_users.FirstOrDefault(u => u.Id == model.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = model.Name;
            user.Email = model.Email;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "UserImages");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                // Optional: delete old image
                if (!string.IsNullOrEmpty(user.Image))
                {
                    var oldImagePath = Path.Combine(uploads, user.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                user.Image = fileName;
            }

            _mc.SaveChanges();
            TempData["msg"] = "Profile updated successfully!";
            return RedirectToAction("Profile");
        }

        //Appointment

        public IActionResult Appointment()
        {


            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            ViewBag.departments = _mc.tbl_department.ToList();
            ViewBag.doctors = _mc.tbl_staff.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Appointment(Booking appointment)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
                _mc.tbl_bookings.Add(appointment);
                _mc.SaveChanges();
                TempData["successs"] = "Thank you! Your message has been sent successfully. We will get back to you shortly.";
                return RedirectToAction("Appointment");
            }
            else
            {
                TempData["msgs"] = "Please login first";
                return RedirectToAction("Appointment");
            }

        }

        //Add To Cart
        public IActionResult Showcart()
        {
            int? userId = HttpContext.Session.GetInt32("userId");

            var cartItems = _mc.tbl_cart.Where(c => c.U_id == userId).ToList();

            if (cartItems.Count == 0)
            {
                ViewBag.msg = "No item in cart.";
                return View(new List<Cart>()); 
            }

            var productImages = _mc.tbl_medicines
                .Where(p => cartItems.Select(c => c.Id).Contains(p.Id))
                .ToDictionary(p => p.Id, p => p.Image);

            ViewBag.ProductImages = productImages;

            return View(cartItems);
        }



        [HttpPost]
        public IActionResult Addtocart(Cart cart)
        {
            int? userId = HttpContext.Session.GetInt32("userId");

            if (userId == null)
            {
                TempData["msg"] = "Please login to add items to your cart.";
                return RedirectToAction("Login");
            }
            cart.U_id = userId.Value;

            _mc.tbl_cart.Add(cart);  
            _mc.SaveChanges();

            TempData["msg"] = "Item added to cart successfully!";
            return RedirectToAction("Index");
        }
        public IActionResult DeleteCartItem(int id)
        {
            var cartItem = _mc.tbl_cart.FirstOrDefault(c => c.Id == id);

            if (cartItem != null)
            {
                _mc.tbl_cart.Remove(cartItem);
                _mc.SaveChanges();
                TempData["msg"] = "Item removed from cart.";
            }
            else
            {
                TempData["msg"] = "Item not found.";
            }

            return RedirectToAction("showcart");
        }
        public IActionResult OrderNow(int id)
        {
            var cartItem = _mc.tbl_cart.FirstOrDefault(c => c.Id == id);

            if (cartItem == null)
            {
                TempData["msg"] = "Cart item not found.";
                return RedirectToAction("MyCart");
            }

            var order = new Order
            {
                Cart_id = cartItem.Id,
                U_id = cartItem.U_id
            };

            _mc.tbl_order.Add(order);
            _mc.tbl_cart.Remove(cartItem); 
            _mc.SaveChanges();

            TempData["msg"] = "Order placed successfully!";
            return RedirectToAction("MyOrders");
        }

        public IActionResult MyOrders()
        {
            int? userId = HttpContext.Session.GetInt32("userId");

            var orders = _mc.tbl_order
                .Where(o => o.U_id == userId)
                .ToList();

            return View(orders);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
