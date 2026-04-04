using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Models;

[Authorize]
public class DetalleOrdenesController : Controller
{
    private readonly AppDbContext _context;

    public DetalleOrdenesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var detalles = await _context.DetalleOrden
            .Include(d => d.Orden)
            .Include(d => d.Servicio)
            .ToListAsync();

        return View(detalles);
    }

    public async Task<IActionResult> Create()
    {
        await CargarRelacionesAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(DetalleOrden detalleOrden)
    {
        if (!ModelState.IsValid)
        {
            await CargarRelacionesAsync(detalleOrden.OrdenId, detalleOrden.ServicioId);
            return View(detalleOrden);
        }

        _context.DetalleOrden.Add(detalleOrden);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var detalleOrden = await _context.DetalleOrden
            .Include(d => d.Orden)
            .Include(d => d.Servicio)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (detalleOrden == null) return NotFound();

        await CargarRelacionesAsync(detalleOrden.OrdenId, detalleOrden.ServicioId);
        return View(detalleOrden);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DetalleOrden detalleOrden)
    {
        if (!ModelState.IsValid)
        {
            await CargarRelacionesAsync(detalleOrden.OrdenId, detalleOrden.ServicioId);
            return View(detalleOrden);
        }

        _context.DetalleOrden.Update(detalleOrden);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var detalleOrden = await _context.DetalleOrden.FindAsync(id);

        if (detalleOrden != null)
        {
            _context.DetalleOrden.Remove(detalleOrden);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task CargarRelacionesAsync(int? ordenId = null, int? servicioId = null)
    {
        var ordenes = await _context.OrdenesTrabajo
            .Include(o => o.Cliente)
            .Include(o => o.Vehiculo)
            .ToListAsync();

        var servicios = await _context.Servicios.ToListAsync();

        ViewBag.Ordenes = new SelectList(ordenes, "Id", "Id", ordenId);
        ViewBag.Servicios = new SelectList(servicios, "Id", "Nombre", servicioId);
    }
}
