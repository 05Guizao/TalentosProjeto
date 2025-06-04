using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentosIT.Models
{
    [Table("Skill")]
    public class Skill
    {
        [Key]
        public int Cod { get; set; }

        [Required(ErrorMessage = "O nome da skill é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O estado da skill é obrigatório")]
        public string Estado { get; set; }
    }

}
