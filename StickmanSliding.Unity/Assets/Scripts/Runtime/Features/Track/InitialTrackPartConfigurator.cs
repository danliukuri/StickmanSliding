﻿using StickmanSliding.Infrastructure.ObjectCreation;
using Zenject;

namespace StickmanSliding.Features.Track
{
    public class InitialTrackPartConfigurator : IGameObjectConfigurator<InitialTrackPartEntity>
    {
        [Inject] private readonly ITrackPartPlacer                     _trackPartPlacer;
        [Inject] private readonly ITrackPartPlayerDespawningSubscriber _trackPartPlayerDespawningSubscriber;

        public void Configure(InitialTrackPartEntity trackPart)
        {
            _trackPartPlacer.Place(trackPart);

            _trackPartPlayerDespawningSubscriber.SubscribeToDespawnPlayerCubes(trackPart);
            _trackPartPlayerDespawningSubscriber.SubscribeToDespawnPlayerCharacter(trackPart);
        }
    }
}