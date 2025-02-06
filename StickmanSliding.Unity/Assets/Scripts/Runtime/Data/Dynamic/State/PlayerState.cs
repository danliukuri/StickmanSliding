using R3;

namespace StickmanSliding.Data.Dynamic.State
{
    public class PlayerState
    {
        public ReactiveProperty<bool> IsCharacterGrounded { get; } = new();
    }
}