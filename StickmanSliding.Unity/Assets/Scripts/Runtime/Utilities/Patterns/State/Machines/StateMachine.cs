using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using StickmanSliding.Utilities.Patterns.State.Containers;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Utilities.Patterns.State.Machines
{
    public class StateMachine : IStateMachine
    {
        [Inject] private readonly IStateProvider _stateProvider;
        private                   IState         _currentState;

        public async UniTask ChangeState<TState>() where TState : IState
        {
            await ExitCurrentState();
            var newState = ChangeCurrentState<TState>();
            await EnterState(newState);
        }

        public async UniTask ChangeState<TState, TEnterArgument>(TEnterArgument argument) where TState : IState
        {
            await ExitCurrentState();
            await EnterState(ChangeCurrentState<TState>(), argument);
        }

        private async UniTask ExitCurrentState()
        {
            (_currentState as IExitableState)?.Exit();
            if (_currentState is IAsyncExitableState asyncExitableState)
                await asyncExitableState.Exit();
        }

        private TState ChangeCurrentState<TState>() where TState : IState
        {
            var newState = _stateProvider.Get<TState>();
            _currentState = newState;
            return newState;
        }

        private async Task EnterState<TState>(TState state) where TState : IState
        {
            (state as IEnterableState)?.Enter();
            if (state is IAsyncEnterableState asyncEnterableState)
                await asyncEnterableState.Enter();
        }

        private async UniTask EnterState<TState, TArgument>(TState state, TArgument argument) where TState : IState
        {
            (state as IEnterableState<TArgument>)?.Enter(argument);
            if (state is IAsyncEnterableState<TArgument> asyncEnterableState)
                await asyncEnterableState.Enter(argument);
        }
    }
}