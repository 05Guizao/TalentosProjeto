using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TalentoSkill
{
    [Key]
    [Column(Order = 1)]
    public int CodPerfilTalento { get; set; }
    
    [Key]
    [Column(Order = 2)]
    public int CodSkill { get; set; }
    
    public int AnosDeExperiencia { get; set; }
    
    [ForeignKey("CodPerfilTalento")]
    public PerfilTalento PerfilTalento { get; set; }
    
    [ForeignKey("CodSkill")]
    public Skill Skill { get; set; }
}