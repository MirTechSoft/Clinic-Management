using Clinic_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management.Controllers
{
    public class AdminController : Controller
    {
        private myContext _cm;
        private IWebHostEnvironment _env;
        public AdminController(myContext context, IWebHostEnvironment env)
        {
            _cm = context;
            _env = env;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            return View();
        }

        // User Site

        public IActionResult AddU()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddU(User user)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }

            _cm.tbl_users.Add(user);
            _cm.SaveChanges();
            return RedirectToAction("ShowU");
        }

        public IActionResult ShowU()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_users.ToList();
            return View(std);
        }

        [HttpGet]
        public IActionResult ShowU(string search)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            List<User> user = new List<User>();
            if(string.IsNullOrEmpty(search))
            {
                user = _cm.tbl_users.ToList();
            }
            else
            {
                user = _cm.tbl_users.FromSqlInterpolated(
                    $"select * from tbl_users where name  like '%' +{search}+ '%'").ToList();
                if(user.Count == 0)
                {
                    ViewBag.msg = $"No Record Found for '{search}'";
                }
            }
            return View(user);
        }

        public IActionResult Delete(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }

            var std = _cm.tbl_users.Find(Id);
            _cm.tbl_users.Remove(std);
            _cm.SaveChanges();
            return RedirectToAction("ShowU");
        }

        public IActionResult EditU(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var edu = _cm.tbl_users.Find(Id);
            return View(edu);
        }

        [HttpPost]
        public IActionResult EditU(int Id,User admin)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            _cm.tbl_users.Update(admin);
            _cm.SaveChanges();
            return RedirectToAction("ShowU");
        }

        // Admin Site

        public IActionResult AddA()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddA(Admin admin)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            _cm.tbl_admins.Add(admin);
            _cm.SaveChanges();
            return RedirectToAction("ShowA");
        }

        public IActionResult ShowA()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_admins.ToList();
            return View(std);
        }

        [HttpGet]
        public IActionResult ShowA(string search)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            List<Admin> admin = new List<Admin>();
            if (string.IsNullOrEmpty(search))
            {
                admin = _cm.tbl_admins.ToList();
            }
            else
            {
                admin = _cm.tbl_admins.FromSqlInterpolated(
                    $"select * from tbl_admins where name  like '%' +{search}+ '%'").ToList();
                if (admin.Count == 0)
                {
                    ViewBag.msg = $"No Record Found for '{search}'";
                }
            }
            return View(admin);
        }

        public IActionResult DeleteA(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_admins.Find(Id);
            _cm.tbl_admins.Remove(std);
            _cm.SaveChanges();
            return RedirectToAction("ShowA");
        }

        public IActionResult EditA(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var edu = _cm.tbl_admins.Find(Id);
            return View(edu);
        }

        [HttpPost]
        public IActionResult EditA(int Id, Admin user)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            _cm.tbl_admins.Update(user);
            _cm.SaveChanges();
            return RedirectToAction("ShowA");
        }

        // Admin Login & Logout & Forget

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string uemail, string upass)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var row = _cm.tbl_admins.FirstOrDefault(u => u.Email == uemail);
            if (row != null && row.Password == upass)
            {
                HttpContext.Session.SetString("name", row.Name);
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
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            if (HttpContext.Session.GetString("name") != null)
            {
                HttpContext.Session.Remove("name");
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");
        }

        public IActionResult Forget()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Forget(string uemail)
        {
            var row = _cm.tbl_admins.FirstOrDefault(u => u.Email == uemail);
            if (row != null)
            {
                return RedirectToAction("Newpasswords", new { id = row.Id });
            }
            else
            {
                ViewBag.msg = "Email Does not Exist";
            }
            return View();
        }

        public IActionResult Newpasswords(int Id)
        {
            ViewBag.id = Id;
            return View();
        }

        [HttpPost]
        public IActionResult Newpasswords(int Id, string pass, string cpass)
        {
            var row = _cm.tbl_admins.FirstOrDefault(u => u.Id == Id);
            if (row != null)
            {
                if (pass == cpass)
                {
                    row.Password = pass;
                    _cm.SaveChanges();
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

        // Services

        public IActionResult Addser()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Addser(Services services, IFormFile Image)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            String filename = Path.GetFileName(Image.FileName);
            String filePath = Path.Combine(_env.WebRootPath, "Services", filename);
            FileStream fs = new FileStream(filePath, FileMode.Create);
            Image.CopyTo(fs);

            services.Image = filename;
            _cm.tbl_services.Add(services);
            _cm.SaveChanges();
            return RedirectToAction("Showser");
        }

        public IActionResult Showser()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_services.ToList();
            return View(std);
        }

        [HttpGet]
        public IActionResult Showser(string search)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            List<Services> services = new List<Services>();
            if (string.IsNullOrEmpty(search))
            {
                services = _cm.tbl_services.ToList();
            }
            else
            {
                services = _cm.tbl_services.FromSqlInterpolated(
                    $"select * from tbl_services where name  like '%' +{search}+ '%'").ToList();
                if (services.Count == 0)
                {
                    ViewBag.msg = $"No Record Found for '{search}'";
                }
            }
            return View(services);
        }

        public IActionResult Deleteser(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_services.Find(Id);
            _cm.tbl_services.Remove(std);
            _cm.SaveChanges();
            return RedirectToAction("Showser");
        }

        public IActionResult Editser(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var edu = _cm.tbl_services.Find(Id);
            return View(edu);
        }

        [HttpPost]
        public IActionResult Editser(int Id, Services services, IFormFile Image)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            if (Image != null && Image.Length > 0)
            {
                var filename = Path.GetFileName(Image.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "Services", filename);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fs);
                }
                services.Image = filename;
            }
            else
            {
                var existingservices = _cm.tbl_services.AsNoTracking().FirstOrDefault(show => show.Id == Id);
                if(existingservices != null)
                {
                    services.Image = existingservices.Image;
                }
            }


            _cm.tbl_services.Update(services);
            _cm.SaveChanges();
            return RedirectToAction("Showser");
        }

        //Department

        public IActionResult Adddep()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adddep(Department depart, IFormFile Image)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            String filename = Path.GetFileName(Image.FileName);
            String filePath = Path.Combine(_env.WebRootPath, "Department", filename);
            FileStream fs = new FileStream(filePath, FileMode.Create);
            Image.CopyTo(fs);

            depart.Image = filename;
            _cm.tbl_department.Add(depart);
            _cm.SaveChanges();
            return RedirectToAction("Showdep");
        }

        public IActionResult Showdep()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_department.ToList();
            return View(std);
        }

        [HttpGet]
        public IActionResult Showdep(string search)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            List<Department> department = new List<Department>();
            if (string.IsNullOrEmpty(search))
            {
                department = _cm.tbl_department.ToList();
            }
            else
            {
                department = _cm.tbl_department.FromSqlInterpolated(
                    $"select * from tbl_department where name  like '%' +{search}+ '%'").ToList();
                if (department.Count == 0)
                {
                    ViewBag.msg = $"No Record Found for '{search}'";
                }
            }
            return View(department);
        }

        public IActionResult Deletedep(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_department.Find(Id);
            _cm.tbl_department.Remove(std);
            _cm.SaveChanges();
            return RedirectToAction("Showdep");
        }

        public IActionResult Editdep(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var edu = _cm.tbl_department.Find(Id);
            return View(edu);
        }

        [HttpPost]
        public IActionResult Editdep(int Id, Department depart, IFormFile Image)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            if (Image != null && Image.Length > 0)
            {
                var filename = Path.GetFileName(Image.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "Department", filename);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fs);
                }
                depart.Image = filename;
            }
            else
            {
                var existingservices = _cm.tbl_services.AsNoTracking().FirstOrDefault(show => show.Id == Id);
                if (existingservices != null)
                {
                    depart.Image = existingservices.Image;
                }
            }


            _cm.tbl_department.Update(depart);
            _cm.SaveChanges();
            return RedirectToAction("Showdep");
        }

        //FAQ's

        public IActionResult Addfaq()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Addfaq(FAQS faq)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            _cm.tbl_faqs.Add(faq);
            _cm.SaveChanges();
            return RedirectToAction("Showfaq");
        }

        public IActionResult Showfaq()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_faqs.ToList();
            return View(std);
        }

        [HttpGet]
        public IActionResult Showfaq(string search)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            List<FAQS> FAQs = new List<FAQS>();
            if (string.IsNullOrEmpty(search))
            {
                FAQs = _cm.tbl_faqs.ToList();
            }
            else
            {
                FAQs = _cm.tbl_faqs.FromSqlInterpolated(
                    $"select * from tbl_faqs where Questions like '%' +{search}+ '%'").ToList();
                if (FAQs.Count == 0)
                {
                    ViewBag.msg = $"No Record Found for '{search}'";
                }
            }
            return View(FAQs);
        }

        public IActionResult Deletefaq(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_faqs.Find(Id);
            _cm.tbl_faqs.Remove(std);
            _cm.SaveChanges();
            return RedirectToAction("Showfaq");
        }

        public IActionResult Editfaq(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }

            var edu = _cm.tbl_faqs.Find(Id);
            return View(edu);
        }

        [HttpPost]
        public IActionResult Editfaq(int Id, FAQS faq)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            _cm.tbl_faqs.Update(faq);
            _cm.SaveChanges();
            return RedirectToAction("Showfaq");
        }

        //Medicines

        public IActionResult Addmed()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Addmed(Medicines medicines, IFormFile Image)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            String filename = Path.GetFileName(Image.FileName);
            String filePath = Path.Combine(_env.WebRootPath, "Medicines", filename);
            FileStream fs = new FileStream(filePath, FileMode.Create);
            Image.CopyTo(fs);

            medicines.Image = filename;
            _cm.tbl_medicines.Add(medicines);
            _cm.SaveChanges();
            return RedirectToAction("Showmed");
        }

        public IActionResult Showmed()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_medicines.ToList();
            return View(std);
        }

        [HttpGet]
        public IActionResult Showmed(string search)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            List<Medicines> medicines = new List<Medicines>();
            if (string.IsNullOrEmpty(search))
            {
                medicines = _cm.tbl_medicines.ToList();
            }
            else
            {
                medicines = _cm.tbl_medicines.FromSqlInterpolated(
                    $"select * from tbl_medicines where name  like '%' +{search}+ '%'").ToList();
                if (medicines.Count == 0)
                {
                    ViewBag.msg = $"No Record Found for '{search}'";
                }
            }
            return View(medicines);
        }

        public IActionResult Deletemed(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_medicines.Find(Id);
            _cm.tbl_medicines.Remove(std);
            _cm.SaveChanges();
            return RedirectToAction("Showmed");
        }

        public IActionResult Editmed(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var edu = _cm.tbl_medicines.Find(Id);
            return View(edu);
        }

        [HttpPost]
        public IActionResult Editmed(int Id, Medicines medicines, IFormFile Image)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            if (Image != null && Image.Length > 0)
            {
                var filename = Path.GetFileName(Image.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "Medicines", filename);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fs);
                }
                medicines.Image = filename;
            }
            else
            {
                var existingservices = _cm.tbl_medicines.AsNoTracking().FirstOrDefault(show => show.Id == Id);
                if (existingservices != null)
                {
                    medicines.Image = existingservices.Image;
                }
            }


            _cm.tbl_medicines.Update(medicines);
            _cm.SaveChanges();
            return RedirectToAction("Showmed");
        }

        //Apparatus

        public IActionResult Addapp()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Addapp(Apparatus apparatus, IFormFile Image)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            String filename = Path.GetFileName(Image.FileName);
            String filePath = Path.Combine(_env.WebRootPath, "Apparatus", filename);
            FileStream fs = new FileStream(filePath, FileMode.Create);
            Image.CopyTo(fs);

            apparatus.Image = filename;
            _cm.tbl_apparatus.Add(apparatus);
            _cm.SaveChanges();
            return RedirectToAction("Showapp");
        }

        public IActionResult Showapp()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_apparatus.ToList();
            return View(std);
        }

        [HttpGet]
        public IActionResult Showapp(string search)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            List<Apparatus> apparatus = new List<Apparatus>();
            if (string.IsNullOrEmpty(search))
            {
                apparatus = _cm.tbl_apparatus.ToList();
            }
            else
            {
                apparatus = _cm.tbl_apparatus.FromSqlInterpolated(
                    $"select * from tbl_apparatus where name  like '%' +{search}+ '%'").ToList();
                if (apparatus.Count == 0)
                {
                    ViewBag.msg = $"No Record Found for '{search}'";
                }
            }
            return View(apparatus);
        }

        public IActionResult Deleteapp(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_apparatus.Find(Id);
            _cm.tbl_apparatus.Remove(std);
            _cm.SaveChanges();
            return RedirectToAction("Showapp");
        }

        public IActionResult Editapp(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var edu = _cm.tbl_apparatus.Find(Id);
            return View(edu);
        }

        [HttpPost]
        public IActionResult Editapp(int Id, Apparatus apparatus, IFormFile Image)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            if (Image != null && Image.Length > 0)
            {
                var filename = Path.GetFileName(Image.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "Apparatus", filename);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fs);
                }
                apparatus.Image = filename;
            }
            else
            {
                var existingservices = _cm.tbl_apparatus.AsNoTracking().FirstOrDefault(show => show.Id == Id);
                if (existingservices != null)
                {
                    apparatus.Image = existingservices.Image;
                }
            }


            _cm.tbl_apparatus.Update(apparatus);
            _cm.SaveChanges();
            return RedirectToAction("Showapp");
        }

        //Staff

        public IActionResult Addstaff()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Addstaff(Staff staff, IFormFile Image)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            String filename = Path.GetFileName(Image.FileName);
            String filePath = Path.Combine(_env.WebRootPath, "Staff", filename);
            FileStream fs = new FileStream(filePath, FileMode.Create);
            Image.CopyTo(fs);

            staff.Image = filename;
            _cm.tbl_staff.Add(staff);
            _cm.SaveChanges();
            return RedirectToAction("Showstaff");
        }

        public IActionResult Showstaff()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_staff.ToList();
            return View(std);
        }

        [HttpGet]
        public IActionResult Showstaff(string search)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            List<Staff> staff = new List<Staff>();
            if (string.IsNullOrEmpty(search))
            {
                staff = _cm.tbl_staff.ToList();
            }
            else
            {
                staff = _cm.tbl_staff.FromSqlInterpolated(
                    $"select * from tbl_staff where name  like '%' +{search}+ '%'").ToList();
                if (staff.Count == 0)
                {
                    ViewBag.msg = $"No Record Found for '{search}'";
                }
            }
            return View(staff);
        }
        public IActionResult Deletestaff(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_staff.Find(Id);
            _cm.tbl_staff.Remove(std);
            _cm.SaveChanges();
            return RedirectToAction("Showstaff");
        }

        public IActionResult Editstaff(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var edu = _cm.tbl_staff.Find(Id);
            return View(edu);
        }

        [HttpPost]
        public IActionResult Editstaff(int Id, Staff staff, IFormFile Image)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            if (Image != null && Image.Length > 0)
            {
                var filename = Path.GetFileName(Image.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "Staff", filename);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fs);
                }
                staff.Image = filename;
            }
            else
            {
                var existingservices = _cm.tbl_staff.AsNoTracking().FirstOrDefault(show => show.Id == Id);
                if (existingservices != null)
                {
                    staff.Image = existingservices.Image;
                }
            }


            _cm.tbl_staff.Update(staff);
            _cm.SaveChanges();
            return RedirectToAction("Showstaff");
        }

        //Contact

        public IActionResult Showcon()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_contact_us.ToList();
            return View(std);
        }

        [HttpGet]
        public IActionResult Showcon(string search)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            List<Contact_us> contact_us = new List<Contact_us>();
            if (string.IsNullOrEmpty(search))
            {
                contact_us = _cm.tbl_contact_us.ToList();
            }
            else
            {
                contact_us = _cm.tbl_contact_us.FromSqlInterpolated(
                    $"select * from tbl_contact_us where name  like '%' +{search}+ '%'").ToList();
                if (contact_us.Count == 0)
                {
                    ViewBag.msg = $"No Record Found for '{search}'";
                }
            }
            return View(contact_us);
        }

        public IActionResult Deletecon(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_contact_us.Find(Id);
            _cm.tbl_contact_us.Remove(std);
            _cm.SaveChanges();
            return RedirectToAction("Showcon");
        }

        //Feedback

        public IActionResult Showfeed()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_feedback.ToList();
            return View(std);
        }

        [HttpGet]
        public IActionResult Showfeed(string search)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            List<Feedback> feedback = new List<Feedback>();
            if (string.IsNullOrEmpty(search))
            {
                feedback = _cm.tbl_feedback.ToList();
            }
            else
            {
                feedback = _cm.tbl_feedback.FromSqlInterpolated(
                    $"select * from tbl_feedback where name  like '%' +{search}+ '%'").ToList();
                if (feedback.Count == 0)
                {
                    ViewBag.msg = $"No Record Found for '{search}'";
                }
            }
            return View(feedback);
        }

        public IActionResult Deletefeed(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_feedback.Find(Id);
            _cm.tbl_feedback.Remove(std);
            _cm.SaveChanges();
            return RedirectToAction("Showfeed");
        }

        //Cart

        public IActionResult Showcart()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_cart.ToList();
            return View(std);
        }

        [HttpGet]
        public IActionResult Showcart(string search)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            List<Cart> cart = new List<Cart>();
            if (string.IsNullOrEmpty(search))
            {
                cart = _cm.tbl_cart.ToList();
            }
            else
            {
                cart = _cm.tbl_cart.FromSqlInterpolated(
                    $"select * from tbl_cart where name  like '%' +{search}+ '%'").ToList();
                if (cart.Count == 0)
                {
                    ViewBag.msg = $"No Record Found for '{search}'";
                }
            }
            return View(cart);
        }

        public IActionResult Deletecart(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_cart.Find(Id);
            _cm.tbl_cart.Remove(std);
            _cm.SaveChanges();
            return RedirectToAction("Showcart");
        }
        //Order
        public IActionResult Showorder()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_order.ToList();
            return View(std);
        }

        [HttpGet]
        public IActionResult Showorder(string search)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            List<Order> order = new List<Order>();
            if (string.IsNullOrEmpty(search))
            {
                order = _cm.tbl_order.ToList();
            }
            else
            {
                order = _cm.tbl_order.FromSqlInterpolated(
                    $"select * from tbl_order where Cart_id  like '%' +{search}+ '%'").ToList();
                if (order.Count == 0)
                {
                    ViewBag.msg = $"No Record Found for '{search}'";
                }
            }
            return View(order);
        }

        public IActionResult Deleteorder(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_order.Find(Id);
            _cm.tbl_order.Remove(std);
            _cm.SaveChanges();
            return RedirectToAction("Showorder");
        }
        //Appointment

        public IActionResult Showappoint()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_bookings.ToList();
            return View(std);
        }

        [HttpGet]
        public IActionResult Showappoints(string search)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            List<Booking> book = new List<Booking>();
            if (string.IsNullOrEmpty(search))
            {
                book = _cm.tbl_bookings.ToList();
            }
            else
            {
                book = _cm.tbl_bookings.FromSqlInterpolated(
                    $"select * from tbl_bookings where name  like '%' +{search}+ '%'").ToList();
                if (book.Count == 0)
                {
                    ViewBag.msg = $"No Record Found for '{search}'";
                }
            }
            return View(book);
        }

        public IActionResult Deleteappoint(int Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }
            var std = _cm.tbl_bookings.Find(Id);
            _cm.tbl_bookings.Remove(std);
            _cm.SaveChanges();
            return RedirectToAction("Showappoint");
        }

    }
}
