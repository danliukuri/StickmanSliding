namespace StickmanSliding.Infrastructure.InputServices
{
    public interface IMoveInputService : IInputService
    {
        float GetMovement();
    }
}