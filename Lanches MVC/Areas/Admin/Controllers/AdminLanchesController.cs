using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lanches_MVC.Context;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ReflectionIT.Mvc.Paging;
using Lanches_MVC.Models;
using Microsoft.Extensions.Options;

namespace Lanches_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminLanchesController : Controller
    {
        private readonly AppDbContext _context;
        public readonly ConfigurationImagens _myConfig;
        public readonly IWebHostEnvironment _hostingEnvironment;

        public AdminLanchesController(AppDbContext context, IWebHostEnvironment hostingEnviroment, IOptions<ConfigurationImagens> myConfiguration)
        {
            _context = context;
            _hostingEnvironment = hostingEnviroment;
            _myConfig = myConfiguration.Value;
        }

        public async Task<IActionResult> Index(string filter, int pageIndex = 1, string sort = "Nome")
        {
            var resultado = _context.Lanches.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(x => x.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 5, pageIndex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lanches == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanches
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.LancheId == id);
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome");
            ViewData["Imagens"] = GetImagens();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LancheId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemUrl,ImagemThumbnailUrl,IsLanchePreferido,EmEstoque,CategoriaId")] Lanche lanche)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lanche);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", lanche.CategoriaId);
            ViewData["Imagens"] = GetImagens();

            return View(lanche);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lanches == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanches.FindAsync(id);
            if (lanche == null)
            {
                return NotFound();
            }

            var listFiles = new List<SelectListItem>();
            listFiles = GetImagens();

            var imagemSelecionada = lanche.ImagemUrl;

            var item = listFiles.FirstOrDefault(x => x.Value == imagemSelecionada);
            
            if (item != null) {
                listFiles.FirstOrDefault(x => x.Value == imagemSelecionada).Selected = true;
            };

            ViewData["Imagens"] = listFiles;
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", lanche.CategoriaId);
            
            return View(lanche);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LancheId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemUrl,ImagemThumbnailUrl,IsLanchePreferido,EmEstoque,CategoriaId")] Lanche lanche)
        {
            if (id != lanche.LancheId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lanche);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LancheExists(lanche.LancheId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", lanche.CategoriaId);
            return View(lanche);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lanches == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanches
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.LancheId == id);
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lanches == null)
            {
                return Problem("Entity set 'AppDbContext.Lanches'  is null.");
            }
            var lanche = await _context.Lanches.FindAsync(id);
            if (lanche != null)
            {
                _context.Lanches.Remove(lanche);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LancheExists(int id)
        {
          return _context.Lanches.Any(e => e.LancheId == id);
        }

        private List<SelectListItem> GetImagens()
        {
            var listFiles = new List<SelectListItem>();

            var pastaImagens = _myConfig.NomePastaImagensProdutos;
            var userImagesPath = Path.Combine(
                _hostingEnvironment.WebRootPath,
                pastaImagens
            );

            if (Directory.Exists(userImagesPath))
            {
                var files = Directory.GetFiles(userImagesPath);

                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);

                    listFiles.Add(new SelectListItem
                    {
                        Text = fileName,
                        Value = $"/{pastaImagens}/{fileName}"
                    });
                }
            }

            return listFiles;
        }
    }
}
