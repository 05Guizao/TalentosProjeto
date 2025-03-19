using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Skill")]
public class Skill
{
    [Key]
    public int Cod { get; set; }
    
    public string Nome { get; set; }
    
    public string AreaProfissional { get; set; }
    
    public string Estado { get; set; }
}