using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Models;

[Authorize]
public class ProveedoresController : Controller
{
    private readonly AppDbContext _context;

    public ProveedoresController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var proveedores = await _context.Proveedores
            .Include(p => p.Repuestos)
            .ToListAsync();

        return View(proveedores);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Proveedor proveedor)
    {
        if (!ModelState.IsValid)
            return View(proveedor);

        _context.Proveedores.Add(proveedor);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var proveedor = await _context.Proveedores.FindAsync(id);
        if (proveedor == null) return NotFound();

        return View(proveedor);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Proveedor proveedor)
    {
        if (!ModelState.IsValid)
            return View(proveedor);

        _context.Proveedores.Update(proveedor);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var proveedor = await _context.Proveedores.FindAsync(id);

        if (proveedor != null)
        {
            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
