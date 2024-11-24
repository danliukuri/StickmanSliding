using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace StickmanSliding.Infrastructure.AssetLoading
{
    public interface IAssetLoader : IDisposable
    {
        UniTask Initialize();

        UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : class;
        void            Release(AssetReference      assetReference);
    }
}