using Cysharp.Threading.Tasks;
using StickmanSliding.Architecture.GameStates.Global;
using StickmanSliding.Data.Static.Enumerations;
using StickmanSliding.Utilities.Patterns.State.Machines;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Architecture.Bootstrap
{
    public class EntrySceneBootstrapper : MonoBehaviour
    {
        [Inject] private IStateMachine _gameStateMachine;

        private void Start() => Initialize().Forget();

        private async UniTaskVoid Initialize()
        {
            await _gameStateMachine.ChangeState<BootstrapGameState>();
            _gameStateMachine.ChangeState<SceneLoadingGameState, SceneName>(SceneName.GameHub).Forget();
        }
    }
}