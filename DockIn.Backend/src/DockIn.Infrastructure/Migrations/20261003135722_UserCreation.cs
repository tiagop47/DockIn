using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DockIn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacidadeMaxima",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ArtigoClasses",
                table: "Artigos");

            migrationBuilder.RenameColumn(
                name: "QUANTIDADE_MAX",
                table: "Artigos",
                newName: "UnidadeMedida");

            migrationBuilder.RenameColumn(
                name: "ExigeValidade",
                table: "Artigos",
                newName: "AtivoBool");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Stocks",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    AtivoFlag = table.Column<bool>(type: "boolean", nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.RenameColumn(
                name: "UnidadeMedida",
                table: "Artigos",
                newName: "QUANTIDADE_MAX");

            migrationBuilder.RenameColumn(
                name: "AtivoBool",
                table: "Artigos",
                newName: "ExigeValidade");

            migrationBuilder.AlterColumn<double>(
                name: "Preco",
                table: "Stocks",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<int>(
                name: "CapacidadeMaxima",
                table: "Stocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ArtigoClasses",
                table: "Artigos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
