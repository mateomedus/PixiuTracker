using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseContext.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BinanceUser",
                columns: table => new
                {
                    Username = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    LastName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Password = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ApiKey = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ApiSecret = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinanceUser", x => x.Username);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BinanceUser_Email",
                table: "BinanceUser",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BinanceUser_Username_Password",
                table: "BinanceUser",
                columns: new[] { "Username", "Password" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinanceUser");
        }
    }
}
