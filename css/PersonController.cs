using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NTD_Baithi2324.Models;

namespace NTD_Baithi2324.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
              return _context.NTD195Person != null ? 
                          View(await _context.NTD195Person.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.NTD195Person'  is null.");
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.NTD195Person == null)
            {
                return NotFound();
            }

            var nTD195Person = await _context.NTD195Person
                .FirstOrDefaultAsync(m => m.NTD195PersonID == id);
            if (nTD195Person == null)
            {
                return NotFound();
            }

            return View(nTD195Person);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NTD195PersonID,NTD195FullName,NTD195Diachi")] NTD195Person nTD195Person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nTD195Person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nTD195Person);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.NTD195Person == null)
            {
                return NotFound();
            }

            var nTD195Person = await _context.NTD195Person.FindAsync(id);
            if (nTD195Person == null)
            {
                return NotFound();
            }
            return View(nTD195Person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NTD195PersonID,NTD195FullName,NTD195Diachi")] NTD195Person nTD195Person)
        {
            if (id != nTD195Person.NTD195PersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nTD195Person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NTD195PersonExists(nTD195Person.NTD195PersonID))
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
            return View(nTD195Person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.NTD195Person == null)
            {
                return NotFound();
            }

            var nTD195Person = await _context.NTD195Person
                .FirstOrDefaultAsync(m => m.NTD195PersonID == id);
            if (nTD195Person == null)
            {
                return NotFound();
            }

            return View(nTD195Person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.NTD195Person == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NTD195Person'  is null.");
            }
            var nTD195Person = await _context.NTD195Person.FindAsync(id);
            if (nTD195Person != null)
            {
                _context.NTD195Person.Remove(nTD195Person);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NTD195PersonExists(string id)
        {
          return (_context.NTD195Person?.Any(e => e.NTD195PersonID == id)).GetValueOrDefault();
        }
    }
}
