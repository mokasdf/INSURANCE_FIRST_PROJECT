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
    public class AboutsController : Controller
    {
        private readonly ModelContext _context;
        //for the site image
        private readonly IWebHostEnvironment webHostEnvironment;
        //

        public AboutsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            //for the site image
            this.webHostEnvironment = webHostEnvironment;
            //
        }

        // GET: Abouts
        public async Task<IActionResult> Index()
        {
              return _context.Abouts != null ? 
                          View(await _context.Abouts.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Abouts'  is null.");
        }

        // GET: Abouts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Abouts == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // GET: Abouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Abouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image1,siteImage1,Text1,Text2,Text3,Text4,Text5,Text6,Image2,siteImage2,Image3,siteImage3,Image4,siteImage4,Image5,siteImage5,Image6,siteImage6")] About about)
        {
            if (ModelState.IsValid)
            {
                // add image to the app
                if (about.siteImage1 != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + about.siteImage1.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await about.siteImage1.CopyToAsync(fileStream);
                    }

                    about.Image1 = fileName;
                }
                //
                // add image to the app
                if (about.siteImage2 != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + about.siteImage2.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await about.siteImage2.CopyToAsync(fileStream);
                    }

                    about.Image2 = fileName;
                }
                //
                // add image to the app
                if (about.siteImage3 != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + about.siteImage3.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await about.siteImage3.CopyToAsync(fileStream);
                    }

                    about.Image3 = fileName;
                }
                //
                // add image to the app
                if (about.siteImage4 != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + about.siteImage4.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await about.siteImage4.CopyToAsync(fileStream);
                    }

                    about.Image4 = fileName;
                }
                //
                // add image to the app
                if (about.siteImage5 != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + about.siteImage5.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await about.siteImage5.CopyToAsync(fileStream);
                    }

                    about.Image5 = fileName;
                }
                //
                // add image to the app
                if (about.siteImage6 != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + about.siteImage6.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await about.siteImage6.CopyToAsync(fileStream);
                    }

                    about.Image6 = fileName;
                }
                //

                _context.Add(about);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(about);
        }

        // GET: Abouts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Abouts == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        // POST: Abouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Image1,siteImage1,Text1,Text2,Text3,Text4,Text5,Text6,Image2,siteImage2,Image3,siteImage3,Image4,siteImage4,Image5,siteImage5,Image6,siteImage6")] About about)
        {
            if (id != about.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // add image to the app
                    if (about.siteImage1 != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + about.siteImage1.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await about.siteImage1.CopyToAsync(fileStream);
                        }

                        about.Image1 = fileName;
                    }
                    //
                    // add image to the app
                    if (about.siteImage2 != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + about.siteImage2.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await about.siteImage2.CopyToAsync(fileStream);
                        }

                        about.Image2 = fileName;
                    }
                    //
                    // add image to the app
                    if (about.siteImage3 != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + about.siteImage3.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await about.siteImage3.CopyToAsync(fileStream);
                        }

                        about.Image3 = fileName;
                    }
                    //
                    // add image to the app
                    if (about.siteImage4 != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + about.siteImage4.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await about.siteImage4.CopyToAsync(fileStream);
                        }

                        about.Image4 = fileName;
                    }
                    //
                    // add image to the app
                    if (about.siteImage5 != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + about.siteImage5.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await about.siteImage5.CopyToAsync(fileStream);
                        }

                        about.Image5 = fileName;
                    }
                    //
                    // add image to the app
                    if (about.siteImage6 != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + about.siteImage6.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await about.siteImage6.CopyToAsync(fileStream);
                        }

                        about.Image6 = fileName;
                    }
                    //


                    _context.Update(about);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutExists(about.Id))
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
            return View(about);
        }

        // GET: Abouts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Abouts == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // POST: Abouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Abouts == null)
            {
                return Problem("Entity set 'ModelContext.Abouts'  is null.");
            }
            var about = await _context.Abouts.FindAsync(id);
            if (about != null)
            {
                _context.Abouts.Remove(about);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutExists(decimal id)
        {
          return (_context.Abouts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
