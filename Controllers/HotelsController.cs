using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Travel_Application.Data;
using Travel_Application.Models;
using Travel_Application.ViewModels;

namespace Travel_Application.Controllers
{
    public class HotelsController : Controller
    {
        private readonly Travel_ApplicationContext _context;

        public HotelsController(Travel_ApplicationContext context)
        {
            _context = context;
        }

        // GET: Hotels
        public async Task<IActionResult> Index(string Name, int star)
        {
            IQueryable < Hotel> hotelsQuery = _context.Hotel.AsQueryable();
            IQueryable<int?> starsQuery = _context.Hotel.OrderBy(m => m.Stars).Select(m => m.Stars).Distinct();
            if (!string.IsNullOrEmpty(Name))
            {
                hotelsQuery = hotelsQuery.Where(x => x.Name.Contains(Name));
            }
            if (star != null && star != 0)
            {
                hotelsQuery = hotelsQuery.Where(s => s.Stars == star);
            }
            var HotelFilter = new FilterHotel
            {
                hotels = await hotelsQuery.Include(c => c.Agency).ToListAsync(),
                stars = new SelectList(await starsQuery.ToListAsync())
            };
            return View(HotelFilter);
        }

        // GET: Hotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hotel == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotel
                .Include(h => h.Agency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // GET: Hotels/Create
        public IActionResult Create()
        {
            ViewData["Agencies"] = new SelectList(_context.Set<Agency>(), "Id", "Name");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Stars,PetFriendly,Spa,AgencyId")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Agencies"] = new SelectList(_context.Set<Agency>() , "AgencyId", "Name", hotel.AgencyId);
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hotel == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            ViewData["Agencies"] = new SelectList(_context.Set<Agency>(), "Id", "Name");
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Stars,PetFriendly,Spa,AgencyId")] Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.Id))
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
            ViewData["Agencies"] = new SelectList(_context.Set<Agency>(), "AgencyId", "Name", hotel.AgencyId);
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hotel == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotel
                .Include(h => h.Agency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hotel == null)
            {
                return Problem("Entity set 'Travel_ApplicationContext.Hotel'  is null.");
            }
            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotel.Remove(hotel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelExists(int id)
        {
          return (_context.Hotel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> Hotels_Agency(int? id, string Name, int star)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agency = await _context.Agency
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.Message = agency.Name;
            IQueryable<Hotel> hotelsQuery = _context.Hotel.Where(m => m.AgencyId == id);
            IQueryable<int?> starsQuery = _context.Hotel.OrderBy(m => m.Stars).Select(m => m.Stars).Distinct();
            await _context.SaveChangesAsync();
            if (agency == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(Name))
            {
                hotelsQuery = hotelsQuery.Where(x => x.Name.Contains(Name));
            }
            if (star != null && star != 0)
            {
                hotelsQuery = hotelsQuery.Where(s => s.Stars == star);
            }
            var HotelFilter = new FilterHotel
            {
                hotels = await hotelsQuery.Include(c => c.Agency).ToListAsync(),
                stars = new SelectList(await starsQuery.ToListAsync())
            };
            return View(HotelFilter);
        }
        public async Task<IActionResult> Hotels_City(int? id, string Name, int star)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.City
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.Message = city.Name;
            IQueryable<Hotel> hotelsQuery = _context.HotelCity.Where(m => m.CityId == id).Select(x => x.Hotel);
            IQueryable<int?> starsQuery = _context.Hotel.OrderBy(m => m.Stars).Select(m => m.Stars).Distinct();
            await _context.SaveChangesAsync();
            if (city == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(Name))
            {
                hotelsQuery = hotelsQuery.Where(x => x.Name.Contains(Name));
            }
            if (star != null && star != 0)
            {
                hotelsQuery = hotelsQuery.Where(s => s.Stars == star);
            }
            var HotelFilter = new FilterHotel
            {
                hotels = await hotelsQuery.ToListAsync(),
                stars = new SelectList(await starsQuery.ToListAsync())
            };
            return View(HotelFilter);
        }
    }
}
