using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DatabaseContext.Migrations
{
    public partial class Includeforeingkeybinanceuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PorfolioID",
                table: "BinanceUser",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_BinanceUser_PorfolioID",
                table: "BinanceUser",
                column: "PorfolioID");

            migrationBuilder.AddForeignKey(
                name: "FK_BinanceUser_Portfolio_PorfolioID",
                table: "BinanceUser",
                column: "PorfolioID",
                principalTable: "Portfolio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BinanceUser_Portfolio_PorfolioID",
                table: "BinanceUser");

            migrationBuilder.DropIndex(
                name: "IX_BinanceUser_PorfolioID",
                table: "BinanceUser");

            migrationBuilder.AlterColumn<int>(
                name: "PorfolioID",
                table: "BinanceUser",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}
