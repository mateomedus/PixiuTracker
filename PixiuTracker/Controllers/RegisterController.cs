using AutoMapper;
using DatabaseContext;
using DatabaseContext.Models;
using Microsoft.AspNetCore.Mvc;
using PixiuTracker.Forms;
using PixiuTracker.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

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

            // Las inputs estan ok? Mapear a user
            var binanceUser = map.Map<BinanceUser>(registerForm);
            //si es necesario hacer lógica antes o despues del mapeo se hace acá. Link: https://docs.automapper.org/en/stable/Before-and-after-map-actions.html

            //Guardar en DB
            context.Add(binanceUser);
            await context.SaveChangesAsync(); // --> importante esto

            return new CreatedResult("", null);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserForm loginForm)
        {
            //El mismo framework se asegura que te haya llegado todo lo que hayas puesto como required

            // Pero si es necesario, hay que aplicar logica sobre las inputs 
            // Por ejemplo acá podrían checkear que efectivamente el email sea un email o cosas asi. Ya que, si bien el framework se asegura que te llegue algo en email, no te puede asegura que carajo te mandaron.

            // Las inputs estan ok? Mapear a user
            //var binanceUser = map.Map<BinanceUser>(loginForm);
            //si es necesario hacer lógica antes o despues del mapeo se hace acá. Link: https://docs.automapper.org/en/stable/Before-and-after-map-actions.html

            var user = context.Users.SingleOrDefault(u => u.Email == loginForm.Email);

            if (user == null) return BadRequest(new { message = "Invalid Credentials" });
            else
                
               if (!Crypto.VerifyHashedPassword(user.Password, loginForm.Password))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var jwt = jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true
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
            catch (Exception )
            {
                return Unauthorized();
            }

        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new
            {
                message = "success"
            }
            );

        }



        /*/ POST /api/user/login 
        [HttpPost("/login")]
        public IActionResult Login()
        {
            return Quique.Gay;
        }
        */
    }
}
