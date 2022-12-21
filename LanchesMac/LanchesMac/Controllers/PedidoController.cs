using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class PedidoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
