using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioSanitariaAPI.Migrations
{
    public partial class ModifyPacientesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sexe",
                table: "Pacientes",
                newName: "Genere");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genere",
                table: "Pacientes",
                newName: "Sexe");
        }
    }
}
