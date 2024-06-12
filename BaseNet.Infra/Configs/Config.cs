using System.Reflection;

namespace BaseNet.Infra.Configs
{
    public class Config : IConfig
    {
        public List<Assembly> Assemblies { get; set; } = new();
        public List<Type> OpenBehaviors { get; set; } = new();
    }
}