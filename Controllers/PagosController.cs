using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Models;

[Authorize]
public class PagosController : Controller
{
    private readonly AppDbContext _context;

    public PagosController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var pagos = await _context.Pagos
            .Include(p => p.Factura)
            .ThenInclude(f => f!.Orden)
            .ToListAsync();

        return View(pagos);
    }

    public async Task<IActionResult> Create()
    {
        await CargarFacturasAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Pago pago)
    {
        if (!ModelState.IsValid)
        {
            await CargarFacturasAsync(pago.FacturaId);
            return View(pago);
        }

        _context.Pagos.Add(pago);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var pago = await _context.Pagos
            .Include(p => p.Factura)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pago == null) return NotFound();

        await CargarFacturasAsync(pago.FacturaId);
        return View(pago);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Pago pago)
    {
        if (!ModelState.IsValid)
        {
            await CargarFacturasAsync(pago.FacturaId);
            return View(pago);
        }

        _context.Pagos.Update(pago);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var pago = await _context.Pagos.FindAsync(id);

        if (pago != null)
        {
            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task CargarFacturasAsync(int? facturaId = null)
    {
        var facturas = await _context.Facturacion
            .Include(f => f.Orden)
            .ToListAsync();

        var opciones = facturas.Select(f => new
        {
            f.Id,
            Descripcion = $"Factura #{f.Id} - Total {f.Total}"
        }).ToList();

        ViewBag.Facturas = new SelectList(opciones, "Id", "Descripcion", facturaId);
    }
}
