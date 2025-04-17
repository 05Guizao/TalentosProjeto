namespace TalentosIT.Models
{
    public class SkillAtiva : SkillBase
    {
        public override string Estado { get; set; } = "Ativo";

        public override bool EhValida()
        {
            return true;
        }
    }
}