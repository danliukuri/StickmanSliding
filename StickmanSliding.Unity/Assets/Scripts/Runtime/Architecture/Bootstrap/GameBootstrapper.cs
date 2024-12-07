using Cysharp.Threading.Tasks;
using StickmanSliding.Architecture.GameStates.Global;
using StickmanSliding.Utilities.Patterns.State.Machines;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Architecture.Bootstrap
{
    /// <summary>
    /// Ensures proper game bootstrapping from any scene
    /// </summary>
    public abstract class GameBootstrapper : MonoBehaviour
    {
        [Inject] protected readonly IStateMachine _gameStateMachine;

        [Inject] private readonly IStateMachineStateProvider _gameStateMachineStateProvider;

        protected void Start() => Bootstrap().Forget();

        private async UniTaskVoid Bootstrap()
        {
            await BootstrapGame();
            BootstrapScene();
        }

        private async UniTask BootstrapGame()
        {
            if (_gameStateMachineStateProvider.CurrentState == default)
                await _gameStateMachine.ChangeState<BootstrapGameState>();
        }

        protected abstract void BootstrapScene();
    }
}