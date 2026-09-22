using Cysharp.Threading.Tasks;
using StickmanSliding.Architecture.GameStates.Gameplay;

namespace StickmanSliding.Architecture.Bootstrap
{
    public class GameplaySceneBootstrapper : GameBootstrapper
    {
        protected override void BootstrapScene() => BootstrapSceneAsync().Forget();

        private async UniTaskVoid BootstrapSceneAsync()
        {
            await _gameStateMachine.ChangeState<SetupGameplayState>();
            await _gameStateMachine.ChangeState<ProcessGameplayState>();
            await _gameStateMachine.ChangeState<FinishGameplayState>();
        }
    }
}