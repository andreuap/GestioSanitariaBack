using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioSanitariaAPI.Migrations
{
    public partial class ModifyEntityPaciente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNeixament",
                table: "Pacientes",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNeixament",
                table: "Pacientes");
        }
    }
}
