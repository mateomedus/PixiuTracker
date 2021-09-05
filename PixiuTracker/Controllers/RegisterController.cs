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

            if (result.Error == null)
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

        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                var user = context.Users.SingleOrDefault(u => u.Id == userId);

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }

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

        [HttpGet("prices")]
        public async Task<IActionResult> Test() 
        {
            var client = CustomBinanceClient.GetInstance("azmnlAv1bBa5mpk6XMkwSPcQEEFuMUwrlXRtD6ownafLPjRObaWCHqAyWDEaSVgb", "uZ4pAe8ihACDZbgjs2Z5mVmRHItZBckyv6bEA4HbWXPK1wrDOP8wv8OFvE06mPm9");

            var prices = (await client.Spot.Market.GetPricesAsync());

            var asd = prices.Data.OrderBy(p => p.Symbol).Where(p => p.Symbol.EndsWith("USDT")).ToList();


            /*
            */

            return new OkResult();
        }
              
    }
}
