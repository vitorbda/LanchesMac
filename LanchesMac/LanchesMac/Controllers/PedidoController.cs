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
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            //Busca os itens do carrinho
            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItems = items;

            //Se não existirem itens, retorna um erro na ModelState
            if (_carrinhoCompra.CarrinhoCompraItems.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho está vazio...");
            }

            //Percorre os itens do carrinho, pega seus valores
            foreach(var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
            }


            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            //Verifica se a ModelState está valida
            if (ModelState.IsValid)
            {
                //Cria o pedido e os detalhes
                _pedidoRepositoriy.CriarPedido(pedido);

                //Define a mensagem ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarinhoCompraTotal();

                _carrinhoCompra.LimparCarrinho();

                //Retorna a view com os dados do cliente e do pedido
                return View("~/View/Pedido/CheckoutCompleto.cshtml", pedido);
            }
            return View(pedido);
        }
    }
}
