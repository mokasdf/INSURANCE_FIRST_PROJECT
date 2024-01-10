using INSURANCE_FIRST_PROJECT.Models;
using INSURANCE_FIRST_PROJECT.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Security.Principal;

namespace INSURANCE_FIRST_PROJECT.Controllers
{
    public class UserController : Controller
    {

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        //email 
        private readonly IEmailSender emailSender;
        public UserController(ModelContext context, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender)
        {
            this._context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.emailSender = emailSender;

        }
        public IActionResult Index()
        {
            //HttpContext.Session.SetString("layout2", "userlayout");
            ViewBag.UserID = HttpContext.Session.GetInt32("UserId");
            int useridse = (int)HttpContext.Session.GetInt32("UserId");
            ViewBag.UserName = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Fullname).Single();
            ViewBag.UserEmail = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Email).Single();
            ViewBag.UserImage = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Image).Single();
            ViewBag.RoleId = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Roleid).Single();
            ViewBag.layout = HttpContext.Session.GetString("userlayout");
            

            return View();
            //return RedirectToAction("Index", "Home");
        }
        public IActionResult INSURANCE()
        {
            //HttpContext.Session.SetString("layout2", "userlayout");
            ViewBag.UserID = HttpContext.Session.GetInt32("UserId");
            int useridse = (int)HttpContext.Session.GetInt32("UserId");
            ViewBag.UserName = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Fullname).Single();
            ViewBag.UserEmail = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Email).Single();
            ViewBag.UserImage = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Image).Single();
            ViewBag.RoleId = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Roleid).Single();

            return View();
            //return RedirectToAction("Index", "Home");
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




        #region profile functions
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
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Fullname,Email,Image,UserImageFile,Extra,Roleid")] Useraccount useraccount, string chosenFileName)
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
                    useraccount.Password = _context.Useraccounts.Where(_ => _.Id == id).AsNoTracking<Useraccount>().SingleOrDefault().Password;
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




        //to edit the user password
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
        public async Task<IActionResult> EditPassword(decimal id, [Bind("Id")] Useraccount useraccount, string Newpassword, string conPassword,string re_Newpassword)
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
                ViewBag.Error = "the re_Newpassword doesn't match with new passowrd";
                return View(useraccount);
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    int useridse = (int)HttpContext.Session.GetInt32("UserId");
                    string pass = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Password).Single();
                    if (pass == conPassword)
                    {
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
                        return RedirectToAction("Profile", "User", new { id = useraccount.Id });
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

        //
        #endregion



        #region subscribe function

        public IActionResult ViewAllSubscrebtion()
        {
            setviewbags();
            int useridse = (int)HttpContext.Session.GetInt32("UserId");

            var subsinfo = _context.Subcrebtions.Include(x => x.Useraccount).Include(x => x.Subcrebtiontype).Where(x => x.Useraccountid == useridse).ToList();
            
            if (subsinfo != null)
            {
                return View(subsinfo);
            }
            else
            {
                return RedirectToAction("NoSubscribtion", "User");

            }
        }



        public IActionResult ViewSubscrebtion(decimal? id)
        {

            setviewbags();
            int useridse = (int)HttpContext.Session.GetInt32("UserId");
            
            var subinfo = _context.Subcrebtions.Include(x=>x.Useraccount).Include(x=>x.Subcrebtiontype).Where(x => x.Id == id).FirstOrDefault();
            ViewBag.subid = subinfo.Id;
            if (subinfo != null)
            {
                return View(subinfo);
            }
            else
            {
                return RedirectToAction("NoSubscribtion", "User");

            }
        }


        public IActionResult NoSubscribtion()
        {

            ViewBag.UserID = HttpContext.Session.GetInt32("UserId");
            int useridse = (int)HttpContext.Session.GetInt32("UserId");
            ViewBag.UserName = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Fullname).Single();
            ViewBag.UserEmail = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Email).Single();
            ViewBag.UserImage = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Image).Single();
            ViewBag.RoleId = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Roleid).Single();

            return View();
            
        }







        // subsucribe method
        public IActionResult SubscribePage( decimal? subid)
        {
            ViewBag.Erorr= HttpContext.Session.GetString("suberror");
            subid =(int) HttpContext.Session.GetInt32("subnowId");
            ViewBag.UserID = HttpContext.Session.GetInt32("UserId");
            var userid = HttpContext.Session.GetInt32("UserId");
            var subinfo = _context.Subcrebtiontypes.Where(x => x.Id == subid).FirstOrDefault();
            var useroldsubs = _context.Subcrebtions.Where(x => x.Useraccountid == userid & x.Subcrebtiontypeid == subid).FirstOrDefault();
            if(useroldsubs != null)
            {
                return RedirectToAction("ViewAllSubscrebtion", "User");

            }

            if (ViewBag.UserID != null & subinfo != null )
            {
                int useridse = (int)HttpContext.Session.GetInt32("UserId");

                //
                //ViewBag.UserID = HttpContext.Session.GetInt32("UserId");
                setviewbags();

                //

                //viewbags to set the right values to the new subscribtion
                ViewBag.userid =  useridse;
                ViewBag.subtypeId = subid;
                ViewBag.subprice = subinfo.Price;

                //generate the invoice
                string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                Random random = new Random();
                string invoiceNumber = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());

                ViewBag.invoice=invoiceNumber;
                HttpContext.Session.SetString("invoice", invoiceNumber);

                //send two tables to view
                var useraccounts = _context.Useraccounts.Include(x => x.Role).ToList();
               
                var subcrebtiontypes = _context.Subcrebtiontypes.ToList();

                var model = Tuple.Create<IEnumerable<Useraccount>, IEnumerable<Subcrebtiontype>>(useraccounts, subcrebtiontypes);

                    




                return View();

            }
            else
            {
                //should show a toaster here to go and login to sub

                //return NotFound();
                
                return RedirectToAction("Login", "Authentication");
            }
            
        }


        [HttpPost]
        public async Task<IActionResult> SubscribePage([Bind("Id,Subcrebtiondate,State,Extra,Subcrebtiontypeid,Useraccountid")] Subcrebtion subcrebtion,decimal? cvv ,decimal? cardnumber)
        {
            var card =_context.Virtualbanks.Where(x=>x.Cvv==cvv & x.Cardnumber==cardnumber).FirstOrDefault();
            var subtype = _context.Subcrebtiontypes.Where(x => x.Id == subcrebtion.Subcrebtiontypeid).FirstOrDefault();

            if (card != null) { 
                if(subtype != null) { 
                    if(card.Balance>= subtype.Price ) {
                        if(card.Cvv == cvv) {
                            if (card.Cardnumber == cardnumber)
                            {

                                


                                subcrebtion.Subcrebtiontypeid = subtype.Id;
                                subcrebtion.Useraccountid = (int)HttpContext.Session.GetInt32("UserId");
                                subcrebtion.State = "Subscribed";
                                subcrebtion.Subcrebtiondate = DateTime.Now.Date;
                                _context.Add(subcrebtion);
                                await _context.SaveChangesAsync();

                                // update the bank


                                var virtualbank =_context.Virtualbanks.Where(x=>x.Cardnumber==cardnumber &x.Cvv==cvv).FirstOrDefault();
                                virtualbank.Balance = virtualbank.Balance - subtype.Price;

                                _context.Update(virtualbank);
                                await _context.SaveChangesAsync();

                                //send the email

                                var invoice = HttpContext.Session.GetString("invoice");
                                int useridse = (int)HttpContext.Session.GetInt32("UserId");
                                string username = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Fullname).Single();
                                string UserEmail = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Email).Single();

                                string subject = "Thank You for Subscribing!";
                                string messaage = $"Dear {username},\r\n\r\nWe are thrilled to inform you that your subscription has been successfully processed. Thank you for choosing to be part of our community. Your trust and support mean a lot to us.\r\n\r\nAs a token of our appreciation, we have attached your subscription invoice in PDF format to this email. You can find all the details of your subscription, including payment information, within the document.\r\n\r\n\r\nIf you have any questions or require further assistance, please don't hesitate to reach out to our dedicated support team. We are here to help you with any inquiries or concerns you may have.\r\n\r\nOnce again, we want to express our gratitude for your subscription. We are looking forward to providing you with exceptional service and value throughout your subscription journey.\r\n\r\nThank you for being a part of our community.\r\n\r\nBest regards,\r\n\r\nMohammad khabour\r\nCEO\r\nFunder";

                                string price = Convert.ToString( subtype.Price);
                                //( email,  subject,  message,  invoice,  username,  price,  subtype)
                                emailSender.SendEmailAsync("mohammedkabour@gmail.com", subject, messaage, invoice, username, price, subtype.Name,true);

                                HttpContext.Session.SetString("suberror", "");

                                return RedirectToAction("ViewAllSubscrebtion", "User");




                            }
                            else
                            {
                                setviewbags();
                                ViewBag.Erorr = "cardnumber is wrong";
                                HttpContext.Session.SetString("suberror", "cardnumber is wrong");
                                return RedirectToAction("SubscribePage", "User", new { subid = subcrebtion.Id });
                            }
                        }
                        else
                        {
                            setviewbags();
                            ViewBag.Erorr = "cvv is wrong";
                            HttpContext.Session.SetString("suberror", "cvv is wrong");
                            return RedirectToAction("SubscribePage", "User", new { subid = subcrebtion.Id });
                        }
                    }
                    else
                    {
                        setviewbags();
                        ViewBag.Erorr = "you don't have enouph money";
                        HttpContext.Session.SetString("suberror", "you don't have enouph money");
                        return RedirectToAction("SubscribePage", "User", new { subid = subcrebtion.Id });

                    }
                }
                else
                {
                    setviewbags();
                    ViewBag.Erorr = "Data base error";
                    HttpContext.Session.SetString("suberror", "Data base error");
                    return RedirectToAction("SubscribePage", "User", new { subid = subcrebtion.Id });
                }
            }
            else
            {
                setviewbags();
                ViewBag.Erorr = "cvv or card number is wrong";
                HttpContext.Session.SetString("suberror", "cvv or card number is wrong");
                return RedirectToAction("SubscribePage", "User", new {subid= subcrebtion.Id});
                return View(subcrebtion);
            }
            
        }


        //


        //delete 
        // GET: Subcrebtions/Delete/5
        public async Task<IActionResult> DeleteSubscrebtion(decimal? id)
        {
            setviewbags();
            if (id == null || _context.Subcrebtions == null)
            {
                return NotFound();
            }

            var subcrebtion = await _context.Subcrebtions
                .Include(s => s.Subcrebtiontype)
                .Include(s => s.Useraccount)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subcrebtion == null)
            {
                return NotFound();
            }

            return View(subcrebtion);
        }

        // POST: Subcrebtions/Delete/5
        [HttpPost, ActionName("DeleteSubscrebtion")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Subcrebtions == null)
            {
                return Problem("Entity set 'ModelContext.Subcrebtions'  is null.");
            }
            var subcrebtion = await _context.Subcrebtions.FindAsync(id);
            if (subcrebtion != null)
            {
                _context.Subcrebtions.Remove(subcrebtion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ViewAllSubscrebtion", "User");
        }

        #endregion




        #region testimonial function


        public IActionResult Testimonial()
        {

            int useridse = (int)HttpContext.Session.GetInt32("UserId");
            ViewBag.UserName = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Fullname).Single();
            ViewBag.UserEmail = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Email).Single();
            ViewBag.UserImage = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Image).Single();
            ViewBag.RoleId = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Roleid).Single();
            var usertest = _context.Testimonials.Where(x => x.Useraccountid == useridse).FirstOrDefault();
            if (usertest != null)
            {
              return RedirectToAction("ViewTestimonial", "User", new {id=usertest.Id});

            }
            else
            {

                return View();
            }



        }

        // POST: Testimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Testimonial([Bind("Id,State,Message,Testimonialdate,Extra,Useraccountid")] Testimonial testimonial)
        {
            
            var userid= (int)HttpContext.Session.GetInt32("UserId");
            testimonial.Testimonialdate= DateTime.Now;
            testimonial.State = "On hold";
            testimonial.Useraccountid = userid;
            _context.Add(testimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction("ViewTestimonial", "User", new {id= testimonial.Id });
            
            
        }




        // GET: Testimonials/Details/5
        public async Task<IActionResult> ViewTestimonial(decimal? id)
        {
            setviewbags();
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .Include(t => t.Useraccount)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }


        #endregion


        #region ADD BENEFICIARIES

        public IActionResult ShowBeneficiaries(decimal? subid)
        {
            //HttpContext.Session.SetString("layout2", "userlayout");
            ViewBag.UserID = HttpContext.Session.GetInt32("UserId");
            int useridse = (int)HttpContext.Session.GetInt32("UserId");
            ViewBag.UserName = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Fullname).Single();
            ViewBag.UserEmail = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Email).Single();
            ViewBag.UserImage = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Image).Single();
            ViewBag.RoleId = _context.Useraccounts.Where(x => x.Id == useridse).Select(x => x.Roleid).Single();
            ViewBag.subid = subid;

            var benefilist =_context.Beneficiaries.Include(x=>x.Subcrebtion).Include(x => x.Subcrebtion.Useraccount).Include(x => x.Subcrebtion.Subcrebtiontype).Where(x=>x.Subcrebtionid==subid).ToList();
            return View(benefilist);
            //return RedirectToAction("Index", "Home");
        }

        // GET: Beneficiaries/Details/5
        public async Task<IActionResult> ViewBeneficiaryRequest(decimal? id)
        {
            setviewbags();
            if (id == null || _context.Beneficiaries == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries
                .Include(b => b.Subcrebtion).Include(b => b.Subcrebtion.Useraccount)
                .Include(b => b.Subcrebtion.Subcrebtiontype)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            return View(beneficiary);
        }


        // POST: Beneficiaries/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewBeneficiaryRequest(decimal id)
        {
            if (_context.Beneficiaries == null)
            {
                return Problem("Entity set 'ModelContext.Beneficiaries'  is null.");
            }
            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            if (beneficiary != null)
            {
                _context.Beneficiaries.Remove(beneficiary);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        #endregion



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
