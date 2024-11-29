using StickmanSliding.Utilities.Patterns.State.Types;

namespace StickmanSliding.Utilities.Patterns.State.Containers
{
    public interface IStateRegister
    {
        public void Register(IState state);

        public void Unregister(IState state);
    }
}