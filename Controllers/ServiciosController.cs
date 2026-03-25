using Microsoft.AspNetCore.Mvc;

public class ServiciosController : Controller
{
    public IActionResult Index()
    {
        ViewData["Pagina"] = "Servicios";
        return View();
    }
}
