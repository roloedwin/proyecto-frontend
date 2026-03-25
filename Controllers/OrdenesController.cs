using Microsoft.AspNetCore.Mvc;

public class OrdenesController : Controller
{
    public IActionResult Index()
    {
        ViewData["Pagina"] = "Ordenes";
        return View();
    }
}
