using Cysharp.Threading.Tasks;

namespace StickmanSliding.Utilities.Patterns.State.Types
{
    public interface IAsyncEnterableState : IState
    {
        UniTask Enter();
    }

    public interface IAsyncEnterableState<in TArgument> : IState
    {
        UniTask Enter(TArgument argument);
    }

    public interface IEnterableState : IAsyncEnterableState
    {
        UniTask IAsyncEnterableState.Enter()
        {
            Enter();
            return UniTask.CompletedTask;
        }

        new void Enter();
    }

    public interface IEnterableState<in TArgument> : IAsyncEnterableState<TArgument>
    {
        UniTask IAsyncEnterableState<TArgument>.Enter(TArgument argument)
        {
            Enter(argument);
            return UniTask.CompletedTask;
        }

        new void Enter(TArgument argument);
    }
}