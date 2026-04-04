using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Models;

[Authorize]
public class OrdenesController : Controller
{
    private readonly AppDbContext _context;

    public OrdenesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var ordenes = await _context.OrdenesTrabajo
            .Include(o => o.Cliente)
            .Include(o => o.Vehiculo)
            .Include(o => o.Detalles)
            .ToListAsync();

        return View(ordenes);
    }

    public async Task<IActionResult> Create()
    {
        await CargarRelacionesAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrdenTrabajo ordenTrabajo)
    {
        if (!ModelState.IsValid)
        {
            await CargarRelacionesAsync(ordenTrabajo.ClienteId, ordenTrabajo.VehiculoId);
            return View(ordenTrabajo);
        }

        _context.OrdenesTrabajo.Add(ordenTrabajo);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var ordenTrabajo = await _context.OrdenesTrabajo
            .Include(o => o.Cliente)
            .Include(o => o.Vehiculo)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (ordenTrabajo == null) return NotFound();

        await CargarRelacionesAsync(ordenTrabajo.ClienteId, ordenTrabajo.VehiculoId);
        return View(ordenTrabajo);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(OrdenTrabajo ordenTrabajo)
    {
        if (!ModelState.IsValid)
        {
            await CargarRelacionesAsync(ordenTrabajo.ClienteId, ordenTrabajo.VehiculoId);
            return View(ordenTrabajo);
        }

        _context.OrdenesTrabajo.Update(ordenTrabajo);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var ordenTrabajo = await _context.OrdenesTrabajo.FindAsync(id);

        if (ordenTrabajo != null)
        {
            _context.OrdenesTrabajo.Remove(ordenTrabajo);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task CargarRelacionesAsync(int? clienteId = null, int? vehiculoId = null)
    {
        var clientes = await _context.Clientes.ToListAsync();
        var vehiculos = await _context.Vehiculos
            .Include(v => v.Cliente)
            .ToListAsync();

        ViewBag.Clientes = new SelectList(clientes, "Id", "Nombre", clienteId);
        ViewBag.Vehiculos = new SelectList(vehiculos, "Id", "Placa", vehiculoId);
    }
}
