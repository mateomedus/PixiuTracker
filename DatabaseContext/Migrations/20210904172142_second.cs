using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DatabaseContext.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BinanceUser",
                table: "BinanceUser");

            migrationBuilder.DropIndex(
                name: "IX_BinanceUser_Username_Password",
                table: "BinanceUser");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "BinanceUser");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BinanceUser",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BinanceUser",
                table: "BinanceUser",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BinanceUser_Username_Password",
                table: "BinanceUser",
                columns: new[] { "Id", "Password" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BinanceUser",
                table: "BinanceUser");

            migrationBuilder.DropIndex(
                name: "IX_BinanceUser_Username_Password",
                table: "BinanceUser");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BinanceUser");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "BinanceUser",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BinanceUser",
                table: "BinanceUser",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_BinanceUser_Username_Password",
                table: "BinanceUser",
                columns: new[] { "Username", "Password" },
                unique: true);
        }
    }
}
