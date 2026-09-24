using UnityEngine;

namespace StickmanSliding.Features.CollectableCube
{
    public class CollectableCubePhysicsConfigurator : ICollectableCubePhysicsConfigurator
    {
        private const RigidbodyConstraints PlayerStackConstraints =
            RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

        public void ConfigureAsWorldCollectible(CollectableCubeEntity cube)
        {
            cube.Rigidbody.isKinematic            = true;
            cube.Collider.enabled                 = false;
            cube.CollectTrigger.enabled           = true;
            cube.Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }

        public void ConfigureAsPlayerStack(CollectableCubeEntity cube)
        {
            cube.Rigidbody.isKinematic  =  false;
            cube.Collider.enabled       =  true;
            cube.CollectTrigger.enabled =  false;
            cube.Rigidbody.constraints  |= PlayerStackConstraints;
        }

        public void ConfigureAsDetached(CollectableCubeEntity cube)
        {
            cube.Rigidbody.isKinematic  =  false;
            cube.Collider.enabled       =  true;
            cube.CollectTrigger.enabled =  false;
            cube.Rigidbody.constraints  &= ~PlayerStackConstraints;
        }

        public void ConfigureAsThrown(CollectableCubeEntity cube)
        {
            ConfigureAsDetached(cube);
            cube.Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }

        public void ConfigureAsObstacle(CollectableCubeEntity cube)
        {
            cube.Rigidbody.isKinematic  = true;
            cube.Collider.enabled       = true;
            cube.CollectTrigger.enabled = false;
        }

        public bool IsConfiguredAsWorldCollectible(CollectableCubeEntity cube) => cube.CollectTrigger.enabled;
    }
}