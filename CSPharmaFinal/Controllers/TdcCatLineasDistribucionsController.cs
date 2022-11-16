using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSPharmaDAL.Modelo;

namespace CSPharmaFinal.Controllers
{
    public class TdcCatLineasDistribucionsController : Controller
    {
        private readonly CspharmaInformacionalContext _context;

        public TdcCatLineasDistribucionsController(CspharmaInformacionalContext context)
        {
            _context = context;
        }

        // GET: TdcCatLineasDistribucions
        public async Task<IActionResult> Index()
        {
              return View(await _context.TdcCatLineasDistribucions.ToListAsync());
        }

        // GET: TdcCatLineasDistribucions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TdcCatLineasDistribucions == null)
            {
                return NotFound();
            }

            var tdcCatLineasDistribucion = await _context.TdcCatLineasDistribucions
                .FirstOrDefaultAsync(m => m.MdUuid == id);
            if (tdcCatLineasDistribucion == null)
            {
                return NotFound();
            }

            return View(tdcCatLineasDistribucion);
        }

        // GET: TdcCatLineasDistribucions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TdcCatLineasDistribucions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdUuid,MdDate,Id,CodLinea,CodProvincia,CodMunicipio,CodBarrio")] TdcCatLineasDistribucion tdcCatLineasDistribucion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tdcCatLineasDistribucion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tdcCatLineasDistribucion);
        }

        // GET: TdcCatLineasDistribucions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TdcCatLineasDistribucions == null)
            {
                return NotFound();
            }

            var tdcCatLineasDistribucion = await _context.TdcCatLineasDistribucions.FindAsync(id);
            if (tdcCatLineasDistribucion == null)
            {
                return NotFound();
            }
            return View(tdcCatLineasDistribucion);
        }

        // POST: TdcCatLineasDistribucions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MdUuid,MdDate,Id,CodLinea,CodProvincia,CodMunicipio,CodBarrio")] TdcCatLineasDistribucion tdcCatLineasDistribucion)
        {
            if (id != tdcCatLineasDistribucion.MdUuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tdcCatLineasDistribucion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TdcCatLineasDistribucionExists(tdcCatLineasDistribucion.MdUuid))
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
            return View(tdcCatLineasDistribucion);
        }

        // GET: TdcCatLineasDistribucions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TdcCatLineasDistribucions == null)
            {
                return NotFound();
            }

            var tdcCatLineasDistribucion = await _context.TdcCatLineasDistribucions
                .FirstOrDefaultAsync(m => m.MdUuid == id);
            if (tdcCatLineasDistribucion == null)
            {
                return NotFound();
            }

            return View(tdcCatLineasDistribucion);
        }

        // POST: TdcCatLineasDistribucions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TdcCatLineasDistribucions == null)
            {
                return Problem("Entity set 'CspharmaInformacionalContext.TdcCatLineasDistribucions'  is null.");
            }
            var tdcCatLineasDistribucion = await _context.TdcCatLineasDistribucions.FindAsync(id);
            if (tdcCatLineasDistribucion != null)
            {
                _context.TdcCatLineasDistribucions.Remove(tdcCatLineasDistribucion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TdcCatLineasDistribucionExists(string id)
        {
          return _context.TdcCatLineasDistribucions.Any(e => e.MdUuid == id);
        }
    }
}
