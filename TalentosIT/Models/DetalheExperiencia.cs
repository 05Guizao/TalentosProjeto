using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TalentosIT.Models;

namespace TalentosIT.Models
{
    [Table("DetalheExperiencia")]
    public class DetalheExperiencia
    {
        [Key]
        public int CodExperienciaTalento { get; set; }

        public string Titulo { get; set; }

        public string NomeEmpresa { get; set; }

        public int AnoComeco { get; set; }

        public int AnoTermino { get; set; }

        [ForeignKey("CodExperienciaTalento")]
        public PerfilTalento PerfilTalento { get; set; }
    }
}