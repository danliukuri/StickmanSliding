using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Enumerations;
using StickmanSliding.Infrastructure.AssetLoading;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Architecture.GameStates
{
    public class SceneLoadingGameState : IAsyncEnterableState<SceneName>
    {
        [Inject] private readonly ISceneLoader _sceneLoader;

        public async UniTask Enter(SceneName sceneName)
        {
            await _sceneLoader.Load(sceneName);
            await _sceneLoader.ActivateLastLoaded();
        }
    }
}