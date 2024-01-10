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
    public class BeneficiariesController : Controller
    {
        private readonly ModelContext _context;

        //for the site image
        private readonly IWebHostEnvironment webHostEnvironment;
        //

        public BeneficiariesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            //for the site image
            this.webHostEnvironment = webHostEnvironment;
            //

        }

        // GET: Beneficiaries
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Beneficiaries.Include(b => b.Subcrebtion).Include(m => m.Subcrebtion.Subcrebtiontype);
            return View(await modelContext.ToListAsync());
        }

        // GET: Beneficiaries/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Beneficiaries == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries
                .Include(b => b.Subcrebtion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            return View(beneficiary);
        }

        // GET: Beneficiaries/Create
        public IActionResult Create(decimal? subid)
        {
            var subinfo = _context.Subcrebtions.Include(b => b.Subcrebtiontype).Where(x=>x.Id ==subid).FirstOrDefault();
            ViewBag.subid=subid;
            
            
            ViewData["Subcrebtionid"] = new SelectList(_context.Subcrebtions, "Id", "Id");
            return View();
        }

        // POST: Beneficiaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Document,BeneficiaryDocument,State,Relationtype,Beneficiarystartdate,Extra,Subcrebtionid")] Beneficiary beneficiary)
        {
            var subinfo = _context.Subcrebtions.Where(x => x.Id == beneficiary.Subcrebtionid).FirstOrDefault();
            if(subinfo != null)
            {
                var subtypeinfo = _context.Subcrebtions.Where(x => x.Id == subinfo.Subcrebtiontypeid).FirstOrDefault();

                beneficiary.State = "On Hold";
                beneficiary.Beneficiarystartdate=DateTime.Now;


                // add image to the app
                if (beneficiary.BeneficiaryDocument != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + beneficiary.BeneficiaryDocument.FileName;

                    string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await beneficiary.BeneficiaryDocument.CopyToAsync(fileStream);
                    }

                    beneficiary.Document = fileName;
                }
            //
                _context.Add(beneficiary);
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewSubscrebtion", "User", new { id= beneficiary.Subcrebtionid });
            }

            ViewData["Subcrebtionid"] = new SelectList(_context.Subcrebtions, "Id", "Id", beneficiary.Subcrebtionid);
            return View(beneficiary);
        }

        // GET: Beneficiaries/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Beneficiaries == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            if (beneficiary == null)
            {
                return NotFound();
            }
            ViewData["Subcrebtionid"] = new SelectList(_context.Subcrebtions, "Id", "Id", beneficiary.Subcrebtionid);
            return View(beneficiary);
        }

        // POST: Beneficiaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Document,BeneficiaryDocument,State,Relationtype,Beneficiarystartdate,Extra,Subcrebtionid")] Beneficiary beneficiary)
        {
            if (id != beneficiary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // add image to the app
                    if (beneficiary.BeneficiaryDocument != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + beneficiary.BeneficiaryDocument.FileName;

                        string path = Path.Combine(wwwRootPath + "/imgs/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await beneficiary.BeneficiaryDocument.CopyToAsync(fileStream);
                        }

                        beneficiary.Document = fileName;
                    }
                    //


                    _context.Update(beneficiary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeneficiaryExists(beneficiary.Id))
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
            ViewData["Subcrebtionid"] = new SelectList(_context.Subcrebtions, "Id", "Id", beneficiary.Subcrebtionid);
            return View(beneficiary);
        }

        // GET: Beneficiaries/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Beneficiaries == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries
                .Include(b => b.Subcrebtion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            return View(beneficiary);
        }

        // POST: Beneficiaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
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

        private bool BeneficiaryExists(decimal id)
        {
          return (_context.Beneficiaries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
