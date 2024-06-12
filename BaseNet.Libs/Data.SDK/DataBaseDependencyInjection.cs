using System.Reflection;
using BaseNet.Libs.Data.SDK.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BaseNet.Libs.Data.SDK
{
    public static class DataBaseDependencyInjection
    {
        public static IServiceCollection AddPostgresSqlDb<TContext>(this IServiceCollection services)
        where TContext : DbContext
        {
            var connectionString = GetConnectionString();

            services.AddDbContext<TContext>(options =>
                options.UseNpgsql(connectionString,
                    b => b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            return services;
        }

        public static IServiceCollection AddRepositories<TUnitOfWork, TUnitOfWorkImpl>(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
            where TUnitOfWork : class, UnitOfWork
            where TUnitOfWorkImpl : class, TUnitOfWork
        {
            services.AddScoped<TUnitOfWork, TUnitOfWorkImpl>();
            AutomaticallyMapRepositories(services, assemblies);
            return services;
        }

        private static string GetConnectionString()
        {
            var server = Environment.GetEnvironmentVariable("DB_SERVER") ?? throw new InvalidOperationException("Variável de ambiente DB_SERVER não encontrada");
            var port = Environment.GetEnvironmentVariable("DB_PORT") ?? throw new InvalidOperationException("Variável de ambiente DB_PORT não encontrada");
            var database = Environment.GetEnvironmentVariable("DB_DATABASE") ?? throw new InvalidOperationException("Variável de ambiente DB_DATABASE não encontrada");
            var user = Environment.GetEnvironmentVariable("DB_USER") ?? throw new InvalidOperationException("Variável de ambiente DB_USER não encontrada");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? throw new InvalidOperationException("Variável de ambiente DB_PASSWORD não encontrada");

            return $"User Id={user}; Password={password}; Host={server}; Port={port}; Database={database}";
        }

        private static void AutomaticallyMapRepositories(IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var implementacoes = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x is { IsClass: true, IsAbstract: false } &&
                            x.GetInterfaces().Any(y => y.Name.Contains("Repositorio") || y.Name.Contains("Repo")));
            foreach (var implementacao in implementacoes)
            {
                foreach (var interfaceType in implementacao.GetInterfaces())
                {
                    services.AddScoped(interfaceType, implementacao);
                }
            }
        }
    }
}