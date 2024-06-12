using System.Reflection;
using BaseNet.Infra.Configs;
using BaseNet.Libs.Controller.SDK.Behaviors;

var builder = WebApplication.CreateBuilder(args);

var currentAssembly = Assembly.GetAssembly(typeof(Program))!;

var config = new Config
{
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
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaseNet API V1");
});

app.UseAuthorization();
app.MapControllers();

app.Run();