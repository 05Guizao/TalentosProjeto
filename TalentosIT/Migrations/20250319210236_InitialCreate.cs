using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TalentosIT.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Cod = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    AreaProfissional = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Cod);
                });

            migrationBuilder.CreateTable(
                name: "Utilizador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfilTalento",
                columns: table => new
                {
                    Cod = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Pais = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PrecoHora = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    IdUtilizador = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilTalento", x => x.Cod);
                    table.ForeignKey(
                        name: "FK_PerfilTalento_Utilizador_IdUtilizador",
                        column: x => x.IdUtilizador,
                        principalTable: "Utilizador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalheExperiencia",
                columns: table => new
                {
                    CodExperienciaTalento = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    NomeEmpresa = table.Column<string>(type: "text", nullable: false),
                    AnoComeco = table.Column<int>(type: "integer", nullable: false),
                    AnoTermino = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalheExperiencia", x => x.CodExperienciaTalento);
                    table.ForeignKey(
                        name: "FK_DetalheExperiencia_PerfilTalento_CodExperienciaTalento",
                        column: x => x.CodExperienciaTalento,
                        principalTable: "PerfilTalento",
                        principalColumn: "Cod",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropostaTrabalho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    CategoriaTalento = table.Column<string>(type: "text", nullable: false),
                    NumTotalHoras = table.Column<int>(type: "integer", nullable: false),
                    DescricaoTrabalho = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    IdUtilizador = table.Column<int>(type: "integer", nullable: false),
                    CodPerfilTalento = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropostaTrabalho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropostaTrabalho_PerfilTalento_CodPerfilTalento",
                        column: x => x.CodPerfilTalento,
                        principalTable: "PerfilTalento",
                        principalColumn: "Cod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropostaTrabalho_Utilizador_IdUtilizador",
                        column: x => x.IdUtilizador,
                        principalTable: "Utilizador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TalentoSkill",
                columns: table => new
                {
                    CodPerfilTalento = table.Column<int>(type: "integer", nullable: false),
                    CodSkill = table.Column<int>(type: "integer", nullable: false),
                    AnosDeExperiencia = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalentoSkill", x => new { x.CodPerfilTalento, x.CodSkill });
                    table.ForeignKey(
                        name: "FK_TalentoSkill_PerfilTalento_CodPerfilTalento",
                        column: x => x.CodPerfilTalento,
                        principalTable: "PerfilTalento",
                        principalColumn: "Cod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TalentoSkill_Skill_CodSkill",
                        column: x => x.CodSkill,
                        principalTable: "Skill",
                        principalColumn: "Cod",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropostaSkill",
                columns: table => new
                {
                    IdPropostaTrabalho = table.Column<int>(type: "integer", nullable: false),
                    CodSkill = table.Column<int>(type: "integer", nullable: false),
                    MinAnosExperiencia = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropostaSkill", x => new { x.IdPropostaTrabalho, x.CodSkill });
                    table.ForeignKey(
                        name: "FK_PropostaSkill_PropostaTrabalho_IdPropostaTrabalho",
                        column: x => x.IdPropostaTrabalho,
                        principalTable: "PropostaTrabalho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropostaSkill_Skill_CodSkill",
                        column: x => x.CodSkill,
                        principalTable: "Skill",
                        principalColumn: "Cod",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerfilTalento_IdUtilizador",
                table: "PerfilTalento",
                column: "IdUtilizador");

            migrationBuilder.CreateIndex(
                name: "IX_PropostaSkill_CodSkill",
                table: "PropostaSkill",
                column: "CodSkill");

            migrationBuilder.CreateIndex(
                name: "IX_PropostaTrabalho_CodPerfilTalento",
                table: "PropostaTrabalho",
                column: "CodPerfilTalento");

            migrationBuilder.CreateIndex(
                name: "IX_PropostaTrabalho_IdUtilizador",
                table: "PropostaTrabalho",
                column: "IdUtilizador");

            migrationBuilder.CreateIndex(
                name: "IX_TalentoSkill_CodSkill",
                table: "TalentoSkill",
                column: "CodSkill");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalheExperiencia");

            migrationBuilder.DropTable(
                name: "PropostaSkill");

            migrationBuilder.DropTable(
                name: "TalentoSkill");

            migrationBuilder.DropTable(
                name: "PropostaTrabalho");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "PerfilTalento");

            migrationBuilder.DropTable(
                name: "Utilizador");
        }
    }
}
