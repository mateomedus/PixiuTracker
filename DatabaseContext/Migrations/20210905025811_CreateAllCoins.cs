using ExternalLibrary;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq;

namespace DatabaseContext.Migrations
{
    public partial class CreateAllCoins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var client = CustomBinanceClient.GetInstance();

            var prices = client.Spot.Market.GetPricesAsync().Result;

            var usdtPrices = prices.Data.OrderBy(p => p.Symbol).Where(p => p.Symbol.EndsWith("USDT")).ToList();

            string coinWithoutUSDT;

            foreach (var coin in usdtPrices)
            {
                coinWithoutUSDT = coin.Symbol.Replace("USDT", string.Empty);

                migrationBuilder.Sql(
                    @"INSERT INTO public.""Coin""
                        (""Name"", ""Price"")
                        VALUES('" + coinWithoutUSDT + "', " + coin.Price + ") ON CONFLICT DO NOTHING;");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
