using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TalentosIT.Models
{
    [Table("PerfilTalento")]
    public class PerfilTalento
    {
        [Key]
        public int Cod { get; set; }

        public string Nome { get; set; }

        public string Pais { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Preço por hora é obrigatório")]
        [Range(0, 9999.99, ErrorMessage = "Preço/hora inválido")]
        public decimal PrecoHora { get; set; }

        public string Tipo { get; set; }

        [ForeignKey("IdUtilizador")]
        public int IdUtilizador { get; set; }

        [NotMapped]
        [ValidateNever]
        public Utilizador Utilizador { get; set; }

        [NotMapped]
        public decimal PrecoMensal => PrecoHora * 160;

        [ValidateNever]
        public virtual ICollection<TalentoSkill>? TalentoSkills { get; set; } = new List<TalentoSkill>();

        [ValidateNever]
        public virtual ICollection<DetalheExperiencia>? Experiencias { get; set; } = new List<DetalheExperiencia>();

        [ValidateNever]
        public virtual ICollection<PropostaTrabalho>? PropostasTrabalho { get; set; } = new List<PropostaTrabalho>();
    }
}