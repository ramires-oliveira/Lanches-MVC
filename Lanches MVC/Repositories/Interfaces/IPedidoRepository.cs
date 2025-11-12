using Lanches_MVC.Models;

namespace Lanches_MVC.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
    }
}
