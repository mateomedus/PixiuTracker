using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DatabaseContext.Migrations
{
    public partial class Newmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BinanceUser_Portfolio_PorfolioID",
                table: "BinanceUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coin",
                table: "Coin");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Portfolio");

            migrationBuilder.DropColumn(
                name: "CoinID",
                table: "Portfolio");

            migrationBuilder.RenameColumn(
                name: "PorfolioID",
                table: "BinanceUser",
                newName: "PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_BinanceUser_PorfolioID",
                table: "BinanceUser",
                newName: "IX_BinanceUser_PortfolioId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coin",
                type: "character varying(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Coin",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coin",
                table: "Coin",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PortfolioCoin",
                columns: table => new
                {
                    PortfolioId = table.Column<int>(type: "integer", nullable: false),
                    CoinId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioCoin", x => new { x.PortfolioId, x.CoinId });
                    table.ForeignKey(
                        name: "FK_PortfolioCoin_Coin_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioCoin_Portfolio_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioCoin_CoinId",
                table: "PortfolioCoin",
                column: "CoinId");

            migrationBuilder.AddForeignKey(
                name: "FK_BinanceUser_Portfolio_PortfolioId",
                table: "BinanceUser",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BinanceUser_Portfolio_PortfolioId",
                table: "BinanceUser");

            migrationBuilder.DropTable(
                name: "PortfolioCoin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coin",
                table: "Coin");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Coin");

            migrationBuilder.RenameColumn(
                name: "PortfolioId",
                table: "BinanceUser",
                newName: "PorfolioID");

            migrationBuilder.RenameIndex(
                name: "IX_BinanceUser_PortfolioId",
                table: "BinanceUser",
                newName: "IX_BinanceUser_PorfolioID");

            migrationBuilder.AddColumn<long>(
                name: "Amount",
                table: "Portfolio",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "CoinID",
                table: "Portfolio",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coin",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coin",
                table: "Coin",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_BinanceUser_Portfolio_PorfolioID",
                table: "BinanceUser",
                column: "PorfolioID",
                principalTable: "Portfolio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
