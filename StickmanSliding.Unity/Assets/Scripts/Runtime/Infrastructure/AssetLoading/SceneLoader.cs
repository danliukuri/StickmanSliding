using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Enumerations;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace StickmanSliding.Infrastructure.AssetLoading
{
    public class SceneLoader : ISceneLoader
    {
        public AsyncOperationHandle<SceneInstance> LoadingOperation { get; private set; }

        public async UniTask<Scene> Load(SceneName sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            LoadingOperation = Addressables.LoadSceneAsync(sceneName.ToString(), mode, activateOnLoad: false);
            await LoadingOperation.ToUniTask();
            return LoadingOperation.Result.Scene;
        }

        public async UniTask<Scene> ActivateLastLoaded()
        {
            if (LoadingOperation.IsDone)
                await LoadingOperation.Result.ActivateAsync().ToUniTask();
            return LoadingOperation.Result.Scene;
        }
    }
}