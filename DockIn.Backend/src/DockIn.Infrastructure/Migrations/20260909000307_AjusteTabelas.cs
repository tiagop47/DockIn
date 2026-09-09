using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DockIn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClasseArtigo",
                table: "Artigos",
                newName: "ArtigoClasses");

            migrationBuilder.AddColumn<int>(
                name: "ArmazemId",
                table: "Stocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CapacidadeMaxima",
                table: "Stocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QUANTIDADE_MAX",
                table: "Artigos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Armazens",
                columns: table => new
                {
                    ArmazemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Localizacao = table.Column<int>(type: "integer", nullable: false),
                    CapacidadeMaxima = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armazens", x => x.ArmazemId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ArmazemId",
                table: "Stocks",
                column: "ArmazemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Armazens_ArmazemId",
                table: "Stocks",
                column: "ArmazemId",
                principalTable: "Armazens",
                principalColumn: "ArmazemId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Armazens_ArmazemId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "Armazens");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ArmazemId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ArmazemId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "CapacidadeMaxima",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "QUANTIDADE_MAX",
                table: "Artigos");

            migrationBuilder.RenameColumn(
                name: "ArtigoClasses",
                table: "Artigos",
                newName: "ClasseArtigo");
        }
    }
}
