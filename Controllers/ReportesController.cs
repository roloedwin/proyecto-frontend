using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class ReportesController : Controller
{
    public IActionResult Index()
    {
        ViewData["Pagina"] = "Reportes";
        return View();
    }
}
