using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Models;

[Authorize]
public class FacturacionController : Controller
{
    private readonly AppDbContext _context;

    public FacturacionController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var facturas = await _context.Facturacion
            .Include(f => f.Orden)
            .Include(f => f.Pagos)
            .ToListAsync();

        return View(facturas);
    }

    public async Task<IActionResult> Create()
    {
        await CargarOrdenesAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Facturacion factura)
    {
        if (!ModelState.IsValid)
        {
            await CargarOrdenesAsync(factura.OrdenId);
            return View(factura);
        }

        _context.Facturacion.Add(factura);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var factura = await _context.Facturacion
            .Include(f => f.Orden)
            .Include(f => f.Pagos)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (factura == null) return NotFound();

        await CargarOrdenesAsync(factura.OrdenId);
        return View(factura);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Facturacion factura)
    {
        if (!ModelState.IsValid)
        {
            await CargarOrdenesAsync(factura.OrdenId);
            return View(factura);
        }

        _context.Facturacion.Update(factura);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var factura = await _context.Facturacion.FindAsync(id);

        if (factura != null)
        {
            _context.Facturacion.Remove(factura);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task CargarOrdenesAsync(int? ordenId = null)
    {
        var ordenes = await _context.OrdenesTrabajo
            .Include(o => o.Cliente)
            .Include(o => o.Vehiculo)
            .ToListAsync();

        var opciones = ordenes.Select(o => new
        {
            o.Id,
            Descripcion = $"Orden #{o.Id} - {o.Estado}"
        }).ToList();

        ViewBag.Ordenes = new SelectList(opciones, "Id", "Descripcion", ordenId);
    }
}
