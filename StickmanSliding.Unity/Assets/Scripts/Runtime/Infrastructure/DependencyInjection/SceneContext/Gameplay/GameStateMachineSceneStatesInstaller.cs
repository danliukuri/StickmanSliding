using StickmanSliding.Architecture.GameStates.Gameplay;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.SceneContext.Gameplay
{
    public class GameStateMachineGameplayStatesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindStateFactory();
            Container.BindStateContainerInitializer(new[]
            {
                typeof(SetupGameplayState)
            });
        }
    }
}