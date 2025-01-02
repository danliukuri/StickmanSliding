using StickmanSliding.Infrastructure.ObjectCreation;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.CollectableCube
{
    public class CollectableCubeResetter : IGameObjectResetter<CollectableCubeEntity>
    {
        [Inject] private readonly ICollectableCubesParentProvider _collectableCubesParentProvider;

        public void Reset(CollectableCubeEntity cube)
        {
            cube.transform.SetParent(_collectableCubesParentProvider.DefaultParent);
            cube.transform.position = Vector3.zero;
            cube.transform.rotation = Quaternion.identity;

            cube.Rigidbody.isKinematic  = true;
            cube.Collider.enabled       = false;
            cube.CollectTrigger.enabled = true;
        }
    }
}