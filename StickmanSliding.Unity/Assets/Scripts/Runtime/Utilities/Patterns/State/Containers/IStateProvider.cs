using StickmanSliding.Utilities.Patterns.State.Types;

namespace StickmanSliding.Utilities.Patterns.State.Containers
{
    public interface IStateProvider
    {
        TState Get<TState>() where TState : IState;
    }
}