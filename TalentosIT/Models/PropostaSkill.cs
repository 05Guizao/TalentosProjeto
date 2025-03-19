using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PropostaSkill
{
    [Key]
    [Column(Order = 1)]
    public int IdPropostaTrabalho { get; set; }
    
    [Key]
    [Column(Order = 2)]
    public int CodSkill { get; set; }
    
    public int? MinAnosExperiencia { get; set; }
    
    [ForeignKey("CodSkill")]
    public Skill Skill { get; set; }
    
    [ForeignKey("IdPropostaTrabalho")]
    public PropostaTrabalho PropostaTrabalho { get; set; }
}