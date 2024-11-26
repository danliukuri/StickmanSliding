using Cysharp.Threading.Tasks;

namespace StickmanSliding.Utilities.Patterns.State.Types
{
    public interface IAsyncExitableState : IState
    {
        public UniTask Exit();
    }

    public interface IExitableState : IAsyncExitableState
    {
        UniTask IAsyncExitableState.Exit() => UniTask.CompletedTask.ContinueWith(Exit);

        public new void Exit();
    }
}