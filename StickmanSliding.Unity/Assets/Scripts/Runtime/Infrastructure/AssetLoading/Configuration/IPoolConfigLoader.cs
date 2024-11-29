using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Configuration;

namespace StickmanSliding.Infrastructure.AssetLoading.Configuration
{
    public interface IPoolConfigLoader
    {
        public UniTask<PoolConfig> Load<TComponent>();
        void                       Release<TComponent>();
    }
}