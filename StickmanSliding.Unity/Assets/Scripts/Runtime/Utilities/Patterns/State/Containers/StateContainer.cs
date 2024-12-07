using System;
using System.Collections.Generic;
using StickmanSliding.Utilities.Patterns.State.Types;

namespace StickmanSliding.Utilities.Patterns.State.Containers
{
    public class StateContainer : IStateRegister, IStateProvider
    {
        private readonly Dictionary<Type, IState> _states = new();

        public TState Get<TState>() where TState : IState => _states.TryGetValue(typeof(TState), out IState state)
            ? (TState)state
            : throw new ArgumentException($"State machine doesn't contains the '{typeof(TState).Name}' state");

        public void Register(IState state) => _states.Add(state.GetType(), state);

        public void Unregister(IState state) => _states.Remove(state.GetType());
    }
}