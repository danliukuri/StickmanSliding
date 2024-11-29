using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Configuration.ObjectCreation;

namespace StickmanSliding.Infrastructure.AssetLoading.Configuration
{
    public interface IFactoryConfigLoader
    {
        public UniTask<FactoryConfig> Load<TComponent>();
        void                          Release<TComponent>();
    }
}