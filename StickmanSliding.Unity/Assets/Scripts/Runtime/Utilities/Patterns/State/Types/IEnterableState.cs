using Cysharp.Threading.Tasks;

namespace StickmanSliding.Utilities.Patterns.State.Types
{
    public interface IAsyncEnterableState : IState
    {
        public UniTask Enter();
    }

    public interface IAsyncEnterableState<in TArgument> : IState
    {
        public UniTask Enter(TArgument argument);
    }

    public interface IEnterableState : IAsyncEnterableState
    {
        UniTask IAsyncEnterableState.Enter() => UniTask.CompletedTask.ContinueWith(Enter);

        public new void Enter();
    }

    public interface IEnterableState<in TArgument> : IAsyncEnterableState<TArgument>
    {
        UniTask IAsyncEnterableState<TArgument>.Enter(TArgument argument) =>
            UniTask.CompletedTask.ContinueWith(() => Enter(argument));

        public new void Enter(TArgument argument);
    }
}