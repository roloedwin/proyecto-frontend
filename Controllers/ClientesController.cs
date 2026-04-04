using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Models;

[Authorize]
public class ClientesController : Controller
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    // LISTAR
    public async Task<IActionResult> Index()
    {
        var clientes = await _context.Clientes.ToListAsync();
        return View(clientes);
    }

    //  CREATE
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Cliente cliente)
    {
        if (!ModelState.IsValid)
            return View(cliente);

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    //  EDIT GET
    public async Task<IActionResult> Edit(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null) return NotFound();

        return View(cliente);
    }

    //  EDIT POST
    [HttpPost]
    public async Task<IActionResult> Edit(Cliente cliente)
    {
        if (!ModelState.IsValid)
            return View(cliente);

        _context.Update(cliente);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    //  delete
    public async Task<IActionResult> Delete(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente != null)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }
}
