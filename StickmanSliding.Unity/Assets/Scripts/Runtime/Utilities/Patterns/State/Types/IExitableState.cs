using Cysharp.Threading.Tasks;

namespace StickmanSliding.Utilities.Patterns.State.Types
{
    public interface IExitableState : IState
    {
        public void Exit();
    }

    public interface IAsyncExitableState : IState
    {
        public UniTask Exit();
    }
}