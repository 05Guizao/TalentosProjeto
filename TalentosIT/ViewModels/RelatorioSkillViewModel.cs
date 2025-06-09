namespace TalentosIT.ViewModels
{
    public class RelatorioSkillViewModel
    {
        public string Skill { get; set; }
        public double PrecoMedio { get; set; }

        public double PrecoMensalEstimado => Math.Round(PrecoMedio * 176, 2);
    }
}