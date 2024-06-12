using System.Reflection;
using BaseNet.App.Jobs;
using BaseNet.Infra.Configs;
using BaseNet.Libs.Controller.SDK.Behaviors;
using BaseNet.Libs.Job.SDK;

var builder = WebApplication.CreateBuilder(args);

var currentAssembly = Assembly.GetAssembly(typeof(Program))!;

var config = new Config
{
    CurrentAssembly = currentAssembly,
    Assemblies = currentAssembly
        .GetReferencedAssemblies()
        .Where(e => e.FullName.StartsWith("BaseNet"))
        .Select(Assembly.Load)
        .Union(new[] { currentAssembly })
        .ToList(),
    OpenBehaviors = new List<Type> { typeof(ValidationBehavior<,>) }
};

builder.Services.AddSingleton<IConfig>(config);
builder.Services.AddLibs(config);
builder.Services.AddControllers();
builder.Services.AddJobsConfiguration();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaseNet API V1");
});
app.UseHangfireConfiguration("/api/jobs/dashboard");

using (var scope = app.Services.CreateScope())
{
    var scopeFactory = scope.ServiceProvider.GetRequiredService<IServiceScopeFactory>();
    app.UseJobsConfiguration(scopeFactory);
}

app.UseAuthorization();
app.MapControllers();

app.Run();