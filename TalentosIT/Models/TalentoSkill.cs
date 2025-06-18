using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TalentosIT.Models
{
    [Table("TalentoSkill")]
    [PrimaryKey(nameof(CodPerfilTalento), nameof(CodSkill))]
    public class TalentoSkill
    {
        [Required]
        public int CodPerfilTalento { get; set; }

        [Required]
        public int CodSkill { get; set; }

        [Required]
        public int AnosDeExperiencia { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string NivelConforto { get; set; }

        [Column(TypeName = "text")]
        public string DescricaoProjetos { get; set; }

        [Required]
        public string AreaProfissional { get; set; }

        // Estas propriedades são de navegação e não devem ser validadas no POST
        [ValidateNever]
        [ForeignKey(nameof(CodPerfilTalento))]
        public PerfilTalento PerfilTalento { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(CodSkill))]
        public Skill Skill { get; set; }
    }
}