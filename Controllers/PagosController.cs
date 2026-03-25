using Microsoft.AspNetCore.Mvc;

public class PagosController : Controller
{
    public IActionResult Index()
    {
        ViewData["Pagina"] = "Pagos";
        return View();
    }
}
