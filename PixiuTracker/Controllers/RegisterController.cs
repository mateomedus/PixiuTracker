using AutoMapper;
using DatabaseContext;
using DatabaseContext.Models;
using Microsoft.AspNetCore.Mvc;
using PixiuTracker.Forms;
using System.Threading.Tasks;

namespace PixiuTracker.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private Context context;
        private IMapper map;

        // Le decimos al framework que vamos a necesitar estos services y se encarga de generarnos los objetos (hace el new) y pasarlos por Inyeccion de Dependencia
        // Link: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0
        public UserController(Context DbContext, IMapper map)
        {
            context = DbContext;
            this.map = map;
        }

        // POST /api/user/register 
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserForm ru)
        {
            //El mismo framework se asegura que te haya llegado todo lo que hayas puesto como required

            // Pero si es necesario, hay que aplicar logica sobre las inputs 
            // Por ejemplo acá podrían checkear que efectivamente el email sea un email o cosas asi. Ya que, si bien el framework se asegura que te llegue algo en email, no te puede asegura que carajo te mandaron.

            // Las inputs estan ok? Mapear a user
            var binanceUser = map.Map<BinanceUser>(ru);
            //si es necesario hacer lógica antes o despues del mapeo se hace acá. Link: https://docs.automapper.org/en/stable/Before-and-after-map-actions.html

            //Guardar en DB
            context.Add(binanceUser);
            await context.SaveChangesAsync(); // --> importante esto

            return new CreatedResult("", null);
        }
        
        /*// POST /api/user/login 
        [HttpPost("/login")]
        public IActionResult Login()
        {
            return Quique.Gay;
        }*/

    }
}
