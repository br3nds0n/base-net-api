using BaseNet.App.Jobs.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BaseNet.App.Jobs
{
    public static class JobsConfig
    {
        public static IApplicationBuilder UseJobsConfiguration(this IApplicationBuilder app, IServiceScopeFactory scopeFactory)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var testJob = scope.ServiceProvider.GetRequiredService<TestJob>();
                testJob.Init();
            }
            return app;
        }

        public static IServiceCollection AddJobsConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<TestJob, TestJobImpl>();
            return services;
        }
    }
}