using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TalentosIT.Models
{
    [Table("TalentoSkill")]
    [PrimaryKey(nameof(CodPerfilTalento), nameof(CodSkill))]
    public class TalentoSkill
    {
        public int CodPerfilTalento { get; set; }
        public int CodSkill { get; set; }

        public int AnosDeExperiencia { get; set; }

        [Column(TypeName = "text")]
        public string NivelConforto { get; set; }  // "Baixo", "Médio" ou "Alto"

        [Column(TypeName = "text")]
        public string DescricaoProjetos { get; set; }

        [ForeignKey(nameof(CodPerfilTalento))]
        public PerfilTalento PerfilTalento { get; set; }

        [ForeignKey(nameof(CodSkill))]
        public Skill Skill { get; set; }
    }
}