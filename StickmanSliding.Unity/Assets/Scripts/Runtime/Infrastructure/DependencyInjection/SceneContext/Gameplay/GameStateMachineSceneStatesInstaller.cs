using StickmanSliding.Architecture.GameStates.Gameplay;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.SceneContext.Gameplay
{
    public class GameStateMachineSceneStatesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindStateFactory();
            Container.BindStateContainerInitializer(new[]
            {
                typeof(SetupGameplayState), typeof(ProcessGameplayState)
            });
        }
    }
}