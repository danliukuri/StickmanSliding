using StickmanSliding.Features.Background;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Architecture.GameStates.GameHub
{
    public class ProcessGameHubState : IEnterableState, IExitableState
    {
        [Inject] private readonly IBackgroundColorChanger _backgroundColorChanger;

        public void Enter() => _backgroundColorChanger.StartChanging();

        public void Exit() => _backgroundColorChanger.StopChanging();
    }
}