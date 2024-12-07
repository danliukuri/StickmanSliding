using StickmanSliding.Utilities.Patterns.State.Types;

namespace StickmanSliding.Utilities.Patterns.State.Machines
{
    public interface IStateMachineStateProvider
    {
        IState CurrentState { get; }
    }
}