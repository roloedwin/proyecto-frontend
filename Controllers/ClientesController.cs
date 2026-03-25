using Microsoft.AspNetCore.Mvc;

public class ClientesController : Controller
{
    public IActionResult Index()
    {
        ViewData["Pagina"] = "Clientes";
        return View();
    }
}
