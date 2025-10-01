using Lanches_MVC.Context;
using Lanches_MVC.Repositories.Interfaces;
using LanchesMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Lanches_MVC.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias.Include(x => x.Lanches);
    }
}
