using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Models;

[Authorize]
public class ServiciosController : Controller
{
    private readonly AppDbContext _context;

    public ServiciosController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var servicios = await _context.Servicios
            .Include(s => s.Detalles)
            .ToListAsync();

        return View(servicios);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Servicio servicio)
    {
        if (!ModelState.IsValid)
            return View(servicio);

        _context.Servicios.Add(servicio);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var servicio = await _context.Servicios
            .Include(s => s.Detalles)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (servicio == null) return NotFound();

        return View(servicio);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Servicio servicio)
    {
        if (!ModelState.IsValid)
            return View(servicio);

        _context.Servicios.Update(servicio);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var servicio = await _context.Servicios.FindAsync(id);

        if (servicio != null)
        {
            _context.Servicios.Remove(servicio);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
