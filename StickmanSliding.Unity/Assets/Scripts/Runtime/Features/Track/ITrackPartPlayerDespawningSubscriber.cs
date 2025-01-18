using System;

namespace StickmanSliding.Features.Track
{
    public interface ITrackPartPlayerDespawningSubscriber : IDisposable
    {
        void SubscribeToDespawnPlayerCubes(ITrackPart trackPart);

        void SubscribeToDespawnPlayerCharacter(ITrackPart trackPart);

        void UnsubscribeFromDespawnPlayerCubes(ITrackPart trackPart);

        void UnsubscribeFromDespawnPlayerCharacter(ITrackPart trackPart);
    }
}