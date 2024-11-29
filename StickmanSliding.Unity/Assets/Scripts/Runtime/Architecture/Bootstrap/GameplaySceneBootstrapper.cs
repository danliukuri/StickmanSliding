using Cysharp.Threading.Tasks;
using StickmanSliding.Architecture.GameStates.Gameplay;
using StickmanSliding.Utilities.Patterns.State.Machines;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Architecture.Bootstrap
{
    public class GameplaySceneBootstrapper : MonoBehaviour
    {
        [Inject] private IStateMachine _gameStateMachine;

        private void Start() => _gameStateMachine.ChangeState<SetupGameplayState>().Forget();
    }
}