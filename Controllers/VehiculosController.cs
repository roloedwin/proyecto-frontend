using Microsoft.AspNetCore.Mvc;

public class VehiculosController : Controller
{
    public IActionResult Index()
    {
        ViewData["Pagina"] = "Vehiculos";
        return View();
    }
}
