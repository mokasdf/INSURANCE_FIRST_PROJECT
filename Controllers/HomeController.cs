using INSURANCE_FIRST_PROJECT.Models;
using INSURANCE_FIRST_PROJECT.services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace INSURANCE_FIRST_PROJECT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //
        private readonly ModelContext _context;

        //email 
        private readonly IEmailSender emailSender;
        public HomeController(ILogger<HomeController> logger, ModelContext _context, IEmailSender emailSender)
        {
            _logger = logger;
            this._context = _context;
            //
            this.emailSender = emailSender;
        }

        public void setviewbags()
        {
           
            //user view bag data
            ViewBag.UserID = HttpContext.Session.GetInt32("UserId");
            int useridse = (int)HttpContext.Session.GetInt32("UserId");
            ViewBag.UserName = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Fullname).Single();
            ViewBag.UserEmail = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Email).Single();
            ViewBag.UserImage = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Image).Single();
            ViewBag.RoleId = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Roleid).Single();

        }

        public async Task<IActionResult>  Index()
        {
            //HttpContext.Session.SetInt32("IdHomeIndx", 21);
            //int IdHomeIndx = (int)HttpContext.Session.GetInt32("IdHomeIndx");
            //ViewBag.IdHomeIndx=IdHomeIndx;
            ViewBag.user = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.user != null) { 
            setviewbags();
            }
            ViewBag.layout=HttpContext.Session.GetString("userlayout");
            //ViewBag.layout = "guestlayout";
            var homeContent = _context.Homes.Where(x => x.Id ==21 ).SingleOrDefault();
            var testemonials =_context.Testimonials.Include(x=>x.Useraccount).ToList();
            var subcrebtiontypes = _context.Subcrebtiontypes.ToList();
            //var result = Tuple.Create< <Home>, IEnumerable<Testimonial>>
            //                        (homeContent, testemonials);
            var dataTuple = (HomeContent: homeContent, Testemonials: testemonials, Subcrebtiontypes: subcrebtiontypes);


            //emailSender.SendEmailAsync("mohammedkabour@gmail.com", "test1", "test2test2");

            return View(dataTuple);

        }

        public IActionResult contact()
        {

            ViewBag.user = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.user != null)
            {
                setviewbags();
            }

            ViewBag.layout = HttpContext.Session.GetString("userlayout");
            //ViewBag.layout = "guestlayout";
            //var homeContent = _context.Homes.Where(x => x.Id == 21).SingleOrDefault();
            //return View(homeContent);


            //var homeContent = _context.Homes.Where(x => x.Id == 21).SingleOrDefault();
            var contactusContent = _context.Contactus.Where(x => x.Id == 21).SingleOrDefault();
            //var dataTuple = (HomeContent: homeContent, ContactusContent: contactusContent);

            return View(contactusContent);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> contact([Bind("Id,Email,Phonenumber,Fullname,Text1,Text2,Image1,siteImage1,Image2,siteImage2,Image3,siteImage3,Text3,Text4")] Contactu contactu)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(contactu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactu);
        }



        public IActionResult Privacy()
        {
            ViewBag.userName = HttpContext.Session.GetString("userName");
            return View();
        }

        public IActionResult INSURANCE()
        {
            ViewBag.layout = HttpContext.Session.GetString("userlayout");
            //ViewBag.UserID = HttpContext.Session.GetInt32("UserId");
            //if (ViewBag.UserID != null) { 
            //int useridse = (int)HttpContext.Session.GetInt32("UserId");
            //ViewBag.UserName = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Fullname).Single();
            //ViewBag.UserEmail = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Email).Single();
            //ViewBag.UserImage = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Image).Single();
            //ViewBag.RoleId = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Roleid).Single();
            //}
            ViewBag.user = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.user != null)
            {
                setviewbags();
            }


            var subcrebtiontypes = _context.Subcrebtiontypes.ToList();
            return View(subcrebtiontypes);
        }

        public IActionResult GetSubTypeByID(int id)
        {
            ViewBag.layout = HttpContext.Session.GetString("userlayout");
            ViewBag.user = HttpContext.Session.GetInt32("UserId");
            HttpContext.Session.SetInt32("subnowId", id);
            if (ViewBag.user != null)
            {
                setviewbags();
            }


            var subcrebtiontype = _context.Subcrebtiontypes.Where(x => x.Id == id).ToList();

            return View(subcrebtiontype);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}