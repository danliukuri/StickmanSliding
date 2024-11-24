using System;
using System.Collections.Generic;
using StickmanSliding.Architecture.GameStates;
using StickmanSliding.Utilities.Patterns.State.Containers;
using StickmanSliding.Utilities.Patterns.State.Creation;
using StickmanSliding.Utilities.Patterns.State.Machines;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.ProjectContext
{
    public class GameLifecycleBindingsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var gameStatesTypes = new List<Type> { typeof(SceneLoadingGameState) };

            BindStateFactory();
            BindStateContainer(gameStatesTypes);
            BindStateMachine();
        }

        private void BindStateFactory() => Container.BindInterfacesTo<StateFactory>().AsSingle();

        private void BindStateContainer(List<Type> gameStatesTypes) =>
            Container.BindInterfacesTo<StateContainer>().AsSingle().WithArguments(gameStatesTypes);

        private void BindStateMachine() => Container.BindInterfacesTo<StateMachine>().AsSingle();
    }
}