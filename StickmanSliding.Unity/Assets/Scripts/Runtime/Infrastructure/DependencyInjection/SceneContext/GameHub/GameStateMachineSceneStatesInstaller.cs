using StickmanSliding.Architecture.GameStates.GameHub;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.SceneContext.GameHub
{
    public class GameStateMachineSceneStatesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindStateFactory();
            Container.BindStateContainerInitializer(new[]
            {
                typeof(SetupGameHubState), typeof(ProcessGameHubState)
            });
        }
    }
}