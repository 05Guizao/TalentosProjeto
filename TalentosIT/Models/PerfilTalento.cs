using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TalentosIT.Models;

namespace TalentosIT.Models
{
    [Table("PerfilTalento")]
    public class PerfilTalento
    {
        [Key] public int Cod { get; set; }

        public string Nome { get; set; }

        public string Pais { get; set; }

        public string Email { get; set; }

        public int PrecoHora { get; set; }

        public string Tipo { get; set; }

        [ForeignKey("IdUtilizador")] public int IdUtilizador { get; set; }

        [NotMapped] [ValidateNever] public Utilizador Utilizador { get; set; }

        public virtual ICollection<TalentoSkill> TalentoSkills { get; set; }
        
        public virtual ICollection<DetalheExperiencia> Experiencias { get; set; }
        
        public virtual ICollection<PropostaTrabalho> PropostasTrabalho { get; set; }

    }
}
