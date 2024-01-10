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
    public class SubcrebtiontypesController : Controller
    {
        private readonly ModelContext _context;
        //for the site image
        private readonly IWebHostEnvironment webHostEnvironment;
        //

        public SubcrebtiontypesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
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


        // GET: Subcrebtiontypes
        public async Task<IActionResult> Index()
        {
            setviewbags();
              return _context.Subcrebtiontypes != null ? 
                          View(await _context.Subcrebtiontypes.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Subcrebtiontypes'  is null.");
        }

        // GET: Subcrebtiontypes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            setviewbags();
            if (id == null || _context.Subcrebtiontypes == null)
            {
                return NotFound();
            }

            var subcrebtiontype = await _context.Subcrebtiontypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subcrebtiontype == null)
            {
                return NotFound();
            }

            return View(subcrebtiontype);
        }

        // GET: Subcrebtiontypes/Create
        public IActionResult Create()
        {
            setviewbags();
            return View();
        }

        // POST: Subcrebtiontypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Maxnumberofbeneficiaries,Text1,Image,subImage,Text2")] Subcrebtiontype subcrebtiontype)
        {
            if (ModelState.IsValid)
            {

                // add image to the app
                if (subcrebtiontype.subImage != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + subcrebtiontype.subImage.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await subcrebtiontype.subImage.CopyToAsync(fileStream);
                    }

                    subcrebtiontype.Image = fileName;
                }
                //
                _context.Add(subcrebtiontype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subcrebtiontype);
        }

        // GET: Subcrebtiontypes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            setviewbags();
            if (id == null || _context.Subcrebtiontypes == null)
            {
                return NotFound();
            }

            var subcrebtiontype = await _context.Subcrebtiontypes.FindAsync(id);
            if (subcrebtiontype == null)
            {
                return NotFound();
            }
            return View(subcrebtiontype);
        }

        // POST: Subcrebtiontypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Price,Maxnumberofbeneficiaries,Text1,Image,subImage,Text2")] Subcrebtiontype subcrebtiontype)
        {
            if (id != subcrebtiontype.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // add image to the app
                    if (subcrebtiontype.subImage != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + subcrebtiontype.subImage.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await subcrebtiontype.subImage.CopyToAsync(fileStream);
                        }

                        subcrebtiontype.Image = fileName;
                    }
                    //




                    _context.Update(subcrebtiontype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubcrebtiontypeExists(subcrebtiontype.Id))
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
            return View(subcrebtiontype);
        }

        // GET: Subcrebtiontypes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            setviewbags();
            if (id == null || _context.Subcrebtiontypes == null)
            {
                return NotFound();
            }

            var subcrebtiontype = await _context.Subcrebtiontypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subcrebtiontype == null)
            {
                return NotFound();
            }

            return View(subcrebtiontype);
        }

        // POST: Subcrebtiontypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Subcrebtiontypes == null)
            {
                return Problem("Entity set 'ModelContext.Subcrebtiontypes'  is null.");
            }
            var subcrebtiontype = await _context.Subcrebtiontypes.FindAsync(id);
            if (subcrebtiontype != null)
            {
                _context.Subcrebtiontypes.Remove(subcrebtiontype);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubcrebtiontypeExists(decimal id)
        {
          return (_context.Subcrebtiontypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
