using INSURANCE_FIRST_PROJECT.Models;
using INSURANCE_FIRST_PROJECT.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;

namespace INSURANCE_FIRST_PROJECT.Controllers
{
    public class AdminController : Controller
    {

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        //email 
        private readonly IEmailSender emailSender;
        public AdminController(ModelContext context, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender)
        {
            this._context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.emailSender = emailSender;

        }

        public void setviewbags()
        {
            //site statics data
            ViewBag.Profit = _context.Subcrebtions.Sum(x =>  x.Subcrebtiontype.Price);
            ViewBag.RegisterdUsers = _context.Useraccounts.Count();
            ViewBag.SubscribersNumber = _context.Subcrebtions.Count(user => user.State.ToLower() == "subscribed".ToLower()); ;


            //user view bag data
            ViewBag.UserID = HttpContext.Session.GetInt32("UserId");
            int useridse = (int)HttpContext.Session.GetInt32("UserId");
            ViewBag.UserName = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Fullname).Single();
            ViewBag.UserEmail = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Email).Single();
            ViewBag.UserImage = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Image).Single();
            ViewBag.RoleId = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Roleid).Single();

        }


        public IActionResult Index2()
        {
            //ViewBag.UserName = HttpContext.Session.GetString("UserName");
            //ViewBag.UserImage = HttpContext.Session.GetString("UserImage");
            //ViewBag.UserEmail = HttpContext.Session.GetString("UserEmial");
            //ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");

            //site statics data
            ViewBag.Profit = _context.Subcrebtions.Sum(x => x.State == "subscribed" ? 1 * x.Subcrebtiontype.Price : 0);
            ViewBag.RegisterdUsers = _context.Useraccounts.Count();
            ViewBag.SubscribersNumber = _context.Subcrebtions.Count(user => user.State == "subscribed"); ;


            //user view bag data
            //ViewBag.UserID = HttpContext.Session.GetInt32("UserId");
            //int useridse = (int)HttpContext.Session.GetInt32("UserId");
            //ViewBag.UserName = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Fullname).Single();
            //ViewBag.UserEmail = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Email).Single();
            //ViewBag.UserImage = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Image).Single();
            //ViewBag.RoleId = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Roleid).Single();

            // users information
            var users = _context.Useraccounts.Include(u => u.Role).ToList();
            var subcrebtiontype = _context.Subcrebtiontypes.ToList();
            var subscrebtion = _context.Subcrebtions.ToList();
            var model = Tuple.Create<IEnumerable<Useraccount>, IEnumerable<Subcrebtiontype>, IEnumerable<Subcrebtion>>(users, subcrebtiontype, subscrebtion);
            return View(model);
        }

        public IActionResult Index()
        {

            setviewbags();
            var useraccounts = _context.Useraccounts.Include(x => x.Role).ToList();
            var subcrebtions = _context.Subcrebtions.ToList();
            var subcrebtiontypes = _context.Subcrebtiontypes.ToList();



            var model = from user in useraccounts
                        join sub in subcrebtions on user.Id equals sub.Useraccountid
                        join subtype in subcrebtiontypes on sub.Subcrebtiontypeid equals subtype.Id
                        select new JoinTable { useraccount = user, subcrebtion = sub, subcrebtiontype = subtype };

            //var result = Tuple.Create<IEnumerable<Product>,
            //                          IEnumerable<Customer>,
            //                          IEnumerable<JoinTable>,
            //                          IEnumerable<ProductCustomer>>
            //                    (products, customer, model, productCustomers);
            var model2 = _context.Subcrebtions.Include(c => c.Subcrebtiontype).Include(c => c.Beneficiaries).Include(c => c.Useraccount).ToList();
            var result = Tuple.Create<IEnumerable<JoinTable>,
                                          IEnumerable<Subcrebtion>>
                                    (model, model2);


            var data = _context.Subcrebtions.Include(x=>x.Useraccount);



            int currentYear = DateTime.Now.Year;
            var model3 = _context.Subcrebtions.Include(x => x.Useraccount).Include(x => x.Subcrebtiontype).Where(x => x.Subcrebtiondate.Value.Year == currentYear).ToList();
            var monthlyProfits = model3.GroupBy(x => x.Subcrebtiondate.Value.Month).Select(g => g.Sum(x => x.Subcrebtiontype.Price)).ToList();
            ViewBag.monthlyprofits = monthlyProfits;

            List<decimal> prof = new List<decimal>();
            for (int i = 1; i < 13; i++)
            {
                var m = model3.Where(x => x.Subcrebtiondate.Value.Month == i).Sum(x => x.Subcrebtiontype.Price).Value;
                prof.Add(m);
            }
            string json = JsonConvert.SerializeObject(prof);

            ViewBag.MyList = json;
            ViewBag.prof = prof;

            return View(result);
            //return View(data);



        }




        [HttpPost]
        public IActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            var useraccounts = _context.Useraccounts.Include(x => x.Role).ToList();
            var subcrebtions = _context.Subcrebtions.ToList();
            var subcrebtiontypes = _context.Subcrebtiontypes.ToList();



            var model = from user in useraccounts
                        join sub in subcrebtions on user.Id equals sub.Useraccountid
                        join subtype in subcrebtiontypes on sub.Subcrebtiontypeid equals subtype.Id
                        select new JoinTable { useraccount = user, subcrebtion = sub, subcrebtiontype = subtype };


            var model2 = _context.Subcrebtions.Include(c => c.Subcrebtiontype).Include(c => c.Beneficiaries).Include(c => c.Useraccount).ToList();

            if (startDate == null && endDate == null)
            {
                var result = Tuple.Create<IEnumerable<JoinTable>,
                                          IEnumerable<Subcrebtion>>
                                    (model, model2);
                return View(model);
            }
            else if (startDate != null && endDate == null)
            {
                var subcrebtionnew = model2.Where(x => x.Subcrebtiondate.Value.Date >= startDate);
                var result = Tuple.Create<IEnumerable<JoinTable>,
                                          IEnumerable<Subcrebtion>>
                                    (model, subcrebtionnew);
                return View(result);
            }
            else if (startDate == null && endDate != null)
            {
                var subcrebtionnew = model2.Where(x => x.Subcrebtiondate.Value.Date <= endDate);
                var result = Tuple.Create<IEnumerable<JoinTable>,
                                          IEnumerable<Subcrebtion>>
                                    (model, subcrebtionnew);
                return View(result);
            }
            else
            {
                var subcrebtionnew = model2.Where(x => x.Subcrebtiondate.Value.Date >= startDate && x.Subcrebtiondate.Value.Date <= endDate);
                var result = Tuple.Create<IEnumerable<JoinTable>,
                                          IEnumerable<Subcrebtion>>
                                    (model, subcrebtionnew);
                return View(result);

            }



        }



        #region registers users 

        public IActionResult RegistredUser()
        {
            setviewbags();
            var useraccounts = _context.Useraccounts.Include(x => x.Role).ToList();
            var subcrebtions = _context.Subcrebtions.ToList();
            var subcrebtiontypes = _context.Subcrebtiontypes.ToList();



            var model = from user in useraccounts
                        join sub in subcrebtions on user.Id equals sub.Useraccountid
                        join subtype in subcrebtiontypes on sub.Subcrebtiontypeid equals subtype.Id
                        select new JoinTable { useraccount = user, subcrebtion = sub, subcrebtiontype = subtype };


            var model2 = _context.Subcrebtions.Include(c => c.Subcrebtiontype).Include(c => c.Beneficiaries).Include(c => c.Useraccount).ToList();
            var result = Tuple.Create<IEnumerable<JoinTable>,
                                          IEnumerable<Subcrebtion>,
                                          IEnumerable<Useraccount>>
                                    (model, model2, useraccounts);


            return View(result);
        }


        public IActionResult Userdetails(decimal? id)
        {
            setviewbags();
            //var useraccounts = _context.Useraccounts.Include(x => x.Role).ToList();
            //var subcrebtions = _context.Subcrebtions.ToList();
            //var subcrebtiontypes = _context.Subcrebtiontypes.ToList();



            //var model = from user in useraccounts
            //            join sub in subcrebtions on user.Id equals sub.Useraccountid
            //            join subtype in subcrebtiontypes on sub.Subcrebtiontypeid equals subtype.Id
            //            select new JoinTable { useraccount = user, subcrebtion = sub, subcrebtiontype = subtype };


            //var model2 = _context.Subcrebtions.Include(c => c.Subcrebtiontype).Include(c => c.Beneficiaries).Include(c => c.Useraccount).ToList();
            //var result = Tuple.Create<IEnumerable<JoinTable>,
            //                              IEnumerable<Subcrebtion>>
            //                        (model, model2);


            //return View(result);

            if (id == null || _context.Subcrebtions == null)
            {
                return NotFound();
                

            }

            var subcrebtion = _context.Subcrebtions
                .Include(s => s.Subcrebtiontype)
                .Include(s => s.Useraccount)
                .FirstOrDefault(m => m.Useraccountid == id);
            if (subcrebtion == null)
            {
                return RedirectToAction("NotSubscriedDetails", "Admin", new { id=id});
                //return NotFound();
            }


            return View(subcrebtion);


        }
        public IActionResult NotSubscriedDetails(decimal? id)
        {
            setviewbags();
           

            if (id == null || _context.Subcrebtions == null)
            {
                return NotFound();
            }

            var user = _context.Useraccounts
                .Include(s => s.Role)
                .FirstOrDefault(m => m.Id == id);
            if (user == null)
            {
                
                return NotFound();
            }


            return View(user);


        }

        #endregion


        #region subscriptions with time


        public IActionResult UsersSubscriptions()
        {
            setviewbags();


            var model = _context.Subcrebtions.Include(c => c.Subcrebtiontype).Include(c => c.Beneficiaries).Include(c => c.Useraccount).ToList();
            return View(model);


        }




        [HttpPost]
        public IActionResult UsersSubscriptions(DateTime? startDate, DateTime? endDate)
        {
            setviewbags();

            var model = _context.Subcrebtions.Include(c => c.Subcrebtiontype).Include(c => c.Beneficiaries).Include(c => c.Useraccount).ToList();

            if (startDate == null && endDate == null)
            {
                setviewbags();
                return View(model);
            }
            else if (startDate != null && endDate == null)
            {
                setviewbags();
                var subcrebtionnew = model.Where(x => x.Subcrebtiondate.Value.Date >= startDate);

                return View(subcrebtionnew);
            }
            else if (startDate == null && endDate != null)
            {
                setviewbags();
                var subcrebtionnew = model.Where(x => x.Subcrebtiondate.Value.Date <= endDate);

                return View(subcrebtionnew);
            }
            else
            {
                setviewbags();
                var subcrebtionnew = model.Where(x => x.Subcrebtiondate.Value.Date >= startDate && x.Subcrebtiondate.Value.Date <= endDate);

                return View(subcrebtionnew);

            }



        }





        #endregion


        #region reports
        public IActionResult Report()
        {
            setviewbags();

            ViewBag.Profit = _context.Subcrebtions.Sum(x => x.Subcrebtiontype.Price);
            ViewBag.RegisterdUsers = _context.Useraccounts.Count();
            ViewBag.SubscribersNumber = _context.Subcrebtions.Count(user => user.State.ToLower() == "subscribed".ToLower()); ;


            var model = _context.Subcrebtions.Include(c => c.Subcrebtiontype).Include(c => c.Beneficiaries).Include(c => c.Useraccount).ToList();
            return View(model);


        }




        [HttpPost]
        public IActionResult Report(int? month, int? year)
        {
            setviewbags();
            int s = DateTime.Now.Date.Month;
            var model = _context.Subcrebtions.Include(c => c.Subcrebtiontype).Include(c => c.Beneficiaries).Include(c => c.Useraccount).ToList();

            if (month == 0 && year != null)
            {
                
                var subcrebtionnew = model.Where(x => x.Subcrebtiondate.Value.Date.Year == year);
                ViewBag.Profit = subcrebtionnew.Sum(x => x.Subcrebtiontype.Price);
                ViewBag.RegisterdUsers = subcrebtionnew.Count();
                ViewBag.SubscribersNumber = subcrebtionnew.Count(user => user.State.ToLower() == "subscribed".ToLower()); ;

                return View(subcrebtionnew);
            }
            else if (month != 0 && year != null)
            {
                
                var subcrebtionnew = model.Where(x => x.Subcrebtiondate.Value.Date.Year == year & x.Subcrebtiondate.Value.Date.Month == month);
                ViewBag.Profit = subcrebtionnew.Sum(x => x.Subcrebtiontype.Price);
                ViewBag.RegisterdUsers = subcrebtionnew.Count();
                ViewBag.SubscribersNumber = subcrebtionnew.Count(user => user.State.ToLower() == "subscribed".ToLower()); ;

                return View(subcrebtionnew);
            }

            return View(model);


        }





        #endregion


        #region requests Functions Beneficiaries
        public IActionResult BeneficiariesRequests()
        {
            setviewbags();
            var useraccounts = _context.Useraccounts.Include(x => x.Role).ToList();
            var subcrebtions = _context.Subcrebtions.Include(x => x.Subcrebtiontype).ToList();
            var subcrebtiontypes = _context.Subcrebtiontypes.ToList();
            var testimonial = _context.Testimonials.ToList();
            var beneficiaries = _context.Beneficiaries.Include(x => x.Subcrebtion).ToList();

            var model = from user in useraccounts
                        join sub in subcrebtions on user.Id equals sub.Useraccountid
                        join bene in beneficiaries on sub.Id equals bene.Subcrebtionid
                        select new JoinTable { useraccount = user, subcrebtion = sub, beneficiary = bene };


            //var result = Tuple.Create<IEnumerable<Product>,
            //                          IEnumerable<Customer>,
            //                          IEnumerable<JoinTable>,
            //                          IEnumerable<ProductCustomer>>
            //                    (products, customer, model, productCustomers);

            var model2 = _context.Subcrebtions.Include(c => c.Subcrebtiontype).Include(c => c.Beneficiaries).Include(c => c.Useraccount).ToList();
            var result = Tuple.Create<IEnumerable<JoinTable>,
                                          IEnumerable<Subcrebtion>>
                                    (model, model2);


            return View(model);


        }


        public IActionResult BeneficiaryRequest(decimal? id)
        {
            setviewbags();
            if (id == null || _context.Beneficiaries == null)
            {
                return NotFound();
            }

            var beneficiary =  _context.Beneficiaries
                .Include(b => b.Subcrebtion).Include(b => b.Subcrebtion.Useraccount)
                .Include(b => b.Subcrebtion.Subcrebtiontype)
                .FirstOrDefault(m => m.Id == id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            return View(beneficiary);


        }

        [HttpPost]
        
        public async Task<IActionResult> BeneficiaryRequest(decimal id, [Bind("Id,State")] Beneficiary beneficiary, string? state = "on hold")
        {
            if (id != beneficiary.Id)
            {
                return NotFound();
            }

        
            try
            {
                beneficiary.State = state;

                beneficiary.Name = _context.Beneficiaries.Where(_ => _.Id == id).AsNoTracking<Beneficiary>().SingleOrDefault().Name;
                beneficiary.Document = _context.Beneficiaries.Where(_ => _.Id == id).AsNoTracking<Beneficiary>().SingleOrDefault().Document;
                beneficiary.BeneficiaryDocument = _context.Beneficiaries.Where(_ => _.Id == id).AsNoTracking<Beneficiary>().SingleOrDefault().BeneficiaryDocument;
                beneficiary.Relationtype = _context.Beneficiaries.Where(_ => _.Id == id).AsNoTracking<Beneficiary>().SingleOrDefault().Relationtype;
                beneficiary.Subcrebtionid = _context.Beneficiaries.Where(_ => _.Id == id).AsNoTracking<Beneficiary>().SingleOrDefault().Subcrebtionid;
                beneficiary.Beneficiarystartdate = _context.Beneficiaries.Where(_ => _.Id == id).AsNoTracking<Beneficiary>().SingleOrDefault().Beneficiarystartdate;
                beneficiary.Extra = _context.Beneficiaries.Where(_ => _.Id == id).AsNoTracking<Beneficiary>().SingleOrDefault().Extra;



                _context.Update(beneficiary);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                    
            }
            if(beneficiary.State.ToLower() == "Accepted".ToLower())
            {
                var be = _context.Beneficiaries.Include(x=>x.Subcrebtion).Include(x => x.Subcrebtion.Useraccount).Where(x => x.Id == beneficiary.Id).FirstOrDefault();
                string username = _context.Useraccounts.Where(x => x.Id == be.Subcrebtion.Useraccount.Id).Select(x => x.Fullname).Single();

                string subject = "Your beneficary has been successfully Added.";
                string messaage = $"Dear {username},\r\n\r\nWe are thrilled to inform you that your Beneficary has been successfully Added on you subscribtion. Thank you for choosing to be part of our community. Your trust and support mean a lot to us.\r\n\r\nAs a token of our appreciation, we have attached your subscription invoice in PDF format to this email. You can find all the details of your subscription, including payment information, within the document.\r\n\r\n\r\nIf you have any questions or require further assistance, please don't hesitate to reach out to our dedicated support team. We are here to help you with any inquiries or concerns you may have.\r\n\r\nOnce again, we want to express our gratitude for your subscription. We are looking forward to providing you with exceptional service and value throughout your subscription journey.\r\n\r\nThank you for being a part of our community.\r\n\r\nBest regards,\r\n\r\nMohammad khabour\r\nCEO\r\nFunder";

                emailSender.SendEmailAsync("mohammedkabour@gmail.com", subject, messaage, " ", beneficiary.Name, " ", " ", false);
            }
            return RedirectToAction(nameof(Index));
            
           
        }



        #endregion



        #region requests Functions Testemonials


        //testimonial function for view all testimoals

        public IActionResult UsersTestimonials()
        {
            setviewbags();
            var useraccounts = _context.Useraccounts.Include(x => x.Role).ToList();
            var testimonial = _context.Testimonials.Include(x => x.Useraccount).ToList();

            var model = from testi in testimonial
                        join user in useraccounts on testi.Useraccountid equals user.Id
                        select new JoinTable { useraccount = user, testimonial = testi };

            return View(model);


        }


        //testimonial function for view a specific user's testimonial

        public IActionResult UserTestimonial(decimal? id)
        {
            setviewbags();
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = _context.Testimonials
                .Include(t => t.Useraccount)
                .FirstOrDefault(m => m.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);


        }

        //testimonial function for set the state and the messae for a specific user's testimonial
        [HttpPost]

        public async Task<IActionResult> UserTestimonial(decimal id, [Bind("Id,State,Message,Testimonialdate,Extra,Useraccountid")] Testimonial testimonial,string? state="on hold")
        {
            if (id != testimonial.Id)
            {
                return NotFound();
            }
            try
            {
                testimonial.State = state;

                _context.Update(testimonial);
                await _context.SaveChangesAsync();
            }
            catch { }
            
            return RedirectToAction("UsersTestimonials", "Admin");
            
            
        }


        

        #endregion





        #region Profile functions View/Edit
        public async Task<IActionResult> Profile(decimal? id)
        {
            //
            ViewBag.UserID = HttpContext.Session.GetInt32("UserId");
            int useridse = (int)HttpContext.Session.GetInt32("UserId");
            ViewBag.UserName = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Fullname).Single();
            ViewBag.UserEmail = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Email).Single();
            ViewBag.UserImage = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Image).Single();
            ViewBag.RoleId = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Roleid).Single();
            //
            if (id == null || _context.Useraccounts == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccounts
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (useraccount == null)
            {
                return NotFound();
            }

            return View(useraccount);
        }

        //public IActionResult EditProfile()
        //{
        //    return View();
        //}


        // GET: Useraccounts/Edit/5
        public async Task<IActionResult> EditProfile(decimal? id)
        {
            //
            ViewBag.UserID = HttpContext.Session.GetInt32("UserId");
            int useridse = (int)HttpContext.Session.GetInt32("UserId");
            ViewBag.UserName = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Fullname).Single();
            ViewBag.UserEmail = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Email).Single();
            ViewBag.UserImage = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Image).Single();
            ViewBag.RoleId = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Roleid).Single();
            //


            if (id == null || _context.Useraccounts == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccounts.FindAsync(id);
            if (useraccount == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Id", "Id", useraccount.Roleid);
            return View(useraccount);
        }

        // POST: Useraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Fullname,Email,Image,UserImageFile,Extra,Roleid")] Useraccount useraccount,string chosenFileName)
        {
            if (id != useraccount.Id)
            {
                return NotFound();
            }
            
            //if (ModelState.IsValid)
            {
                try
                {
                    //if (chosenFileName != null)
                    //{
                    //    useraccount.UserImageFile.FileName = chosenFileName;
                    //}

                    
                        //
                        if (useraccount.UserImageFile != null)
                        {
                            string wwwRootPath = webHostEnvironment.WebRootPath;

                            string fileName = Guid.NewGuid().ToString() + useraccount.UserImageFile.FileName;

                            string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await useraccount.UserImageFile.CopyToAsync(fileStream);
                            }

                            useraccount.Image = fileName;
                        }
                    //
                    useraccount.Password = _context.Useraccounts.Where( _=> _.Id == id).AsNoTracking<Useraccount>().SingleOrDefault().Password;
                    _context.Update(useraccount);
                        await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!UseraccountsController.UseraccountExists(useraccount.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
                ViewData["Roleid"] = new SelectList(_context.Roles, "Id", "Id", useraccount.Roleid);
                return View(useraccount);
            }




        public async Task<IActionResult> EditPassword(decimal? id)
        {
            //
            ViewBag.UserID = HttpContext.Session.GetInt32("UserId");
            int useridse = (int)HttpContext.Session.GetInt32("UserId");
            ViewBag.UserName = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Fullname).Single();
            ViewBag.UserEmail = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Email).Single();
            ViewBag.UserImage = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Image).Single();
            ViewBag.RoleId = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Roleid).Single();
            //

            if (id == null || _context.Useraccounts == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccounts.FindAsync(id);
            if (useraccount == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Id", "Id", useraccount.Roleid);
            return View(useraccount);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(decimal id, [Bind("Id")] Useraccount useraccount,string Newpassword,string  conPassword,string re_Newpassword)
        {
            if (id != useraccount.Id)
            {
                return NotFound();
            }
            if (Newpassword != re_Newpassword)
            {
                int useridse = (int)HttpContext.Session.GetInt32("UserId");

                ViewBag.UserName = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Fullname).Single();
                ViewBag.UserEmail = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Email).Single();
                ViewBag.UserImage = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Image).Single();
                ViewBag.Error = "Wrong password";
                return View(useraccount);
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    int useridse = (int)HttpContext.Session.GetInt32("UserId");
                    string pass = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Password).Single();
                    if (pass == conPassword) {
                        useraccount.Password = Newpassword;
                        useraccount.Email = _context.Useraccounts.Where(_ => _.Id == id).AsNoTracking<Useraccount>().SingleOrDefault().Email;
                        useraccount.Fullname = _context.Useraccounts.Where(_ => _.Id == id).AsNoTracking<Useraccount>().SingleOrDefault().Fullname;
                        useraccount.Roleid = _context.Useraccounts.Where(_ => _.Id == id).AsNoTracking<Useraccount>().SingleOrDefault().Roleid;
                        useraccount.Image = _context.Useraccounts.Where(_ => _.Id == id).AsNoTracking<Useraccount>().SingleOrDefault().Image;
                        useraccount.UserImageFile = _context.Useraccounts.Where(_ => _.Id == id).AsNoTracking<Useraccount>().SingleOrDefault().UserImageFile;
                        useraccount.Extra = _context.Useraccounts.Where(_ => _.Id == id).AsNoTracking<Useraccount>().SingleOrDefault().Extra;


                        //useraccount.Password = _context.Useraccounts.Where(_ => _.Id == id).AsNoTracking<Useraccount>().SingleOrDefault().Password;
                        _context.Update(useraccount);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Profile", "Admin", new { id = useraccount.Id });
                    }
                    else
                    {
                        ViewBag.UserName = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Fullname).Single();
                        ViewBag.UserEmail = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Email).Single();
                        ViewBag.UserImage = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Image).Single();
                        ViewBag.Error = "Wrong password";
                        return View(useraccount);
                    }
                    

                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!UseraccountsController.UseraccountExists(useraccount.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Id", "Id", useraccount.Roleid);
            return View(useraccount);
        }


        #endregion



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}

