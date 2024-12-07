using StickmanSliding.Architecture.GameStates.Global;
using StickmanSliding.Utilities.Patterns.State.Containers;
using StickmanSliding.Utilities.Patterns.State.Machines;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.ProjectContext
{
    public class GameStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindStateFactory();
            BindStateContainer();
            BindStateContainerInitializer();
            BindStateMachine();
        }

        private void BindStateContainerInitializer() => Container.BindStateContainerInitializer(new[]
        {
            typeof(BootstrapGameState), typeof(SceneLoadingGameState)
        });

        private void BindStateContainer() => Container.BindInterfacesTo<StateContainer>().AsSingle();

        private void BindStateMachine() => Container.BindInterfacesTo<StateMachine>().AsSingle();
    }
}