using System;
using R3;
using R3.Triggers;
using StickmanSliding.Features.Player;
using Zenject;

namespace StickmanSliding.Features.CollectableCube
{
    public class CubeCollectingSubscriber : ICubeCollectingSubscriber
    {
        [Inject] private readonly ICollectableCubeSpawner _spawner;
        [Inject] private readonly CollectableCubeEntity   _cube;

        private IDisposable _respawningSubscription;

        public void SubscribeToCollectByPlayer() =>
            _respawningSubscription = _cube.Collider.OnTriggerEnterAsObservable()
                .Select(collider => collider.GetComponentInParent<PlayerEntity>())
                .Where(player => player != default)
                .Subscribe(CollectCube);

        public void UnsubscribeToCollectByPlayer() => _respawningSubscription.Dispose();

        private void CollectCube(PlayerEntity player)
        {
            _spawner.Despawn(_cube);
            player.CubeSpawner.Spawn();
        }
    }
}