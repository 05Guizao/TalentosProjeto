using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TalentosIT.Models
{
    [Table("Skill")]
    public class Skill
    {
        [Key] public int Cod { get; set; }

        [Required] public string Nome { get; set; }

        [Required] public string AreaProfissional { get; set; }

        [Required] public string Estado { get; set; }

        public int? IdUtilizador { get; set; } // Agora é opcional: null = skill global do admin

        [NotMapped] [ValidateNever] public Utilizador Utilizador { get; set; }
    }
}