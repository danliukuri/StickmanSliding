using StickmanSliding.Utilities.Patterns.State.Types;

namespace StickmanSliding.Utilities.Patterns.State.Containers
{
    public interface IStateRegister
    {
        void Register(IState state);

        void Unregister(IState state);
    }
}