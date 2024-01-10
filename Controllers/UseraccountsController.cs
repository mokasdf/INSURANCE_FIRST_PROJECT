using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INSURANCE_FIRST_PROJECT.Models;
using Microsoft.AspNetCore.Hosting;

namespace INSURANCE_FIRST_PROJECT.Controllers
{
    public class UseraccountsController : Controller
    {
        private readonly ModelContext _context;

        //for the site image
        private readonly IWebHostEnvironment webHostEnvironment;
        //
        public UseraccountsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            //
            this.webHostEnvironment = webHostEnvironment;
            //
        }

        // GET: Useraccounts
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Useraccounts.Include(u => u.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: Useraccounts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
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

        // GET: Useraccounts/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "Id", "Id");
            return View();
        }

        // POST: Useraccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fullname,Email,Password,Image,UserImageFile,Extra,Roleid")] Useraccount useraccount)
        {
            if (ModelState.IsValid)
            {
                // add image to the app
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

                _context.Add(useraccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Id", "Id", useraccount.Roleid);
            return View(useraccount);
        }

        // GET: Useraccounts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
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
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Fullname,Email,Password,Image,UserImageFile,Extra,Roleid")] Useraccount useraccount)
        {
            if (id != useraccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                    _context.Update(useraccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UseraccountExists(useraccount.Id))
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
            ViewData["Roleid"] = new SelectList(_context.Roles, "Id", "Id", useraccount.Roleid);
            return View(useraccount);
        }

        // GET: Useraccounts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
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

        // POST: Useraccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Useraccounts == null)
            {
                return Problem("Entity set 'ModelContext.Useraccounts'  is null.");
            }
            var useraccount = await _context.Useraccounts.FindAsync(id);
            if (useraccount != null)
            {
                _context.Useraccounts.Remove(useraccount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UseraccountExists(decimal id)
        {
          return (_context.Useraccounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
