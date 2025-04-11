using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TalentosIT.Models;

namespace TalentosIT.Models
{
    [Table("PropostaSkill")]
    [PrimaryKey(nameof(IdPropostaTrabalho), nameof(CodSkill))]
    public class PropostaSkill
    {
        public int IdPropostaTrabalho { get; set; }
        public int CodSkill { get; set; }
        public int? MinAnosExperiencia { get; set; }

        [ForeignKey(nameof(CodSkill))]
        public Skill Skill { get; set; }

        [ForeignKey(nameof(IdPropostaTrabalho))]
        public PropostaTrabalho PropostaTrabalho { get; set; }
    }
}