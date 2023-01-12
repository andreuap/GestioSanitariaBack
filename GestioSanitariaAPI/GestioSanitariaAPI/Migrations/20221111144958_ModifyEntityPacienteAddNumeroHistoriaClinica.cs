using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioSanitariaAPI.Migrations
{
    public partial class ModifyEntityPacienteAddNumeroHistoriaClinica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroHistoriaClinica",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroHistoriaClinica",
                table: "Pacientes");
        }
    }
}
