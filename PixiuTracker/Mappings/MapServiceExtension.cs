using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Mappings
{
    public static class MappServiceExtension
    {
        // Esto se hace para informar al framework de un nuevo servicio disponible para ser inyectado por dependecia en donde se lo necesite (en este caso los controllers)
        public static void AddMapperServices(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserMapping());
            });

            services.AddSingleton(mappingConfig.CreateMapper());
        }
    }
}
