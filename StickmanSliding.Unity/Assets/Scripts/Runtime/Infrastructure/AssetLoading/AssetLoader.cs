using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace StickmanSliding.Infrastructure.AssetLoading
{
    public class AssetLoader : IAssetLoader
    {
        private readonly Dictionary<AssetReference, AsyncOperationHandle> _assetRequests = new();

        public async UniTask Initialize() => await Addressables.InitializeAsync().ToUniTask();

        public void Dispose()
        {
            foreach (AsyncOperationHandle handle in _assetRequests.Values)
                Addressables.Release(handle);
            _assetRequests.Clear();
        }

        public async UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : class
        {
            if (!_assetRequests.TryGetValue(assetReference, out AsyncOperationHandle handle))
                _assetRequests.Add(assetReference, handle = Addressables.LoadAssetAsync<TAsset>(assetReference));

            await handle.ToUniTask();

            return handle.Result as TAsset;
        }
        
        public void Release(AssetReference assetReference)
        {
            if (_assetRequests.Remove(assetReference, out AsyncOperationHandle handle))
                Addressables.Release(handle);
        }
    }
}