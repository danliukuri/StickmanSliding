using Cysharp.Threading.Tasks;
using StickmanSliding.Architecture.GameStates.Global;
using StickmanSliding.Data.Static.Enumerations;

namespace StickmanSliding.Architecture.Bootstrap
{
    public class EntrySceneBootstrapper : GameBootstrapper
    {
        protected override void BootstrapScene() => 
            _gameStateMachine.ChangeState<SceneLoadingGameState, SceneName>(SceneName.Gameplay).Forget();
    }
}