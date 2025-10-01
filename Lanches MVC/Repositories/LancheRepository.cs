using Lanches_MVC.Context;
using Lanches_MVC.Repositories.Interfaces;
using LanchesMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Lanches_MVC.Repositories
{
    public class LancheRepository: ILancheRepository
    {
        public readonly AppDbContext _context;

        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(x => x.Categoria);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(x => x.IsLanchePreferido).Include(x => x.Categoria);

        public Lanche GetById(int id)
        {
            return _context.Lanches.FirstOrDefault(x => x.LancheId == id);
        }
    }
}
