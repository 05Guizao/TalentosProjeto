using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentosIT.Models
{
    [Table("PropostaTrabalho")]
    public class PropostaTrabalho
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string CategoriaTalento { get; set; }

        public int NumTotalHoras { get; set; }

        public string DescricaoTrabalho { get; set; }

        public string Estado { get; set; }

        [ForeignKey("Utilizador")]
        public int IdUtilizador { get; set; }
    }
}