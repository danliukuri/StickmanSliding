using System;

namespace StickmanSliding.Architecture.GameStates.Gameplay
{
    public interface IGameplayFinishingInformer
    {
        event Action GameplayFinished;
    }
}