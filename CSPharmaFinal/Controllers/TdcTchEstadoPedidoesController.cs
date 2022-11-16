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
    public class TdcTchEstadoPedidoesController : Controller
    {
        private readonly CspharmaInformacionalContext _context;

        public TdcTchEstadoPedidoesController(CspharmaInformacionalContext context)
        {
            _context = context;
        }

        // GET: TdcTchEstadoPedidoes
        public async Task<IActionResult> Index()
        {
            var cspharmaInformacionalContext = _context.TdcTchEstadoPedidos.Include(t => t.CodEstadoDevolucionNavigation).Include(t => t.CodEstadoEnvioNavigation).Include(t => t.CodEstadoPagoNavigation).Include(t => t.CodLineaNavigation);
            return View(await cspharmaInformacionalContext.ToListAsync());
        }

        // GET: TdcTchEstadoPedidoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            var tdcTchEstadoPedido = await _context.TdcTchEstadoPedidos
                .Include(t => t.CodEstadoDevolucionNavigation)
                .Include(t => t.CodEstadoEnvioNavigation)
                .Include(t => t.CodEstadoPagoNavigation)
                .Include(t => t.CodLineaNavigation)
                .FirstOrDefaultAsync(m => m.MdUuid == id);
            if (tdcTchEstadoPedido == null)
            {
                return NotFound();
            }

            return View(tdcTchEstadoPedido);
        }

        // GET: TdcTchEstadoPedidoes/Create
        public IActionResult Create()
        {
            ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "MdUuid");
            ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "MdUuid");
            ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "MdUuid");
            ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "MdUuid");
            return View();
        }

        // POST: TdcTchEstadoPedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MdUuid,MdDate,Id,CodEstadoEnvio,CodEstadoPago,CodEstadoDevolucion,CodPedido,CodLinea")] TdcTchEstadoPedido tdcTchEstadoPedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tdcTchEstadoPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "MdUuid", tdcTchEstadoPedido.CodEstadoDevolucion);
            ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "MdUuid", tdcTchEstadoPedido.CodEstadoEnvio);
            ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "MdUuid", tdcTchEstadoPedido.CodEstadoPago);
            ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "MdUuid", tdcTchEstadoPedido.CodLinea);
            return View(tdcTchEstadoPedido);
        }

        // GET: TdcTchEstadoPedidoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            var tdcTchEstadoPedido = await _context.TdcTchEstadoPedidos.FindAsync(id);
            if (tdcTchEstadoPedido == null)
            {
                return NotFound();
            }
            ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "MdUuid", tdcTchEstadoPedido.CodEstadoDevolucion);
            ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "MdUuid", tdcTchEstadoPedido.CodEstadoEnvio);
            ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "MdUuid", tdcTchEstadoPedido.CodEstadoPago);
            ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "MdUuid", tdcTchEstadoPedido.CodLinea);
            return View(tdcTchEstadoPedido);
        }

        // POST: TdcTchEstadoPedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MdUuid,MdDate,Id,CodEstadoEnvio,CodEstadoPago,CodEstadoDevolucion,CodPedido,CodLinea")] TdcTchEstadoPedido tdcTchEstadoPedido)
        {
            if (id != tdcTchEstadoPedido.MdUuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tdcTchEstadoPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TdcTchEstadoPedidoExists(tdcTchEstadoPedido.MdUuid))
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
            ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "MdUuid", tdcTchEstadoPedido.CodEstadoDevolucion);
            ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "MdUuid", tdcTchEstadoPedido.CodEstadoEnvio);
            ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "MdUuid", tdcTchEstadoPedido.CodEstadoPago);
            ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "MdUuid", tdcTchEstadoPedido.CodLinea);
            return View(tdcTchEstadoPedido);
        }

        // GET: TdcTchEstadoPedidoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            var tdcTchEstadoPedido = await _context.TdcTchEstadoPedidos
                .Include(t => t.CodEstadoDevolucionNavigation)
                .Include(t => t.CodEstadoEnvioNavigation)
                .Include(t => t.CodEstadoPagoNavigation)
                .Include(t => t.CodLineaNavigation)
                .FirstOrDefaultAsync(m => m.MdUuid == id);
            if (tdcTchEstadoPedido == null)
            {
                return NotFound();
            }

            return View(tdcTchEstadoPedido);
        }

        // POST: TdcTchEstadoPedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TdcTchEstadoPedidos == null)
            {
                return Problem("Entity set 'CspharmaInformacionalContext.TdcTchEstadoPedidos'  is null.");
            }
            var tdcTchEstadoPedido = await _context.TdcTchEstadoPedidos.FindAsync(id);
            if (tdcTchEstadoPedido != null)
            {
                _context.TdcTchEstadoPedidos.Remove(tdcTchEstadoPedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TdcTchEstadoPedidoExists(string id)
        {
          return _context.TdcTchEstadoPedidos.Any(e => e.MdUuid == id);
        }
    }
}
