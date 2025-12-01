using Lanches_MVC.Models;

namespace Lanches_MVC.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Lanche> LanchesPreferidos { get; set; }
    }
}
