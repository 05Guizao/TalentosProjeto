namespace TalentosIT.Models
{
    public class SkillInativa : SkillBase
    {
        public override string Estado { get; set; } = "Inativo";

        public override bool EhValida()
        {
            return false;
        }
    }
}