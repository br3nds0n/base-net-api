using System.Reflection;

namespace BaseNet.Infra.Configs
{
    public interface IConfig
    {
        public List<Assembly> Assemblies { get; set; }
        public Assembly? CurrentAssembly { get; set; }
        public List<Type> OpenBehaviors { get; set; }
    }
}