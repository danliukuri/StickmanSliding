using StickmanSliding.Features.Background;
using StickmanSliding.Infrastructure.Randomization;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Architecture.GameStates.GameHub
{
    public class SetupGameHubState : IEnterableState
    {
        [Inject] private readonly IBackgroundColorChanger _backgroundColorChanger;
        [Inject] private readonly IRandomizer             _randomizer;

        public void Enter() => InitializeServices();

        private void InitializeServices() => _backgroundColorChanger.Initialize(_randomizer.NextHue());
    }
}