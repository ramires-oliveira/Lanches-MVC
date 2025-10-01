using Lanches_MVC.Models;
using Lanches_MVC.Repositories.Interfaces;
using Lanches_MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lanches_MVC.Controllers
{
    public class LancheController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public LancheController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult List()
        {
            var categoriaListViewModel = new CategoriaListViewModel();

            categoriaListViewModel.Categorias = _categoriaRepository.Categorias;

            return View(categoriaListViewModel);
        }
    }
}
