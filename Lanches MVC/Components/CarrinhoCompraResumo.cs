using Lanches_MVC.Models;
using Lanches_MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Lanches_MVC.Components
{
    public class CarrinhoCompraResumo: ViewComponent
    {
        public readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
        {
            _carrinhoCompra = carrinhoCompra;
        }

        public IViewComponentResult Invoke()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItems();

            _carrinhoCompra.CarrinhoCompraItens = itens;

            var carrinhoCompraViewModel = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetTotalCarrinhoCompra()
            };

            return View(carrinhoCompraViewModel);
        }
    }
}
