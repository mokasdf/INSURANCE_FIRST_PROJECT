using INSURANCE_FIRST_PROJECT.Models;
using Microsoft.AspNetCore.Mvc;

namespace INSURANCE_FIRST_PROJECT.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AuthenticationController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }



        public IActionResult Register()
        {
            return View();
        }


        public IActionResult Login()
        {
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Fullname,Email,Password,Image,UserImageFile,Extra,Roleid")] Useraccount Register,string? rePassword)
        {

            if (ModelState.IsValid)
            {
                if (Register.UserImageFile != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;



                    string fileName = Guid.NewGuid().ToString() + Register.UserImageFile.FileName;



                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);



                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await Register.UserImageFile.CopyToAsync(fileStream);
                    }



                    Register.Image = fileName;
                }

                if(Register.Password != rePassword)
                {
                    ViewBag.Error = "rep asssword is wrong";
                    return View(Register);
                }

                var user = _context.Useraccounts.Where(x => x.Email == Register.Email).FirstOrDefault();
                if (user == null)
                {
                    Register.Roleid = 2;
                    _context.Add(Register);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetString("Email", Register.Email);
                    return RedirectToAction("Login", "Authentication");
                }
                else
                {
                    ViewBag.Error = "Email is alraedy used try another one";
                }
            }
            return View(Register);
        }




        [HttpPost]
        public async Task<IActionResult> Login([Bind("Id,Email,Password")] Useraccount userlogin)
        {
            var auth = _context.Useraccounts.Where(x => x.Email == userlogin.Email && x.Password == userlogin.Password).FirstOrDefault();

            if (auth != null)
            {
                //var user = _context.Useraccounts.Where(x => x.Id == auth.Id).FirstOrDefault();
                switch (auth.Roleid)
                {
                    case 1:
                        
                        //HttpContext.Session.SetString("UserName", auth.Fullname);
                        //HttpContext.Session.SetString("UserEmial", auth.Email);
                        //HttpContext.Session.SetString("UserImage", auth.Image);
                        HttpContext.Session.SetInt32("UserId", (int) auth.Id);
                        HttpContext.Session.SetString("userlayout", "userlayout");

                        //HttpContext.Session.SetInt32("RoleId", (int)auth.Roleid);

                        return RedirectToAction("Index", "Admin");
                    case 2:
                       
                        //HttpContext.Session.SetString("userName", auth.Fullname);
                        HttpContext.Session.SetInt32("UserId", (int)auth.Id);
                        HttpContext.Session.SetString("userlayout", "userlayout");
                        return RedirectToAction("Index", "Home");

                }
            }
            else
            {
                ViewBag.Error = "Wrong password or Email";
            }
            return View(userlogin);

        }
    }
}
