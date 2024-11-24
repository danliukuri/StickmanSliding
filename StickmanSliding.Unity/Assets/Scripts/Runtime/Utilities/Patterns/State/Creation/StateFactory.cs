using System;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Utilities.Patterns.State.Creation
{
    public class StateFactory : IStateFactory
    {
        [Inject] private IInstantiator _instantiator;

        public IState Create(Type stateType) => _instantiator.Instantiate(stateType) as IState;
    }
}