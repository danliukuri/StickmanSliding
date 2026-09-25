using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Features.Player;
using StickmanSliding.Features.Track;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.ObstacleCube
{
    public class PlayerCubeDetacher : IPlayerCubeDetacher
    {
        private readonly List<ContactPoint> _contactBuffer = new();

        [Inject] private readonly ICollectableCubesParentProvider            _collectableCubesParentProvider;
        [Inject] private readonly ICollectableCubePhysicsConfigurator        _physicsConfigurator;
        [Inject] private readonly IConfigProvider<PlayerCubeDetachingConfig> _configProvider;

        public void Detach(PlayerEntity player, CollectableCubeEntity cube, TrackPartEntity trackPart)
        {
            player.State.CollectedCubes.Remove(cube);

            cube.transform.SetParent(_collectableCubesParentProvider.DefaultParent);

            _physicsConfigurator.ConfigureAsDetached(cube);

            trackPart?.State.CollectableCubes.Add(cube.transform.position, cube);
        }

        public bool ShouldDetach(Collision collision, CollectableCubeEntity cube)
        {
            Vector3 up = -Physics.gravity.normalized;

            Bounds cubeBounds = cube.Collider.bounds;
            float  cubeBottom = GetBottomHeight(cubeBounds, up);

            _contactBuffer.Clear();
            collision.GetContacts(_contactBuffer);

            return !HasSupportingSurface(cubeBounds.center, up) && HasContactHighEnoughToDetach(cubeBottom, up);
        }

        private bool HasSupportingSurface(Vector3 cubeCenter, Vector3 up) => _contactBuffer
            .Select(contact => GetNormalTowardCube(contact, cubeCenter))
            .Any(normalTowardCube => Vector3.Dot(normalTowardCube, up) > 0f);

        private bool HasContactHighEnoughToDetach(float cubeBottom, Vector3 up) => _contactBuffer
            .Select(contact => Vector3.Dot(contact.point, up) - cubeBottom)
            .Any(stepHeight => stepHeight > _configProvider.Config.MaxStepHeight);

        private Vector3 GetNormalTowardCube(ContactPoint contact, Vector3 cubeCenter) =>
            Mathf.Sign(Vector3.Dot(contact.normal, cubeCenter - contact.point)) * contact.normal;

        private float GetBottomHeight(Bounds bounds, Vector3 direction) =>
            Vector3.Dot(bounds.center, direction) - Vector3.Dot(bounds.extents, direction.Abs());
    }
}
