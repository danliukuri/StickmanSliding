using System;
using System.Linq;
using ObservableCollections;
using R3;
using StickmanSliding.Architecture.GameStates.Gameplay;
using Zenject;

namespace StickmanSliding.Features.Player
{

    public class PlayerCollectedCubesMonitor : IPlayerCollectedCubesMonitor, IGameplayFinishingInformer
    {
        public event Action GameplayFinished;

        [Inject] private readonly IPlayerProvider _playerProvider;

        private IDisposable _runOutOfCubesMonitoringSubscription;

        public void SubscribeToMonitor() =>
            _runOutOfCubesMonitoringSubscription = _playerProvider.Player.State.CollectedCubes.ObserveRemove()
                .Select(this, (removedCube, handler) => handler)
                .Where(handler => !handler._playerProvider.Player.State.CollectedCubes.Any())
                .Subscribe(handler => handler.GameplayFinished?.Invoke());

        public void UnsubscribeToMonitor() => _runOutOfCubesMonitoringSubscription?.Dispose();
    }
}