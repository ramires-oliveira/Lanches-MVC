using System.ComponentModel.DataAnnotations;

namespace Lanches_MVC.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Nome de usuário")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "Informe o e-mail")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        public string RetornoUrl { get; set; }
    }
}
