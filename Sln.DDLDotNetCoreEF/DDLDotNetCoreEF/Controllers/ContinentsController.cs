using DDLDotNetCoreEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDLDotNetCoreEF.Controllers
{
    public class ContinentsController : Controller
    {
        private readonly DataBaseContext _context;

        public ContinentsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Continents
        public IActionResult Index()
        {
            return View(_context.Continent.ToList());
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContinentID,ContinentName")] Continent continent)
        {
            if (ModelState.IsValid)
            {
                int result = (from tobj in _context.Continent
                              where tobj.ContinentID == continent.ContinentID
                              select tobj).ToList().Count();


                if (result == 0)
                {
                    _context.Add(continent);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //TempData["ErrorMSG"] = "Access Denied! Wrong Credential";
                    ViewBag.ErrorContinentID = "This Continent ID: " + continent.ContinentID + " alredy exits!";
                }

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
            if (id != continent.ContinentID) return NotFound();


            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

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
                        transaction.Commit();
                        return RedirectToAction(nameof(Index));
                    }

                }
                catch (Exception ex)
                {
                    // TODO: Handle failure
                    transaction.Rollback();
                    throw ex;
                }
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

            var continent = await _context.Continent.FirstOrDefaultAsync(m => m.ContinentID == id);
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
