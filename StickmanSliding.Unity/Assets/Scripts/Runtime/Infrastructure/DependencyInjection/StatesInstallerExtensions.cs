using System;
using System.Collections.Generic;
using StickmanSliding.Utilities.Patterns.State.Creation;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection
{
    public static class StatesInstallerExtensions
    {
        public static void BindStateFactory(this DiContainer container) =>
            container.BindInterfacesTo<StateFactory>().AsSingle();

        public static void BindStateContainerInitializer(this DiContainer container, IEnumerable<Type> stateTypes) =>
            container.BindInterfacesTo<StatesContainerInitializer>().AsSingle().WithArguments(stateTypes);
    }
}