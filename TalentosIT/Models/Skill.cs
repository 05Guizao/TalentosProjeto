using System.ComponentModel.DataAnnotations;

public class Skill
{
    [Key]
    public int Cod { get; set; }
    
    public string Nome { get; set; }
    
    public string AreaProfissional { get; set; }
    
    public string Estado { get; set; }
}