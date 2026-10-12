using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DockIn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Localizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Armazens_ArmazemId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ArmazemId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Localizacao",
                table: "Armazens");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "PerfilUser");

            migrationBuilder.RenameColumn(
                name: "AtivoFlag",
                table: "Users",
                newName: "Ativo");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Armazens",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Armazens",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Armazens",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Localizacoes",
                columns: table => new
                {
                    LocalizacaoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArmazemId = table.Column<int>(type: "integer", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Tipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacoes", x => x.LocalizacaoId);
                    table.ForeignKey(
                        name: "FK_Localizacoes_Armazens_ArmazemId",
                        column: x => x.ArmazemId,
                        principalTable: "Armazens",
                        principalColumn: "ArmazemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Localizacoes_ArmazemId_Codigo",
                table: "Localizacoes",
                columns: new[] { "ArmazemId", "Codigo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Localizacoes");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Armazens");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Armazens");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Armazens");

            migrationBuilder.RenameColumn(
                name: "PerfilUser",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "Users",
                newName: "AtivoFlag");

            migrationBuilder.AddColumn<int>(
                name: "Localizacao",
                table: "Armazens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
    }
}
