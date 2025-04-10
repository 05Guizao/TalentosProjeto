using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TalentosIT.Models;

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

        public int PrecoHora { get; set; }

        public string Tipo { get; set; }

        [ForeignKey("Utilizador")]
        public int IdUtilizador { get; set; }

        public Utilizador Utilizador { get; set; }
    }
}