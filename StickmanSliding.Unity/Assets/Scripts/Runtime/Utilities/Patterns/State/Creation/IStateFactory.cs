using System;
using StickmanSliding.Utilities.Patterns.State.Types;

namespace StickmanSliding.Utilities.Patterns.State.Creation
{
    public interface IStateFactory
    {
        IState Create(Type stateType);
    }
}