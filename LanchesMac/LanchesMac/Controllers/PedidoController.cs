using LanchesMac.Models;
using LanchesMac.Respositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepositoriy _pedidoRepositoriy;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepositoriy pedidoRepositoriy, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepositoriy = pedidoRepositoriy;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Pedido pedido) 
        {
            return View();
        }
    }
}
