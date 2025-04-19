using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentosIT.Models
{
    [Table("Skill")]
    public abstract class SkillBase
    {
        [Key]
        public int Cod { get; set; }

        public string Nome { get; set; }

        public string AreaProfissional { get; set; }

        public abstract string Estado { get; set; }

        public abstract bool EhValida();
    }
}