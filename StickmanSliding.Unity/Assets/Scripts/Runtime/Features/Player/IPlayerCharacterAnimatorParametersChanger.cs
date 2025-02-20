namespace StickmanSliding.Features.Player
{
    public interface IPlayerCharacterAnimatorParametersChanger
    {
        void SetJumpTrigger();

        void StartUpdatingGroundedState();
        void StopUpdatingGroundedState();
    }
}