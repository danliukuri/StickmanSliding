using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Ragdoll
{
    public class RagdollEnabler : IRagdollEnabler
    {
        [Inject] private RagdollEntity _ragdollEntity;

        public void SetRagdollActive(bool value)
        {
            foreach (CharacterJoint ragdollJoint in _ragdollEntity.Joints)
                ragdollJoint.enableCollision = value;
            foreach (Collider ragdollCollider in _ragdollEntity.Colliders)
                ragdollCollider.enabled = value;
            foreach (Rigidbody ragdollRigidbody in _ragdollEntity.Rigidbodies)
                ragdollRigidbody.isKinematic = !value;

            _ragdollEntity.Animator.enabled = !value;
        }
    }
}