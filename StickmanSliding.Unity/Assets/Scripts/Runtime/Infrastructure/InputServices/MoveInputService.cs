namespace StickmanSliding.Infrastructure.InputServices
{
    public class MoveInputService : InputService, IMoveInputService
    {
        public float GetMovement() => _inputAction.ReadValue<float>();
    }
}