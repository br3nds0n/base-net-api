using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Hangfire;
using Hangfire.JobsLogger;
using Hangfire.PostgreSql;
using Hangfire.RecurringJobAdmin;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BaseNet.Libs.Job.SDK
{
    public static class HangfireConfig
    {
        public static IServiceCollection AddHangfireConfiguration(this IServiceCollection services, Assembly assembly, string schema, string servidor)
        {
            var serverOptions = new Action<BackgroundJobServerOptions>(config =>
            {
                config.ServerName = string.Format(
                    "{0}.{1}",
                    servidor,
                    System.Guid.NewGuid().ToString());
            });

            services.AddHangfire(config =>
            {
                config.UsePostgreSqlStorage(GetConnectionString())
                .UseRecurringJobAdmin(assembly)
                .UseJobsLogger();
            }).AddHangfireServer(serverOptions);

            return services;
        }

        public static IApplicationBuilder UseHangfireConfiguration(this IApplicationBuilder app, string url)
        {
            app.UseHangfireDashboard(url, new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });
            return app;
        }

        private static string GetConnectionString()
        {
            var server = Environment.GetEnvironmentVariable("JOBS_DB_SERVER");
            var port = Environment.GetEnvironmentVariable("JOBS_DB_PORT");
            var database = Environment.GetEnvironmentVariable("JOBS_DB_DATABASE");
            var user = Environment.GetEnvironmentVariable("JOBS_DB_USER");
            var password = Environment.GetEnvironmentVariable("JOBS_DB_PASSWORD");

            if (string.IsNullOrEmpty(server))
                throw new Exception("Variável de ambiente JOBS_DB_SERVER não encontrada");
            if (string.IsNullOrEmpty(port))
                throw new Exception("Variável de ambiente JOBS_DB_PORT não encontrada");
            if (string.IsNullOrEmpty(database))
                throw new Exception("Variável de ambiente JOBS_DB_DATABASE não encontrada");
            if (string.IsNullOrEmpty(user))
                throw new Exception("Variável de ambiente JOBS_DB_USER não encontrada");
            if (string.IsNullOrEmpty(password))
                throw new Exception("Variável de ambiente JOBS_DB_PASSWORD não encontrada");

            return $"User Id={user}; Password={password}; Host={server}; Port={port}; Database={database}";
        }
    }
}