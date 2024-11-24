using Cysharp.Threading.Tasks;
using StickmanSliding.Utilities.Patterns.State.Types;

namespace StickmanSliding.Utilities.Patterns.State.Machines
{
    public interface IStateMachine
    {
        public UniTask ChangeState<TState>() where TState : IState;

        public UniTask ChangeState<TState, TEnterArgument>(TEnterArgument argument) where TState : IState;
    }
}