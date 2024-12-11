using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Enumerations;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace StickmanSliding.Infrastructure.AssetLoading
{
    public interface ISceneLoader
    {
        AsyncOperationHandle<SceneInstance> LoadingOperation { get; }

        UniTask<Scene> Load(SceneName sceneName, LoadSceneMode mode = LoadSceneMode.Single);
        UniTask<Scene> ActivateLastLoaded();
    }
}