using BaseNet.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BaseNet.Infra.Configs
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLibs(this IServiceCollection services, Configuracao configuracao)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(configuracao.Assemblies.ToArray());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BaseNet API", Version = "v1" });
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuracao.ConnectionString));

            return services;
        }
    }
}