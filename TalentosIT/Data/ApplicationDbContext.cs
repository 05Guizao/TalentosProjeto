using Microsoft.EntityFrameworkCore;
using TalentosIT.Models;

namespace TalentosIT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<PerfilTalento> PerfilTalentos { get; set; }
        public DbSet<PropostaSkill> PropostaSkills { get; set; }
        public DbSet<PropostaTrabalho> PropostaTrabalhos { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<TalentoSkill> TalentoSkills { get; set; }
        public DbSet<DetalheExperiencia> DetalheExperiencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais do modelo podem ser adicionadas aqui
        }
    }
}