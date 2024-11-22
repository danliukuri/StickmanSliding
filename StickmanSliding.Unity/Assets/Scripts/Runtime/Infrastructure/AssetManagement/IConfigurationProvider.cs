using StickmanSliding.Data.Static.Configuration.ObjectsCreation;

namespace StickmanSliding.Infrastructure.AssetManagement
{
    public interface IConfigurationProvider
    {
        public FactoryConfig GetFactoryConfig<TComponent>();
    }
}