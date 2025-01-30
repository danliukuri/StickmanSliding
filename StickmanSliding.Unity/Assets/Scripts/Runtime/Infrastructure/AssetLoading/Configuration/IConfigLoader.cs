using Cysharp.Threading.Tasks;

namespace StickmanSliding.Infrastructure.AssetLoading.Configuration
{
    public interface IConfigLoader<TConfig>
    {
        UniTask<TConfig> Load();

        void Release();
    }
}