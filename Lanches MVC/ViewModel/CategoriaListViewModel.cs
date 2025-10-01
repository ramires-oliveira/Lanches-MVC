using LanchesMVC.Models;

namespace Lanches_MVC.ViewModel
{
    public class CategoriaListViewModel
    {
        public IEnumerable<Categoria> Categorias { get; set; }
        public IEnumerable<Lanche> Lanches { get; set; }
    }
}
