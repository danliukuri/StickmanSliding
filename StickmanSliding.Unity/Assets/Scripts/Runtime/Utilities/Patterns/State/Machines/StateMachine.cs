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

        public async UniTask ChangeState<TState>() where TState : IAsyncEnterableState
        {
            await ExitCurrentState();
            var newState = ChangeCurrentState<TState>();
            await EnterState(newState);
        }

        public async UniTask ChangeState<TState, TEnterArgument>(TEnterArgument argument)
            where TState : IAsyncEnterableState<TEnterArgument>
        {
            await ExitCurrentState();
            await EnterState(ChangeCurrentState<TState>(), argument);
        }

        private async UniTask ExitCurrentState()
        {
            if (_currentState is IAsyncExitableState asyncExitableState)
                await asyncExitableState.Exit();

        }

        private TState ChangeCurrentState<TState>() where TState : IState
        {
            var newState = _stateProvider.Get<TState>();
            _currentState = newState;
            return newState;
        }

        private async UniTask EnterState<TState>(TState state) where TState : IState
        {
            if (state is IAsyncEnterableState asyncEnterableState)
                await asyncEnterableState.Enter();
        }

        private async UniTask EnterState<TState, TArgument>(TState state, TArgument argument) where TState : IState
        {
            if (state is IAsyncEnterableState<TArgument> asyncEnterableState)
                await asyncEnterableState.Enter(argument);
        }
    }
}