using Cysharp.Threading.Tasks;
using StickmanSliding.Architecture.GameStates.GameHub;

namespace StickmanSliding.Architecture.Bootstrap
{
    public class GameHubSceneBootstrapper : GameBootstrapper
    {
        protected override void BootstrapScene() => BootstrapSceneAsync().Forget();

        private async UniTaskVoid BootstrapSceneAsync()
        {
            await _gameStateMachine.ChangeState<SetupGameHubState>();
            _gameStateMachine.ChangeState<ProcessGameHubState>().Forget();
        }
    }
}