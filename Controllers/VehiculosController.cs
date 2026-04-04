using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Models;

[Authorize]
public class VehiculosController : Controller
{
    private readonly AppDbContext _context;

    public VehiculosController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var vehiculos = await _context.Vehiculos
            .Include(v => v.Cliente)
            .ToListAsync();

        return View(vehiculos);
    }

    public IActionResult Create()
    {
        ViewBag.Clientes = new SelectList(_context.Clientes, "Id", "Nombre");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Vehiculo vehiculo)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Clientes = new SelectList(_context.Clientes, "Id", "Nombre");
            return View(vehiculo);
        }

        _context.Vehiculos.Add(vehiculo);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    // EDIT GET
    public async Task<IActionResult> Edit(int id)
    {
        var vehiculo = await _context.Vehiculos.FindAsync(id);
        if (vehiculo == null) return NotFound();

        ViewBag.Clientes = new SelectList(_context.Clientes, "Id", "Nombre", vehiculo.ClienteId);
        return View(vehiculo);
    }

    // EDIT POST
    [HttpPost]
    public async Task<IActionResult> Edit(Vehiculo vehiculo)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Clientes = new SelectList(_context.Clientes, "Id", "Nombre", vehiculo.ClienteId);
            return View(vehiculo);
        }

        _context.Update(vehiculo);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    // DELETE
    public async Task<IActionResult> Delete(int id)
    {
        var vehiculo = await _context.Vehiculos.FindAsync(id);

        if (vehiculo != null)
        {
            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }
}
