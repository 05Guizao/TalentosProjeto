using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentosIT.Models
{
    [Table("Utilizador")]
    public class Utilizador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A password é obrigatória")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O tipo de conta é obrigatório")]
        public string Tipo { get; set; }
    }
}