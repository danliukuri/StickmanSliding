using StickmanSliding.Features.Background;
using StickmanSliding.Features.Player.GameHub;
using StickmanSliding.Infrastructure.InputServices;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Architecture.GameStates.GameHub
{
    public class ProcessGameHubState : IEnterableState, IExitableState
    {
        [Inject] private readonly IBackgroundColorChanger _backgroundColorChanger;
        [Inject] private readonly IRotateInputService     _rotateInputService;
        [Inject] private readonly IPlayerProvider         _playerProvider;

        public void Enter()
        {
            _backgroundColorChanger.StartChanging();

            _rotateInputService.Enable();

            _playerProvider.Player.CharacterRotator.StartRotating();
        }

        public void Exit()
        {
            _playerProvider.Player.CharacterRotator.StopRotating();

            _rotateInputService.Disable();

            _backgroundColorChanger.StopChanging();
        }
    }
}