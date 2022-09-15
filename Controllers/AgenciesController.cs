using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Travel_Application.Data;
using Travel_Application.Models;

namespace Travel_Application.Controllers
{
    public class AgenciesController : Controller
    {
        private readonly Travel_ApplicationContext _context;

        public AgenciesController(Travel_ApplicationContext context)
        {
            _context = context;
        }

        // GET: Agencies
        public async Task<IActionResult> Index()
        {
              return _context.Agency != null ? 
                          View(await _context.Agency.ToListAsync()) :
                          Problem("Entity set 'Travel_ApplicationContext.Agency'  is null.");
        }

        // GET: Agencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Agency == null)
            {
                return NotFound();
            }

            var agency = await _context.Agency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agency == null)
            {
                return NotFound();
            }

            return View(agency);
        }

        // GET: Agencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,PhoneNumber,AgencyUrl")] Agency agency)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(agency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agency);
        }

        // GET: Agencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Agency == null)
            {
                return NotFound();
            }

            var agency = await _context.Agency.FindAsync(id);
            if (agency == null)
            {
                return NotFound();
            }
            return View(agency);
        }

        // POST: Agencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,PhoneNumber,AgencyUrl")] Agency agency)
        {
            if (id != agency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgencyExists(agency.Id))
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
            return View(agency);
        }

        // GET: Agencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Agency == null)
            {
                return NotFound();
            }

            var agency = await _context.Agency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agency == null)
            {
                return NotFound();
            }

            return View(agency);
        }

        // POST: Agencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Agency == null)
            {
                return Problem("Entity set 'Travel_ApplicationContext.Agency'  is null.");
            }
            var agency = await _context.Agency.FindAsync(id);
            if (agency != null)
            {
                _context.Agency.Remove(agency);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgencyExists(int id)
        {
          return (_context.Agency?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
