using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Configuration.ObjectCreation;

namespace StickmanSliding.Infrastructure.AssetLoading
{
    public interface IFactoryConfigurationLoader
    {
        public UniTask<FactoryConfig> Load<TComponent>();
        void                          Release<TComponent>();
    }
}