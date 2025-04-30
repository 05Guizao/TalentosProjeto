using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TalentosIT.Models
{
    [Table("Skill")]
    public class Skill
    {
        [Key]
        public int Cod { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string AreaProfissional { get; set; }

        [Required]
        public string Estado { get; set; }

        // FK para ligar ao utilizador logado
        public int IdUtilizador { get; set; }

        // Não quero que o binder/validador tente preencher isto no POST:
        [NotMapped]
        [ValidateNever]
        public Utilizador Utilizador { get; set; }
    }
}