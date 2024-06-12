using BaseNet.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SANASA.Administracao.Infraestrutura.Contexts;

public class AppDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql("Server=xxxx; Database=xxxx; User Id=xxxx; Password=xxxx");
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}