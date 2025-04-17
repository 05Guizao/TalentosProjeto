using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentosIT.Models
{
    [Table("DetalheExperiencia")]
    public class DetalheExperiencia
    {
        [Key]
        public int Cod { get; set; }

        public string Titulo { get; set; }

        public string NomeEmpresa { get; set; }

        public int AnoComeco { get; set; }

        public int AnoTermino { get; set; }

        [ForeignKey("PerfilTalento")]
        public int CodPerfilTalento { get; set; }
        public PerfilTalento PerfilTalento { get; set; }
    }
}