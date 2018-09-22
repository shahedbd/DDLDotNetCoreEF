using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DDLDotNetCoreEF.Models;

namespace DDLDotNetCoreEF.Controllers
{
    public class ContinentsTMPController : Controller
    {
        private readonly DataBaseContext _context;

        public ContinentsTMPController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: ContinentsTMP
        public async Task<IActionResult> Index()
        {
            return View(await _context.Continent.ToListAsync());
        }

        // GET: ContinentsTMP/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continent = await _context.Continent
                .FirstOrDefaultAsync(m => m.ContinentID == id);
            if (continent == null)
            {
                return NotFound();
            }

            return View(continent);
        }

        // GET: ContinentsTMP/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContinentsTMP/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContinentID,ContinentName")] Continent continent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(continent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(continent);
        }

        // GET: ContinentsTMP/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continent = await _context.Continent.FindAsync(id);
            if (continent == null)
            {
                return NotFound();
            }
            return View(continent);
        }

        // POST: ContinentsTMP/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("ContinentID,ContinentName")] Continent continent)
        {
            if (id != continent.ContinentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(continent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContinentExists(continent.ContinentID))
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
            return View(continent);
        }

        // GET: ContinentsTMP/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continent = await _context.Continent
                .FirstOrDefaultAsync(m => m.ContinentID == id);
            if (continent == null)
            {
                return NotFound();
            }

            return View(continent);
        }

        // POST: ContinentsTMP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var continent = await _context.Continent.FindAsync(id);
            _context.Continent.Remove(continent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContinentExists(byte id)
        {
            return _context.Continent.Any(e => e.ContinentID == id);
        }
    }
}
