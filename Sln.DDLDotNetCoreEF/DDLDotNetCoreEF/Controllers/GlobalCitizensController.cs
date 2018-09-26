using DDLDotNetCoreEF.LIB;
using DDLDotNetCoreEF.Models;
using DDLDotNetCoreEF.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Index(string searchString)
        {
            List<GlobalCitizenVM> result = null;
            var p1 = new SqlParameter("@pOptions", 6);
            var p2 = new SqlParameter("@searchString", searchString);


            if (searchString != null)
            {
                result = _context.GlobalCitizenVM.FromSql("EXECUTE sp_GlobalCitizen @pOptions=6,@searchString='" + searchString + "'").ToList();
            }
            else
            {
                result = _context.GlobalCitizenVM.FromSql("EXECUTE sp_GlobalCitizen @pOptions=6").ToList();
            }
            TempData["searchString"] = searchString;
            return View(result);
        }


        public async Task<IActionResult> IndexSSS(string searchString)
        {
            //List<GlobalCitizenVM> result = _context.GlobalCitizenVM.FromSql("EXECUTE sp_GlobalCitizen @pOptions=6;").ToList();

            var globalCitizen = from m in _context.GlobalCitizen select new { m.Name, m.Gender };

            if (!String.IsNullOrEmpty(searchString))
            {
                globalCitizen = globalCitizen.Where(s => s.Name.Contains(searchString));
            }

            return View(await globalCitizen.ToListAsync());
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
                var ddlContinentCode = Request.Form["ContinentName"].ToString();
                var rdoGender = Request.Form["Gender"].ToString();


                globalCitizen.ContinentCode = Convert.ToByte(ddlContinentCode);
                globalCitizen.Gender = Convert.ToByte(rdoGender);
                globalCitizen.Status = (int)CommonEnumVaues.UserStatus.Active;

                globalCitizen.CreationUser = "Admin";
                globalCitizen.CreationDateTime = DateTime.Now;

                globalCitizen.LastUpdateUser = null;
                globalCitizen.LastUpdateDateTime = null;

                _context.Add(globalCitizen);
                var abc = await _context.SaveChangesAsync();

                //TempData["ErrorMSG"] = "Access Denied! Wrong Credential";
                TempData["AddNewSuccessMessage"] = "Global citizen  added successfully. Citizen Name: " + globalCitizen.Name;
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
                    var rdoGender = Request.Form["Gender"].ToString();

                    globalCitizen.ContinentCode = Convert.ToByte(ddlContinentCode);
                    globalCitizen.Gender = Convert.ToByte(rdoGender);

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
