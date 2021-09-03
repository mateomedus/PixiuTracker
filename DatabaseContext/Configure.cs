using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseContext
{
    public static class Configure
    {
        // Esto se hace para informar al framework de un nuevo servicio disponible para ser inyectado por dependecia en donde se lo necesite (en este caso los controllers)
        public static void AddDatabaseAccess(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<Context>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("DatabaseContext")).EnableSensitiveDataLogging());
        }
    }
}
