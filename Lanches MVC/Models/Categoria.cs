using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMVC.Models
{
    [Table("Categorias")]
    public class Categoria
    { 
        [Key]
        public int CategoriaId { get; set; }

        [StringLength(100, ErrorMessage = "Tamanho máximo 100 caracteres")]
        [Required(ErrorMessage = "Informe a categoria")]
        [Display(Name = "Nome da categoria")]
        public string CategoriaNome { get; set; }

        [StringLength(100, ErrorMessage = "Tamanho máximo 200 caracteres")]
        [Required(ErrorMessage = "Informe a descrição")]
        [Display(Name = "Descrição da categoria")]
        public string Descricao { get; set; }

        public List<Lanche> Lanches { get; set; }
    }
}
