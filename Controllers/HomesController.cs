using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INSURANCE_FIRST_PROJECT.Models;

namespace INSURANCE_FIRST_PROJECT.Controllers
{
    public class HomesController : Controller
    {
        private readonly ModelContext _context;
        //for the site image
        private readonly IWebHostEnvironment webHostEnvironment;
        //

        public HomesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
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

        // GET: Homes
        public async Task<IActionResult> Index()
        {
            setviewbags();
            return _context.Homes != null ? 
                          View(await _context.Homes.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Homes'  is null.");
        }



        // GET: Homes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            setviewbags();
            if (id == null || _context.Homes == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // GET: Homes/Create
        public IActionResult Create()
        {
            setviewbags();
            return View();
        }

        // POST: Homes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text1,Image1,siteImage1,Text2,Text3,Text4,Text5,Text6,Text7,Image2,siteImage2,Image3,siteImage3,Image4,siteImage4,Image5,siteImage5,Image6,siteImage6")] Home home)
        {
            if (ModelState.IsValid)
            {
                // add image to the app
                if (home.siteImage1 != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + home.siteImage1.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await home.siteImage1.CopyToAsync(fileStream);
                    }

                    home.Image1 = fileName;
                }
                //
                // add image to the app
                if (home.siteImage2 != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + home.siteImage2.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await home.siteImage2.CopyToAsync(fileStream);
                    }

                    home.Image2 = fileName;
                }
                //
                // add image to the app
                if (home.siteImage3 != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + home.siteImage3.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await home.siteImage3.CopyToAsync(fileStream);
                    }

                    home.Image3 = fileName;
                }
                //
                // add image to the app
                if (home.siteImage4 != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + home.siteImage4.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await home.siteImage4.CopyToAsync(fileStream);
                    }

                    home.Image4 = fileName;
                }
                //
                // add image to the app
                if (home.siteImage5 != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + home.siteImage5.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await home.siteImage5.CopyToAsync(fileStream);
                    }

                    home.Image5 = fileName;
                }
                //
                // add image to the app
                if (home.siteImage6 != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + home.siteImage6.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await home.siteImage6.CopyToAsync(fileStream);
                    }

                    home.Image6 = fileName;
                }
                //








                _context.Add(home);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(home);
        }

        // GET: Homes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            setviewbags();
            if (id == null || _context.Homes == null)
            {
                return NotFound();
            }

            var home = await _context.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }
            return View(home);
        }

        // POST: Homes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Text1,Image1,siteImage1,Text2,Text3,Text4,Text5,Text6,Text7,Image2,siteImage2,Image3,siteImage3,Image4,siteImage4,Image5,siteImage5,Image6.siteImage6")] Home home)
        {
            if (id != home.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // add image to the app
                    if (home.siteImage1 != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + home.siteImage1.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await home.siteImage1.CopyToAsync(fileStream);
                        }

                        home.Image1 = fileName;
                    }
                    //
                    // add image to the app
                    if (home.siteImage2 != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + home.siteImage2.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await home.siteImage2.CopyToAsync(fileStream);
                        }

                        home.Image2 = fileName;
                    }
                    //
                    // add image to the app
                    if (home.siteImage3 != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + home.siteImage3.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await home.siteImage3.CopyToAsync(fileStream);
                        }

                        home.Image3 = fileName;
                    }
                    //
                    // add image to the app
                    if (home.siteImage4 != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + home.siteImage4.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await home.siteImage4.CopyToAsync(fileStream);
                        }

                        home.Image4 = fileName;
                    }
                    //
                    // add image to the app
                    if (home.siteImage5 != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + home.siteImage5.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await home.siteImage5.CopyToAsync(fileStream);
                        }

                        home.Image5 = fileName;
                    }
                    //
                    // add image to the app
                    if (home.siteImage6 != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + home.siteImage6.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await home.siteImage6.CopyToAsync(fileStream);
                        }

                        home.Image6 = fileName;
                    }
                    //
                    _context.Update(home);
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeExists(home.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "Homes", new { id = home.Id });
            }
            //return View(home);
            return RedirectToAction("Details", "Homes", new { id = home.Id });
        }

        // GET: Homes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            setviewbags();
            if (id == null || _context.Homes == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // POST: Homes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Homes == null)
            {
                return Problem("Entity set 'ModelContext.Homes'  is null.");
            }
            var home = await _context.Homes.FindAsync(id);
            if (home != null)
            {
                _context.Homes.Remove(home);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeExists(decimal id)
        {
          return (_context.Homes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
