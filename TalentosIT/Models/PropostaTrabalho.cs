using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentosIT.Models
{
    [Table("PropostaTrabalho")]
    public class PropostaTrabalho
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string CategoriaTalento { get; set; }

        [Required]
        [Range(1, 10000)]
        public int NumTotalHoras { get; set; }

        [Required]
        public string DescricaoTrabalho { get; set; }

        public string Estado { get; set; } = "Sem Resposta";

        [ForeignKey("Utilizador")]
        public int IdUtilizador { get; set; }

        [Column("IdPerfilTalento")]
        [ForeignKey("PerfilTalento")]
        public int IdPerfilTalento { get; set; }

        public PerfilTalento? PerfilTalento { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}