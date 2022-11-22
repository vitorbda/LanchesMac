using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            //adiciona as operações do banco nessa classe
            _context = context;
        }

        //atributos da classe
        public string Id { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //define uma sessão
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //obtem um serviço do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            //obtem ou gera o Id do carrinho            
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();
            //                  busca a sessão 'carrinhoId'         se não existir, cria uma nova

            //atribui o id do carrinho na sessão
            session.SetString("CarrinhoId", carrinhoId);

            //retorna o carrinho com o contexto e o Id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                Id = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            //verifica se o item já existe no carrinho
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.Id == lanche.Id &&
                s.CarrinhoCompraId == Id);

            //se não existir, cria um novo item no carrinho
            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = Id,
                    Lanche = lanche,
                    Quantidade = 1
                };
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else //se existir, incrementa 1 unidade
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }

        public void RemoverDoCarrinho (Lanche lanche)
        {
            //busca o lanche no carrinho
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.Id == lanche.Id &&
                s.CarrinhoCompraId == Id);

            //se encontrar
            if(carrinhoCompraItem != null)
            {
                //verifica se existe mais de 1 do mesmo
                if(carrinhoCompraItem.Quantidade > 1)
                {
                    //se existir, decrementa 1
                    carrinhoCompraItem.Quantidade--;
                }
                else
                {
                    //se não, remove do carrinho
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges();
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            //se o carrinho não for nulo, retorna a lista de itens
            return CarrinhoCompraItems ??
                (CarrinhoCompraItems =
                    _context.CarrinhoCompraItens
                    .Where(c => c.CarrinhoCompraId == Id)
                    .Include(s => s.Lanche)
                    .ToList());
        }

        public void LimparCarrinho()
        {
            //busca todos os itens do carrinho e apaga
            var carrinhoItens = _context.CarrinhoCompraItens
                                .Where(carrinho => carrinho.CarrinhoCompraId == Id);

            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        public decimal GetCarinhoCompraTotal()
        {
            //soma o valor total, buscando na tabela CarrinhoCompraItens
            var total = _context.CarrinhoCompraItens
                        .Where(c => c.CarrinhoCompraId == Id)
                        .Select(c => c.Lanche.Preco * c.Quantidade).Sum();
            return total;
        }
    }
}
