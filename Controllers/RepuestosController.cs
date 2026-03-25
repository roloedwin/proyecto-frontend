using Microsoft.AspNetCore.Mvc;

public class RepuestosController : Controller
{
    public IActionResult Index()
    {
        ViewData["Pagina"] = "Repuestos";
        return View();
    }
}
