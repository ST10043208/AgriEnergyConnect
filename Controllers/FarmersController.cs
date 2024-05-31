using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;

namespace AgriEnergyConnect.Controllers
{
    public class FarmersController : Controller
    {
        private readonly AppDbContext _context;

        public FarmersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Farmers
        public async Task<IActionResult> Index(string? searchString)
        {
            var appDbContext = _context.Farmers.Include(f => f.ApplicationUser);

            var farmers = from f in _context.Farmers.Include(f => f.ApplicationUser)
                          select f;

            if (!String.IsNullOrEmpty(searchString))
            {
                farmers = farmers.Where(s => s.Name.Contains(searchString) || s.Email.Contains(searchString));
            }

            var farmerList = await farmers.ToListAsync();
            if (!farmerList.Any())
            {
                UserClass.noResults = "No results found. Reverted to showing all records";
                return View(await appDbContext.ToListAsync());
            } else
            {
                UserClass.noResults = string.Empty;
                return View(farmerList);
            }

            
        }

        // GET: Farmers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmers
                .Include(f => f.ApplicationUser)
                .FirstOrDefaultAsync(m => m.FarmerId == id);
            if (farmer == null)
            {
                return NotFound();
            }

            return View(farmer);
        }

        // GET: Farmers/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Farmers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FarmerId,Name,Email,Id")] Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(farmer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.ApplicationUsers, "Id", "Id", farmer.Id);
            return View(farmer);
        }

        // GET: Farmers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmers
                .Include(f => f.ApplicationUser)
                .FirstOrDefaultAsync(m => m.FarmerId == id);
            if (farmer == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.ApplicationUsers, "Id", "Id", farmer.Id);
            return View(farmer);
        }

        // POST: Farmers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? FarmerId, [Bind("FarmerId,Name,Email")] Farmer model)
        {
            var farmer = await _context.Farmers
                .Include(f => f.ApplicationUser)
                .FirstOrDefaultAsync(m => m.FarmerId == FarmerId);

            if (farmer == null || FarmerId != farmer.FarmerId)
            {
                   return NotFound();
            }          
            if (!ModelState.IsValid)
            {
                try
                {                   
                    farmer.Name = model.Name;
                    farmer.Email = model.Email;
                    _context.Update(farmer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmerExists(farmer.FarmerId))
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
            return View(farmer);
        }

        // GET: Farmers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmers
                .Include(f => f.ApplicationUser)
                .FirstOrDefaultAsync(m => m.FarmerId == id);
            if (farmer == null)
            {
                return NotFound();
            }

            return View(farmer);
        }

        // POST: Farmers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farmer = await _context.Farmers.FindAsync(id);
            if (farmer != null)
            {
                _context.Farmers.Remove(farmer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmerExists(int id)
        {
            return _context.Farmers.Any(e => e.FarmerId == id);
        }
    }
}
