using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DDLDotNetCoreEF.Models;
using DDLDotNetCoreEF.LIB;

namespace DDLDotNetCoreEF.Controllers
{
    public class GlobalCitizensController : Controller
    {
        private readonly DataBaseContext _context;

        public GlobalCitizensController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: GlobalCitizens
        public async Task<IActionResult> Index()
        {
            //var innerJoinQuery =
                //from gctgn in _context.GlobalCitizen
                //join cont in _context.Continent on gctgn.ContinentCode equals cont.ContinentID
                //select new { gctgn.Name, gctgn.CountryName, cont.ContinentName, gctgn.CreationDateTime };

            return View(await _context.GlobalCitizen.ToListAsync());
        }

        // GET: GlobalCitizens/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var globalCitizen = await _context.GlobalCitizen
                .FirstOrDefaultAsync(m => m.ID == id);
            if (globalCitizen == null)
            {
                return NotFound();
            }

            return View(globalCitizen);
        }

        // GET: GlobalCitizens/Create
        public IActionResult Create()
        {
            List<Continent> continentlist = _context.Continent.ToList();
            continentlist.Insert(0, new Continent { ContinentID = 0, ContinentName = "Select Item" });
            ViewBag.ContinentList = continentlist;
            return View();
        }

        // POST: GlobalCitizens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CountryName,ContinentCode")] GlobalCitizen globalCitizen)
        {
            if (ModelState.IsValid)
            {
                var ddlContinentCode = Request.Form["ContinentCode"].ToString();
                globalCitizen.ContinentCode = Convert.ToByte(ddlContinentCode);
                globalCitizen.Status = (int)CommonEnumVaues.UserStatus.Active;

                globalCitizen.CreationUser = "Admin";
                globalCitizen.CreationDateTime = DateTime.Now;

                globalCitizen.LastUpdateUser = null;
                globalCitizen.LastUpdateDateTime = null;

                _context.Add(globalCitizen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(globalCitizen);
        }

        // GET: GlobalCitizens/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<Continent> continentlist = _context.Continent.ToList();
            continentlist.Insert(0, new Continent { ContinentID = 0, ContinentName = "Select Item" });
            ViewBag.ContinentList = continentlist;

            var globalCitizen = await _context.GlobalCitizen.FindAsync(id);
            if (globalCitizen == null)
            {
                return NotFound();
            }
            return View(globalCitizen);
        }

        // POST: GlobalCitizens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ID,Name,CountryName,ContinentCode,CreationUser,CreationDateTime")] GlobalCitizen globalCitizen)
        {
            if (id != globalCitizen.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ddlContinentCode = Request.Form["ContinentCode"].ToString();
                    globalCitizen.ContinentCode = Convert.ToByte(ddlContinentCode);
                    

                    globalCitizen.LastUpdateUser = "Admin in Update Mode";
                    globalCitizen.LastUpdateDateTime = DateTime.Now;

                    _context.Update(globalCitizen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GlobalCitizenExists(globalCitizen.ID))
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
            return View(globalCitizen);
        }

        // GET: GlobalCitizens/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var globalCitizen = await _context.GlobalCitizen
                .FirstOrDefaultAsync(m => m.ID == id);
            if (globalCitizen == null)
            {
                return NotFound();
            }

            return View(globalCitizen);
        }

        // POST: GlobalCitizens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var globalCitizen = await _context.GlobalCitizen.FindAsync(id);
            _context.GlobalCitizen.Remove(globalCitizen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GlobalCitizenExists(long id)
        {
            return _context.GlobalCitizen.Any(e => e.ID == id);
        }
    }
}
