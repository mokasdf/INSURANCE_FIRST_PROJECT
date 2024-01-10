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
    public class SubcrebtionsController : Controller
    {
        private readonly ModelContext _context;

        public SubcrebtionsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Subcrebtions
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Subcrebtions.Include(s => s.Subcrebtiontype).Include(s => s.Useraccount);
            return View(await modelContext.ToListAsync());
        }

        // GET: Subcrebtions/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
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

        // GET: Subcrebtions/Create
        public IActionResult Create()
        {
            ViewData["Subcrebtiontypeid"] = new SelectList(_context.Subcrebtiontypes, "Id", "Id");
            ViewData["Useraccountid"] = new SelectList(_context.Useraccounts, "Id", "Id");
            return View();
        }

        // POST: Subcrebtions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subcrebtiondate,State,Extra,Subcrebtiontypeid,Useraccountid")] Subcrebtion subcrebtion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subcrebtion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Subcrebtiontypeid"] = new SelectList(_context.Subcrebtiontypes, "Id", "Id", subcrebtion.Subcrebtiontypeid);
            ViewData["Useraccountid"] = new SelectList(_context.Useraccounts, "Id", "Id", subcrebtion.Useraccountid);
            return View(subcrebtion);
        }

        // GET: Subcrebtions/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Subcrebtions == null)
            {
                return NotFound();
            }

            var subcrebtion = await _context.Subcrebtions.FindAsync(id);
            if (subcrebtion == null)
            {
                return NotFound();
            }
            ViewData["Subcrebtiontypeid"] = new SelectList(_context.Subcrebtiontypes, "Id", "Id", subcrebtion.Subcrebtiontypeid);
            ViewData["Useraccountid"] = new SelectList(_context.Useraccounts, "Id", "Id", subcrebtion.Useraccountid);
            return View(subcrebtion);
        }

        // POST: Subcrebtions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Subcrebtiondate,State,Extra,Subcrebtiontypeid,Useraccountid")] Subcrebtion subcrebtion)
        {
            if (id != subcrebtion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subcrebtion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubcrebtionExists(subcrebtion.Id))
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
            ViewData["Subcrebtiontypeid"] = new SelectList(_context.Subcrebtiontypes, "Id", "Id", subcrebtion.Subcrebtiontypeid);
            ViewData["Useraccountid"] = new SelectList(_context.Useraccounts, "Id", "Id", subcrebtion.Useraccountid);
            return View(subcrebtion);
        }

        // GET: Subcrebtions/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
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
        [HttpPost, ActionName("Delete")]
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
            return RedirectToAction(nameof(Index));
        }

        private bool SubcrebtionExists(decimal id)
        {
          return (_context.Subcrebtions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
