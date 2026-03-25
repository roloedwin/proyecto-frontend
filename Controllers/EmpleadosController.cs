using Microsoft.AspNetCore.Mvc;

public class EmpleadosController : Controller
{
    public IActionResult Index()
    {
        ViewData["Pagina"] = "Empleados";
        return View();
    }
}
