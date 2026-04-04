using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Models;

[Authorize]
public class RepuestosController : Controller
{
    private readonly AppDbContext _context;

    public RepuestosController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var repuestos = await _context.Repuestos
            .Include(r => r.Proveedor)
            .ToListAsync();

        return View(repuestos);
    }

    public async Task<IActionResult> Create()
    {
        await CargarProveedoresAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Repuesto repuesto)
    {
        if (!ModelState.IsValid)
        {
            await CargarProveedoresAsync(repuesto.ProveedorId);
            return View(repuesto);
        }

        _context.Repuestos.Add(repuesto);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var repuesto = await _context.Repuestos
            .Include(r => r.Proveedor)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (repuesto == null) return NotFound();

        await CargarProveedoresAsync(repuesto.ProveedorId);
        return View(repuesto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Repuesto repuesto)
    {
        if (!ModelState.IsValid)
        {
            await CargarProveedoresAsync(repuesto.ProveedorId);
            return View(repuesto);
        }

        _context.Repuestos.Update(repuesto);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var repuesto = await _context.Repuestos.FindAsync(id);

        if (repuesto != null)
        {
            _context.Repuestos.Remove(repuesto);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task CargarProveedoresAsync(int? proveedorId = null)
    {
        var proveedores = await _context.Proveedores.ToListAsync();
        ViewBag.Proveedores = new SelectList(proveedores, "Id", "Nombre", proveedorId);
    }
}
