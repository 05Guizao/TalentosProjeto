using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentosIT.Models
{
    [Table("DetalheExperiencia")]
    public class DetalheExperiencia
    {
        [Key]
        [Column("CodExperienciaTalento")]
        public int Cod { get; set; }

        public string Titulo { get; set; }

        public string NomeEmpresa { get; set; }
        
        [Range(1900, 2025)]
        public int AnoComeco { get; set; }
        
        [Range(1900, 2025)]
        public int AnoTermino { get; set; }

        [Column("CodPerfilTalento")]
        public int CodPerfilTalento { get; set; }

        [ForeignKey(nameof(CodPerfilTalento))]
        public PerfilTalento PerfilTalento { get; set; }
    }
}