using Cysharp.Threading.Tasks;

namespace StickmanSliding.Utilities.Patterns.State.Types
{
    public interface IEnterableState : IState
    {
        public void Enter();
    }

    public interface IEnterableState<in TArgument> : IState
    {
        public void Enter(TArgument argument);
    }

    public interface IAsyncEnterableState : IState
    {
        public UniTask Enter();
    }

    public interface IAsyncEnterableState<in TArgument> : IState
    {
        public UniTask Enter(TArgument argument);
    }
}