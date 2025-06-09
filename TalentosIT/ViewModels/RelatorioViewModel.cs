using System.Collections.Generic;

namespace TalentosIT.ViewModels
{
    public class RelatorioViewModel
    {
        public List<RelatorioCategoriaPaisViewModel> PorCategoriaPais { get; set; }
        public List<RelatorioSkillViewModel> PorSkill { get; set; }
    }
}