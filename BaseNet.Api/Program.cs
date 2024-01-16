using System.Reflection;
using BaseNet.Infra.Configs;

var builder = WebApplication.CreateBuilder(args);

var currentAssembly = Assembly.GetAssembly(typeof(Program))!;

var configuracao = new Configuracao
{
    Assemblies = currentAssembly
        .GetReferencedAssemblies()
        .Where(e => e.FullName.StartsWith("BaseNet"))
        .Select(Assembly.Load)
        .Union(new[] { currentAssembly })
        .ToList(),
    ConnectionString = builder.Configuration.GetConnectionString("Default")
};

builder.Services.AddSingleton<IConfiguracao>(configuracao);

builder.Services.AddLibs(configuracao);
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