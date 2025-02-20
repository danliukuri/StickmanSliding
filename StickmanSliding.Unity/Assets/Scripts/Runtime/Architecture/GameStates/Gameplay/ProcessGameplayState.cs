using StickmanSliding.Features.Background;
using StickmanSliding.Features.Camera;
using StickmanSliding.Features.Player;
using StickmanSliding.Infrastructure.InputServices;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Architecture.GameStates.Gameplay
{
    public class ProcessGameplayState : IEnterableState, IExitableState
    {
        [Inject] private readonly ICameraTargetFollower   _cameraTargetFollower;
        [Inject] private readonly IMoveInputService       _moveInputService;
        [Inject] private readonly IPlayerProvider         _playerProvider;
        [Inject] private readonly IBackgroundColorChanger _backgroundColorChanger;

        public void Enter()
        {
            _backgroundColorChanger.StartChanging();

            _cameraTargetFollower.StartFollowing(_playerProvider.Player.transform);

            _moveInputService.Enable();

            _playerProvider.Player.Mover.StartMoving();
            _playerProvider.Player.GroundedStateUpdater.StartUpdating();
            _playerProvider.Player.CharacterAnimatorParametersChanger.StartUpdatingGroundedState();
        }

        public void Exit()
        {
            _playerProvider.Player.CharacterAnimatorParametersChanger.StopUpdatingGroundedState();
            _playerProvider.Player.GroundedStateUpdater.StopUpdating();
            _playerProvider.Player.Mover.StopMoving();

            _moveInputService.Disable();

            _cameraTargetFollower.StopFollowing();

            _backgroundColorChanger.StopChanging();
        }
    }
}