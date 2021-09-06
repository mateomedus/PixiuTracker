using AutoMapper;
using DatabaseContext;
using DatabaseContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PixiuTracker.Forms;
using PixiuTracker.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using ExternalLibrary;
using System.Text.Json;
using System.Collections.Generic;
using PixiuTracker.Forms.Out;

namespace PixiuTracker.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private Context context;
        private IMapper map;
        private readonly JwtService jwtService;

        // Le decimos al framework que vamos a necesitar estos services y se encarga de generarnos los objetos (hace el new) y pasarlos por Inyeccion de Dependencia
        // Link: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0
        public UserController(Context DbContext, IMapper map, JwtService jwtService)
        {
            context = DbContext;
            this.map = map;
            this.jwtService = jwtService;
        }

        // POST /api/user/register 
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserForm registerForm)
        {
            //El mismo framework se asegura que te haya llegado todo lo que hayas puesto como required

            // Pero si es necesario, hay que aplicar logica sobre las inputs 
            // Por ejemplo acá podrían checkear que efectivamente el email sea un email o cosas asi. Ya que, si bien el framework se asegura que te llegue algo en email, no te puede asegura que carajo te mandaron.

            var binanceClient = CustomBinanceClient.GetInstance(registerForm.ApiKey, registerForm.ApiSecret);            

            var result = await binanceClient.General.GetAccountInfoAsync();

            if (result.Error != null)
            {
                return new ConflictResult();
            }

            // Las inputs estan ok? Mapear a user
            // en este punto podemos conectar con binance
            var binanceUser = map.Map<BinanceUser>(registerForm, opt =>
            {
                opt.AfterMap((src, dest) => dest.Portfolio = new Portfolio());
            });

            //si es necesario hacer lógica antes o despues del mapeo se hace acá. Link: https://docs.automapper.org/en/stable/Before-and-after-map-actions.html

            //Guardar en DB
            context.Add(binanceUser);
            await context.SaveChangesAsync(); // --> importante esto

            return new CreatedResult("", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserForm loginForm)
        {

            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == loginForm.Email);


            if (user == null) return BadRequest(new { message = "Invalid Credentials" });
            else
            {
                if (!Crypto.VerifyHashedPassword(user.Password, loginForm.Password))
                {
                    return BadRequest(new { message = "Invalid Credentials" });
                }
            }

            var jwt = jwtService.Generate(user.Id);

            Response.Headers.Add("Access-Control-Expose-Headers", "Set-Cookie");

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                SameSite = SameSiteMode.None,
                Secure = true
            });

            return Ok("Success");
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Append("jwt", string.Empty, new CookieOptions
            {
                SameSite = SameSiteMode.None,
                Secure = true
            });
            return Ok(new
            {
                message = "success"
            }
            );

        }

        [HttpGet("portfolio")]
        public async Task<IActionResult> Portfolio() 
        {
            try
            {
                BinanceUser user = await getBinanceUser();

                var client = CustomBinanceClient.GetInstance(user.ApiKey, user.ApiSecret);

                var result = await client.General.GetAccountInfoAsync();
                                
                var portfolioId = user.PortfolioId;

                if (result.Error == null)
                {
                    var balance = result.Data.Balances.ToList();
                    foreach (var b in balance)
                    {
                        await CreateOrUpdateCoin(portfolioId, b);
                    }
                    await context.SaveChangesAsync();
                }

                var coinsInPortfolio = await context.PortfolioCoins
                    .Include(pc => pc.Coin)
                    .Where(p => p.PortfolioId == portfolioId).ToListAsync();
                              

                return Ok(map.Map<IEnumerable<PortfolioCoinForm>>(coinsInPortfolio));

            }
            catch (Exception e)
            {
                return Unauthorized();
            }          
        }

        private async Task<BinanceUser> getBinanceUser()
        {            
            var jwt = Request.Cookies["jwt"];
            var token = jwtService.Verify(jwt);

            int userId = int.Parse(token.Issuer);
            var user = await context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            return user;      
            
        }


            private async Task CreateOrUpdateCoin(int portfolioId, Binance.Net.Objects.Spot.SpotData.BinanceBalance b)
        {
            if (b.Free > 0) {
                
                var pCoin = await context.PortfolioCoins
                    .Include(pc => pc.Coin)
                    .FirstOrDefaultAsync(pc => pc.PortfolioId == portfolioId && pc.Coin.Name == b.Asset);

                //(var portfolioCoin = context.PortfolioCoins.SingleOrDefault(p => p.PortfolioId == portfolioId && p.CoinId == coin.Id);

                if (pCoin == null)
                {
                    var coin = await context.Coins
                    .SingleOrDefaultAsync(c => c.Name == b.Asset);
                    if (coin != null)
                    {
                        var portfolioCoin = new PortfolioCoin()
                        {
                            CoinId = coin.Id,
                            Amount = b.Free,
                            PortfolioId = portfolioId
                        };
                        context.Add(portfolioCoin);
                    }              
                   
                }
                else {
                    pCoin.Amount = b.Free;
                    context.Update(pCoin);
                }
            }        
        }

        [HttpGet("general-balance")]
        public async Task<IActionResult> BalanceGeneral()
        {
            var coinsInPortfolio = await context.Coins
                    .Include(c => c.Portfolios)
                    .Where(pc => pc.Portfolios.Any(p=> p.Amount != 0))
                    .ToListAsync();

            Dictionary<string, double> dic = new Dictionary<string, double>();

            double sum;

            foreach (var c in coinsInPortfolio)
            {
                sum = 0;
                foreach (var pc in c.Portfolios)
                {
                    sum += ((double)pc.Amount * c.Price);
                }

                dic.Add(c.Name, sum);
            }

            var result = dic.Select(entry => new TotalCoinBalance() { Name = entry.Key, Value = entry.Value }).OrderByDescending(c => c.Value).ToList();

            return Ok(result);   
        }
    }
}
