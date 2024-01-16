using System.Reflection;

namespace BaseNet.Infra.Configs
{
    public class Configuracao : IConfiguracao
    {
        public List<Assembly> Assemblies { get; set; } = new();
        public string ConnectionString { get; set; }
    }
}