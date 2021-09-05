using AutoMapper;
using DatabaseContext.Models;
using PixiuTracker.Forms;
using System.Web.Helpers;

namespace Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            // Mapear de RegisterUserForm a BinanceUser. Se mapea todas las props que matcheen con el nombre.
            // Si hay diferente nombre o hay que hacer lógica extra (hashear la pass) se la define acá
            CreateMap<RegisterUserForm, BinanceUser>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => Crypto.HashPassword(src.Password)));
            
        }
    }
}
