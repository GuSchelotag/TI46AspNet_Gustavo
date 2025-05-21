using System.ComponentModel.DataAnnotations;
namespace PrimeiroProjeto.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MinLength(4, ErrorMessage = "O nome precisa ter no mínimo 4 letras.")]
        public string Nome { get; set; }

        [Phone(ErrorMessage = "O telefone deve estar em um formato válido.")]
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "O e-mail deve estar em um formato válido.")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; } 
    }
}
