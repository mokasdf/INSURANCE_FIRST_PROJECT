using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INSURANCE_FIRST_PROJECT.Models;

namespace INSURANCE_FIRST_PROJECT.Controllers
{
    public class ContactusController : Controller
    {
        private readonly ModelContext _context;
        //for the site image
        private readonly IWebHostEnvironment webHostEnvironment;
        //

        public ContactusController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            //for the site image
            this.webHostEnvironment = webHostEnvironment;
            //
        }

        public void setviewbags()
        {
            //site statics data
            ViewBag.Profit = _context.Subcrebtions.Sum(x => x.Subcrebtiontype.Price);
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

        // GET: Contactus
        public async Task<IActionResult> Index()
        {
            setviewbags();
              return _context.Contactus != null ? 
                          View(await _context.Contactus.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Contactus'  is null.");
        }


       


        // GET: Contactus/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            setviewbags();
            if (id == null || _context.Contactus == null)
            {
                return NotFound();
            }

            var contactu = await _context.Contactus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactu == null)
            {
                return NotFound();
            }

            return View(contactu);
        }

        // GET: Contactus/Create
        public IActionResult Create()
        {
            setviewbags();
            return View();
        }

        // POST: Contactus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Phonenumber,Fullname,Text1,Text2,Image1,siteImage1,Image2,siteImage2,Image3,siteImage3,Text3,Text4")] Contactu contactu)
        {
            if (ModelState.IsValid)
            {
                // add image to the app
                if (contactu.siteImage1 != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + contactu.siteImage1.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await contactu.siteImage1.CopyToAsync(fileStream);
                    }

                    contactu.Image1 = fileName;
                }
                //
                // add image to the app
                if (contactu.siteImage2 != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + contactu.siteImage2.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await contactu.siteImage2.CopyToAsync(fileStream);
                    }

                    contactu.Image2 = fileName;
                }
                //
                // add image to the app
                if (contactu.siteImage3 != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + contactu.siteImage3.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await contactu.siteImage3.CopyToAsync(fileStream);
                    }

                    contactu.Image3 = fileName;
                }
                //


                _context.Add(contactu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactu);
        }

        // GET: Contactus/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            setviewbags();
            if (id == null || _context.Contactus == null)
            {
                return NotFound();
            }

            var contactu = await _context.Contactus.FindAsync(id);
            if (contactu == null)
            {
                return NotFound();
            }
            return View(contactu);
        }

        // POST: Contactus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Email,Phonenumber,Fullname,Text1,Text2,Image1,siteImage1,Image2,siteImage2,Image3,siteImage3,Text3,Text4")] Contactu contactu)
        {
            if (id != contactu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    // add image to the app
                    if (contactu.siteImage1 != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + contactu.siteImage1.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await contactu.siteImage1.CopyToAsync(fileStream);
                        }

                        contactu.Image1 = fileName;
                    }
                    //
                    // add image to the app
                    if (contactu.siteImage2 != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + contactu.siteImage2.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await contactu.siteImage2.CopyToAsync(fileStream);
                        }

                        contactu.Image2 = fileName;
                    }
                    //
                    // add image to the app
                    if (contactu.siteImage3 != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + contactu.siteImage3.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await contactu.siteImage3.CopyToAsync(fileStream);
                        }

                        contactu.Image3 = fileName;
                    }
                    //


                    _context.Update(contactu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactuExists(contactu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contactu);
        }

        // GET: Contactus/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            setviewbags();
            if (id == null || _context.Contactus == null)
            {
                return NotFound();
            }

            var contactu = await _context.Contactus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactu == null)
            {
                return NotFound();
            }

            return View(contactu);
        }

        // POST: Contactus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Contactus == null)
            {
                return Problem("Entity set 'ModelContext.Contactus'  is null.");
            }
            var contactu = await _context.Contactus.FindAsync(id);
            if (contactu != null)
            {
                _context.Contactus.Remove(contactu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactuExists(decimal id)
        {
          return (_context.Contactus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
