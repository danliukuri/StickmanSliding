using System;
using System.Collections.Generic;
using R3;
using R3.Triggers;
using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Features.Player;
using StickmanSliding.Infrastructure.ObjectCreation;
using Zenject;

namespace StickmanSliding.Features.Track
{
    public class TrackPartPlayerDespawningSubscriber : ITrackPartPlayerDespawningSubscriber
    {
        [Inject] private readonly ICollectableCubeSpawner          _collectableCubeSpawner;
        [Inject] private readonly IGameObjectFactory<PlayerEntity> _playerFactory;

        private readonly Dictionary<ITrackPart, IDisposable> _playerCubesDespawningSubscriptions     = new();
        private readonly Dictionary<ITrackPart, IDisposable> _playerCharacterDespawningSubscriptions = new();

        public void Dispose()
        {
            foreach (IDisposable subscription in _playerCubesDespawningSubscriptions.Values)
                subscription.Dispose();
            _playerCubesDespawningSubscriptions.Clear();

            foreach (IDisposable subscription in _playerCharacterDespawningSubscriptions.Values)
                subscription.Dispose();
            _playerCharacterDespawningSubscriptions.Clear();
        }

        public void SubscribeToDespawnPlayerCubes(ITrackPart trackPart)
        {
            if (!_playerCubesDespawningSubscriptions.ContainsKey(trackPart))
                _playerCubesDespawningSubscriptions.Add(trackPart,
                    trackPart.PlayerDespawnTrigger.OnTriggerEnterAsObservable()
                        .Select(collider => collider.GetComponentInParent<CollectableCubeEntity>())
                        .Where(cube => cube != default)
                        .Subscribe(_collectableCubeSpawner.Despawn)
                );
        }

        public void SubscribeToDespawnPlayerCharacter(ITrackPart trackPart)
        {
            if (!_playerCharacterDespawningSubscriptions.ContainsKey(trackPart))
                _playerCharacterDespawningSubscriptions.Add(trackPart,
                    trackPart.PlayerDespawnTrigger.OnTriggerEnterAsObservable()
                        .Where(collider => collider.GetComponentInParent<PlayerCharacterEntity>() != default)
                        .Select(collider => collider.GetComponentInParent<PlayerEntity>())
                        .Where(player => player != default)
                        .Subscribe(_playerFactory.Release)
                );
        }

        public void UnsubscribeFromDespawnPlayerCubes(ITrackPart trackPart)
        {
            if (_playerCubesDespawningSubscriptions.Remove(trackPart, out IDisposable subscription))
                subscription.Dispose();
        }

        public void UnsubscribeFromDespawnPlayerCharacter(ITrackPart trackPart)
        {
            if (_playerCharacterDespawningSubscriptions.Remove(trackPart, out IDisposable subscription))
                subscription.Dispose();
        }
    }
}