using Cysharp.Threading.Tasks;
using UnityEngine;

namespace StickmanSliding.Infrastructure.AssetLoading.Configuration
{
    public interface IConfigLoader<TConfig> where TConfig : ScriptableObject
    {
        UniTask<TConfig> Load();
        void             Release();
    }
}