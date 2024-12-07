using Cysharp.Threading.Tasks;
using StickmanSliding.Utilities.Patterns.State.Types;

namespace StickmanSliding.Utilities.Patterns.State.Machines
{
    public interface IStateMachine
    {
        UniTask ChangeState<TState>() where TState : IAsyncEnterableState;

        UniTask ChangeState<TState, TEnterArgument>(TEnterArgument argument)
            where TState : IAsyncEnterableState<TEnterArgument>;
    }
}