using Microsoft.AspNetCore.Mvc;

public class FacturacionController : Controller
{
    public IActionResult Index()
    {
        ViewData["Pagina"] = "Facturacion";
        return View();
    }
}
