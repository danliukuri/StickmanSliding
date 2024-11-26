using System;
using System.Collections.Generic;
using StickmanSliding.Utilities.Patterns.State.Creation;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Utilities.Patterns.State.Containers
{
    public class StateContainer : IStateProvider, IInitializable, IDisposable
    {
        [Inject] private readonly IStateFactory _stateFactory;
        [Inject] private readonly List<Type>    _stateTypes;

        private readonly Dictionary<Type, IState> _states = new();

        public void Initialize() => RegisterStates();
        public void Dispose()    => UnregisterStates();

        public TState Get<TState>() where TState : IState => _states.TryGetValue(typeof(TState), out IState state)
            ? (TState)state
            : throw new ArgumentException($"State machine doesn't contains the '{typeof(TState).Name}' state");

        private void RegisterStates()   => _stateTypes.ForEach(type => _states.Add(type, _stateFactory.Create(type)));
        private void UnregisterStates() => _states.Clear();
    }
}