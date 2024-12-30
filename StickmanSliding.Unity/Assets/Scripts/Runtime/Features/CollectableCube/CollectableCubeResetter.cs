using StickmanSliding.Infrastructure.ObjectCreation;
using UnityEngine;

namespace StickmanSliding.Features.CollectableCube
{
    public class CollectableCubeResetter : IGameObjectResetter<CollectableCubeEntity>
    {
        public void Reset(CollectableCubeEntity cube)
        {
            cube.transform.position = Vector3.zero;
            cube.transform.rotation = Quaternion.identity;

            cube.Collider.gameObject.layer = default;

            cube.Collider.isTrigger    = true;
            cube.Rigidbody.isKinematic = true;
        }
    }
}