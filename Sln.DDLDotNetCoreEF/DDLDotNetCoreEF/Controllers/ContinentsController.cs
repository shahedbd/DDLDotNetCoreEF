using DDLDotNetCoreEF.Models;
using Microsoft.AspNetCore.Mvc;
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

            List<Continent> continentlist = _context.Continent.ToList();
            continentlist.Insert(0, new Continent { ContinentID = 0, ContinentName = "Select Item" });
            ViewBag.ContinentList = continentlist;
            return View();
        }

        // GET: ContinentsTMP/Create
        public IActionResult Create()
        {
            List<Continent> continentlist = new List<Continent>();
            continentlist = (from v in _context.Continent select v).ToList();
            continentlist.Insert(0, new Continent { ContinentID = 0, ContinentName = "Select" });

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
                    ViewBag.ErrorContinentID = "This Continent ID alredy exits!";
                }

            }
            return View(continent);
        }
    }
}
