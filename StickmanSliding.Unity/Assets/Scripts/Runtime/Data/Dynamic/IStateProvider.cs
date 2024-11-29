namespace StickmanSliding.Data.Dynamic
{
    public interface IStateProvider<out TState>
    {
        public TState State { get; }
    }
}