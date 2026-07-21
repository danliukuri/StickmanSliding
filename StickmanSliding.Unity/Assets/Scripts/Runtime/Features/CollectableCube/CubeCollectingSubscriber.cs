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
            _respawningSubscription = _cube.CollectTrigger.OnTriggerEnterAsObservable()
                .Select(collider => collider.GetComponentInParent<PlayerEntity>())
                .Where(player => player != default)
                .Subscribe(HandlePlayerEnter);

        public void UnsubscribeToCollectByPlayer() => _respawningSubscription?.Dispose();

        private void HandlePlayerEnter(PlayerEntity player)
        {
            if (player.State.IsAlive.Value)
                CollectCube(player);
            else
                EnableKinematicCollision();
        }

        private void CollectCube(PlayerEntity player)
        {
            _spawner.Despawn(_cube);
            player.CharacterAnimatorParametersChanger.SetJumpTrigger();
            player.CubeSpawner.Spawn();
        }

        private void EnableKinematicCollision()
        {
            _cube.Collider.enabled      = true;
            _cube.Rigidbody.isKinematic = true;
        }
    }
}