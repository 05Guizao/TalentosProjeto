using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentosIT.Migrations
{
    public partial class AddOnlyDataCriacaoToProposta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "PropostaTrabalho",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "PropostaTrabalho");
        }
    }
}
