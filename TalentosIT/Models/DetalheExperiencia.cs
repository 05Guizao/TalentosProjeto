using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TalentosIT.Models
{
    [Table("DetalheExperiencia")]
    public class DetalheExperiencia
    {
        [Key]
        [Column("CodExperienciaTalento")]
        public int Cod { get; set; }

        [Required(ErrorMessage = "Título é obrigatório")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nome da empresa é obrigatório")]
        public string NomeEmpresa { get; set; } = string.Empty;

        [Range(1900, 2025, ErrorMessage = "Ano de início inválido")]
        public int AnoComeco { get; set; }

        [Range(1900, 2025, ErrorMessage = "Ano de término inválido")]
        public int AnoTermino { get; set; }

        [Column("CodPerfilTalento")]
        public int CodPerfilTalento { get; set; }

        [ForeignKey(nameof(CodPerfilTalento))]
        [ValidateNever] // ← EVITA A VALIDAÇÃO
        public PerfilTalento? PerfilTalento { get; set; }
    }
}