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
    public class HotelCitiesController : Controller
    {
        private readonly Travel_ApplicationContext _context;

        public HotelCitiesController(Travel_ApplicationContext context)
        {
            _context = context;
        }

        // GET: HotelCities
        public async Task<IActionResult> Index()
        {
            var travel_ApplicationContext = _context.HotelCity.Include(h => h.City).Include(h => h.Hotel);
            return View(await travel_ApplicationContext.ToListAsync());
        }

        // GET: HotelCities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HotelCity == null)
            {
                return NotFound();
            }

            var hotelCity = await _context.HotelCity
                .Include(h => h.City)
                .Include(h => h.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotelCity == null)
            {
                return NotFound();
            }

            return View(hotelCity);
        }

        // GET: HotelCities/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id");
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Id");
            return View();
        }

        // POST: HotelCities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CityId,HotelId")] HotelCity hotelCity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotelCity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id", hotelCity.CityId);
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Id", hotelCity.HotelId);
            return View(hotelCity);
        }

        // GET: HotelCities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HotelCity == null)
            {
                return NotFound();
            }

            var hotelCity = await _context.HotelCity.FindAsync(id);
            if (hotelCity == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id", hotelCity.CityId);
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Id", hotelCity.HotelId);
            return View(hotelCity);
        }

        // POST: HotelCities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CityId,HotelId")] HotelCity hotelCity)
        {
            if (id != hotelCity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelCity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelCityExists(hotelCity.Id))
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
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id", hotelCity.CityId);
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Id", hotelCity.HotelId);
            return View(hotelCity);
        }

        // GET: HotelCities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HotelCity == null)
            {
                return NotFound();
            }

            var hotelCity = await _context.HotelCity
                .Include(h => h.City)
                .Include(h => h.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotelCity == null)
            {
                return NotFound();
            }

            return View(hotelCity);
        }

        // POST: HotelCities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HotelCity == null)
            {
                return Problem("Entity set 'Travel_ApplicationContext.HotelCity'  is null.");
            }
            var hotelCity = await _context.HotelCity.FindAsync(id);
            if (hotelCity != null)
            {
                _context.HotelCity.Remove(hotelCity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelCityExists(int id)
        {
          return (_context.HotelCity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
