using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Ragdoll
{
    /// <inheritdoc cref="Entity"/>
    public class RagdollEntity : Entity
    {
        [Inject] public IRagdollEnabler Enabler { get; }
        [Inject] public IRagdollThrower Thrower { get; }

        [field: SerializeField] public Animator Animator { get; private set; }

        public Rigidbody[]      Rigidbodies { get; private set; }
        public CharacterJoint[] Joints      { get; private set; }
        public Collider[]       Colliders   { get; private set; }

        public void Initialize()
        {
            Rigidbodies = transform.GetComponentsInChildren<Rigidbody>();
            Joints      = transform.GetComponentsInChildren<CharacterJoint>();
            Colliders   = transform.GetComponentsInChildren<Collider>();

            Enabler.SetRagdollActive(false);
        }
    }
}