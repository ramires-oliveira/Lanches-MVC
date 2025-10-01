using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMVC.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [StringLength(80, MinimumLength = 10, ErrorMessage = "Tamanho máximo 80 caracteres")]
        [Required(ErrorMessage = "Informe o nome do lanche")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [StringLength(100, MinimumLength = 20, ErrorMessage = "Tamanho máximo 100 caracteres")]
        [Required(ErrorMessage = "Informe a descrição do lanche")]
        [Display(Name = "Descrição do lanche")]
        public string DescricaoCurta { get; set; }

        [StringLength(300, MinimumLength = 50, ErrorMessage = "Tamanho máximo 300 caracteres")]
        [Required(ErrorMessage = "Informe a descrição detalhada do lanche")]
        [Display(Name = "Descrição detalhada do lanche")]
        public string DescricaoDetalhada { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        [Required(ErrorMessage = "Informe o preço do lanche")]
        [Display(Name = "Preço do lanche")]
        [Range(1, 999.99, ErrorMessage = "O preço deve estar entre R$ 1,00 e R$ 999,99")]
        public decimal Preco { get; set; }

        [Display(Name = "Imagem do lanche")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Imagem miniatura do lanche")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Lanche preferido")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
