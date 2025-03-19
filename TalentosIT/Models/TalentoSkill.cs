using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
[Table("TalentoSkill")]
[PrimaryKey(nameof(CodPerfilTalento), nameof(CodSkill))]
public class TalentoSkill
{
    public int CodPerfilTalento { get; set; }
    public int CodSkill { get; set; }
    public int AnosDeExperiencia { get; set; }

    [ForeignKey(nameof(CodPerfilTalento))]
    public PerfilTalento PerfilTalento { get; set; }

    [ForeignKey(nameof(CodSkill))]
    public Skill Skill { get; set; }
}
