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
    public class CitiesController : Controller
    {
        private readonly Travel_ApplicationContext _context;

        public CitiesController(Travel_ApplicationContext context)
        {
            _context = context;
        }

        // GET: Cities
        public async Task<IActionResult> Index(string Name, string Country)
        {
            IQueryable<City> citiesQuery = _context.City.AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                citiesQuery = citiesQuery.Where(x => x.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Country))
            {
                citiesQuery = citiesQuery.Where(s => s.Country.Contains(Country));
            }
            var CityFilter = new FilterCity
            {
                cities = await citiesQuery.ToListAsync(),
            };
            return View(CityFilter);
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.City == null)
            {
                return NotFound();
            }

            var city = await _context.City
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            CityPictures viewmodel = new CityPictures
            {
                City = city,
                CityPictureName = city.Picture
            };

            return View(viewmodel);

           
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            ViewData["Hotels"] = new SelectList(_context.Set<Hotel>(), "Id", "Name");
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country,PopularLandMark,Hotels")] City city)
        {
            if (ModelState.IsValid)
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Hotels"] = new SelectList(_context.Set<Hotel>(), "Id", "Name");
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.City == null)
            {
                return NotFound();
            }

            var city = _context.City.Where(m => m.Id == id).Include(x => x.Hotels).First();
            IQueryable<City> citiesQuery = _context.City.AsQueryable();
            citiesQuery = citiesQuery.Where(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            var hotels = _context.Hotel.AsEnumerable();
            hotels = hotels.OrderBy(s => s.Name);
            EnrollHotels viewmodel = new EnrollHotels
            {
                city = await citiesQuery.FirstAsync(),
                hotelsEnrolledList = new MultiSelectList(hotels, "Id", "Name"),
                selectedHotels = city.Hotels.Select(sa => sa.CityId)
            };
            return View(viewmodel);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EnrollHotels viewmodel)
        {
            if (id != viewmodel.city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.city);
                    await _context.SaveChangesAsync();

                    var city = _context.City.Where(c => c.Id == id).First();
                    IEnumerable<int> selectedHotels = viewmodel.selectedHotels;
                    if (selectedHotels != null)
                    {
                        IQueryable<HotelCity> toBeRemoved = _context.HotelCity.Where(s => !selectedHotels.Contains(s.HotelId) && s.CityId == id);
                        _context.HotelCity.RemoveRange(toBeRemoved);

                        IEnumerable<int> existEnrollments = _context.HotelCity.Where(s => selectedHotels.Contains(s.HotelId) && s.CityId == id).Select(s => s.Id);
                        IEnumerable<int> newEnrollments = selectedHotels.Where(s => !existEnrollments.Contains(s));

                        foreach (int studentId in newEnrollments)
                            _context.HotelCity.Add(new HotelCity { HotelId = studentId, CityId = id });

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        IQueryable<HotelCity> toBeRemoved = _context.HotelCity.Where(s => s.CityId == id);
                        _context.HotelCity.RemoveRange(toBeRemoved);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(viewmodel.city.Id))
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
            return View(viewmodel);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.City == null)
            {
                return NotFound();
            }

            var city = await _context.City
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.City == null)
            {
                return Problem("Entity set 'Travel_ApplicationContext.City'  is null.");
            }
            var city = await _context.City.FindAsync(id);
            if (city != null)
            {
                _context.City.Remove(city);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(int id)
        {
          return (_context.City?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> EditPicture(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = _context.City.Where(s => s.Id == id).First();
            if (city == null)
            {
                return NotFound();
            }

            CityPictures viewmodel = new CityPictures
            {
                City = city,
                CityPictureName = city.Picture
            };

            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPicture(int id, CityPictures viewmodel)
        {
            viewmodel.City = _context.City.Where(s => s.Id == id).FirstOrDefault();
            if (id != viewmodel.City.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (viewmodel.CityPictureFile != null)
                    {
                        string uniqueFileName = UploadedFile(viewmodel);
                        viewmodel.City.Picture = uniqueFileName;
                    }
                    else
                    {
                        viewmodel.City.Picture = viewmodel.CityPictureName;
                    }

                    _context.Update(viewmodel.City);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(viewmodel.City.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = viewmodel.City.Id });
            }
            return View(viewmodel);
        }
        private string UploadedFile(CityPictures viewmodel)
        {
            string uniqueFileName = null;

            if (viewmodel.CityPictureFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(viewmodel.CityPictureFile.FileName);
                string fileNameWithPath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    viewmodel.CityPictureFile.CopyTo(stream);
                }
            }
            return uniqueFileName;
        }

    }
}
