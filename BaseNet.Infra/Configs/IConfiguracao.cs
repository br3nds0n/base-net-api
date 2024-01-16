using System.Reflection;

namespace BaseNet.Infra.Configs
{
    public interface IConfiguracao
    {
        public List<Assembly> Assemblies { get; set; }
        public string ConnectionString { get; set; }
    }
}