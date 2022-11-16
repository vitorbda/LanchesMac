using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Nome")]
        [StringLength(30, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe a descrição")]
        [Display(Name = "Descrição Curta")]
        [StringLength(50, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string DescricaoCurta { get; set; }
        [Display(Name = "Descrição Detalhada")]
        [MinLength(20, ErrorMessage = "Descrição detalhada deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição detalhada deve ter no máximo {1} caracteres")]
        public string DescricaoDetalhada { get; set; }
        [Required(ErrorMessage = "Informe o preço")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1,999.99, ErrorMessage = "O preço deve estar entre 1 e 999")]
        public decimal Preco { get; set; }
        [Display(Name = "Caminho da imagem normal")]
        public string ImagemUrl { get; set; }
        [Display(Name = "Caminho da thumbnail")]
        public string ImagemThumbnailUrl { get; set; }
        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }
        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
