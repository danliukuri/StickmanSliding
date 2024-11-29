using System;
using System.Collections.Generic;
using System.Linq;
using StickmanSliding.Utilities.Patterns.State.Containers;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Utilities.Patterns.State.Creation
{
    public class StatesContainerInitializer : IInitializable, IDisposable
    {
        [Inject] private readonly IStateRegister    _stateRegister;
        [Inject] private readonly IStateFactory     _stateFactory;
        [Inject] private readonly IEnumerable<Type> _stateTypes;

        private List<IState> _states;

        public void Initialize()
        {
            CreateAllStates();
            RegisterAllStates();
        }

        public void Dispose() => UnregisterAllStates();

        private void CreateAllStates()     => _states = _stateTypes.Select(_stateFactory.Create).ToList();
        private void RegisterAllStates()   => _states.ForEach(_stateRegister.Register);
        private void UnregisterAllStates() => _states.ForEach(_stateRegister.Unregister);
    }
}