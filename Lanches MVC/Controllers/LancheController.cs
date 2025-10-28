using Lanches_MVC.Models;
using Lanches_MVC.Repositories.Interfaces;
using Lanches_MVC.ViewModel;
using LanchesMVC.Models;
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

        public IActionResult List(string searchString)
        {
            var categoriaListViewModel = new CategoriaListViewModel();

            var categorias = _categoriaRepository.Categorias;

            if (!string.IsNullOrEmpty(searchString))
            {
                foreach (var categoria in categorias)
                {
                    categoria.Lanches = categoria.Lanches
                        .Where(x => x.Nome.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
            }

            categoriaListViewModel.Categorias = categorias.Where(x => x.Lanches.Any());
            ViewBag.SearchString = searchString;

            return View(categoriaListViewModel);
        }
    }
}
