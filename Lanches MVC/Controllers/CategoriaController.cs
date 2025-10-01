using Microsoft.AspNetCore.Mvc;

namespace Lanches_MVC.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
