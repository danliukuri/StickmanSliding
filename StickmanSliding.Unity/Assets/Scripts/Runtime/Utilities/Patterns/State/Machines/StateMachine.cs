using Cysharp.Threading.Tasks;
using StickmanSliding.Utilities.Patterns.State.Containers;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Utilities.Patterns.State.Machines
{
    public class StateMachine : IStateMachine, IStateMachineStateProvider
    {
        [Inject] private readonly IStateProvider _stateProvider;

        public IState CurrentState { get; private set; }

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
            var newState = ChangeCurrentState<TState>();
            await EnterState(newState, argument);
        }

        private UniTask ExitCurrentState() =>
            CurrentState is IAsyncExitableState asyncExitableState
                ? asyncExitableState.Exit()
                : UniTask.CompletedTask;

        private TState ChangeCurrentState<TState>() where TState : IState
        {
            var newState = _stateProvider.Get<TState>();
            CurrentState = newState;
            return newState;
        }

        private UniTask EnterState<TState>(TState state) where TState : IState =>
            state is IAsyncEnterableState asyncEnterableState
                ? asyncEnterableState.Enter()
                : UniTask.CompletedTask;

        private UniTask EnterState<TState, TArgument>(TState state, TArgument argument) where TState : IState =>
            state is IAsyncEnterableState<TArgument> asyncEnterableState
                ? asyncEnterableState.Enter(argument)
                : UniTask.CompletedTask;
    }
}