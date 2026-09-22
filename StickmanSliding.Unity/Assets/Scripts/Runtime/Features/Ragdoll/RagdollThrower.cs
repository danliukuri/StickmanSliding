using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.Player;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Ragdoll
{
    public class RagdollThrower : IRagdollThrower
    {
        [Inject] private readonly RagdollEntity                  _ragdollEntity;
        [Inject] private readonly IConfigProvider<RagdollConfig> _ragdollConfigProvider;
        [Inject] private readonly IPlayerDirectionProvider       _playerDirectionProvider;

        public void ThrowInPlayerDirection() => Throw(_playerDirectionProvider.Direction);

        private void Throw(Vector3 direction)
        {
            _ragdollEntity.Enabler.SetRagdollActive(true);

            Vector3 forceVector = direction.normalized * _ragdollConfigProvider.Config.ThrowForce;

            foreach (Rigidbody rigidbody in _ragdollEntity.Rigidbodies)
                rigidbody.AddForce(forceVector, ForceMode.Impulse);
        }
    }
}