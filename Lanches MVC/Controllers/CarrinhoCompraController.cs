using Lanches_MVC.Models;
using Lanches_MVC.Repositories.Interfaces;
using Lanches_MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Lanches_MVC.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var items = _carrinhoCompra.GetCarrinhoCompraItems();

            _carrinhoCompra.CarrinhoCompraItens = items;

            var carrinhoCompraViewModel = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetTotalCarrinhoCompra()
            };
        
            return View(carrinhoCompraViewModel);
        }

        public IActionResult AdicionarItemCarrinhoCompra(int lancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(x => x.LancheId == lancheId);

            if(lanche != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(lanche);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoverItemCarrinhoCompra(int lancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(x => x.LancheId == lancheId);

            if (lanche != null)
            {
                _carrinhoCompra.RemoverItemCarrinho(lanche);
            }

            return RedirectToAction("Index");
        }

    }
}
