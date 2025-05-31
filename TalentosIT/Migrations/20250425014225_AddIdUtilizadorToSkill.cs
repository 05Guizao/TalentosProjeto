using Microsoft.EntityFrameworkCore.Migrations;

namespace TalentosIT.Migrations
{
    public partial class AddIdUtilizadorToSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) Adiciona a coluna como nullable (não impõe default 0)
            migrationBuilder.AddColumn<int>(
                name: "IdUtilizador",
                table: "Skill",
                type: "integer",
                nullable: true);

            // 2) (Opcional) Se quiseres atribuir um utilizador padrão às linhas existentes,
            //    podes descomentar e ajustar o SQL abaixo:
            // migrationBuilder.Sql(
            //     "UPDATE \"Skill\" SET \"IdUtilizador\" = 1 WHERE \"IdUtilizador\" IS NULL");

            // 3) Cria o índice sobre a nova coluna
            migrationBuilder.CreateIndex(
                name: "IX_Skill_IdUtilizador",
                table: "Skill",
                column: "IdUtilizador");

            // 4) Adiciona a FK, mas como a coluna é nullable, não haverá violação
            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Utilizador_IdUtilizador",
                table: "Skill",
                column: "IdUtilizador",
                principalTable: "Utilizador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Utilizador_IdUtilizador",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Skill_IdUtilizador",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "IdUtilizador",
                table: "Skill");
        }
    }
}