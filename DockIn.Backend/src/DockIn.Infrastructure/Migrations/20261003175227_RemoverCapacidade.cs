using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DockIn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoverCapacidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacidadeMaxima",
                table: "Armazens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapacidadeMaxima",
                table: "Armazens",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
