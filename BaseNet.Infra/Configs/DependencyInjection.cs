using System.IO.Abstractions;
using BaseNet.Infra.Contexts;
using BaseNet.Infra.Repositories;
using BaseNet.Libs.Data.SDK;
using BaseNet.Libs.Job.SDK;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BaseNet.Infra.Configs
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLibs(this IServiceCollection services, Config config)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(config.Assemblies.ToArray());
                foreach (var openBehavior in config.OpenBehaviors.AsParallel())
                    cfg.AddOpenBehavior(openBehavior);
            });

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Informe o token no formato: Bearer {your token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BaseNet API", Version = "v1" });
            });

            services.AddAutoMapper(config.Assemblies);
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddFluentValidation(config.Assemblies);
            services.AddPostgresSqlDb<ApplicationDbContext>();
            services.AddRepositories<IAppUnitOfWork, AppUnitOfWork>(config.Assemblies);
            services.AddHangfireConfiguration(config.CurrentAssembly!, "public", "base-net");

            return services;
        }
    }
}