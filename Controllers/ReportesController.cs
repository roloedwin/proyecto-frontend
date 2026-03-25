using Microsoft.AspNetCore.Mvc;

public class ReportesController : Controller
{
    public IActionResult Index()
    {
        ViewData["Pagina"] = "Reportes";
        return View();
    }
}
