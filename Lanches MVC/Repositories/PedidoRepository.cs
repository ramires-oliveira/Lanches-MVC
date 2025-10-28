using Lanches_MVC.Context;
using Lanches_MVC.Models;
using Lanches_MVC.Repositories.Interfaces;

namespace Lanches_MVC.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        public readonly AppDbContext _context;
        public readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext context, CarrinhoCompra carrinhoCompra)
        {
            _context = context;
            _carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItens;

            foreach (var item in carrinhoCompraItens)
            {
                var pedidoDetalhe = new PedidoDetalhe
                {
                    Quantidade = item.Quantidade,
                    LancheId = item.Lanche.LancheId,
                    PedidoId = pedido.PedidoId,
                    Preco = item.Lanche.Preco
                };

                _context.PedidoDetalhes.Add(pedidoDetalhe);
            }

            _context.SaveChanges();
        }
    }
}
