namespace StickmanSliding.Infrastructure.InputServices
{
    public class RotateInputService : InputService, IRotateInputService
    {
        public float GetRotation() => _inputAction.ReadValue<float>();
    }
}