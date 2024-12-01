using UnityEngine;

namespace StickmanSliding.Infrastructure.AssetLoading.Configuration
{
    public interface IConfigProvider<out TConfig> where TConfig : ScriptableObject
    {
        TConfig Config { get; }
    }
}