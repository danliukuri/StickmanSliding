using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Features.Track;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Utilities.Extensions;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.ObstacleCube
{
    public class PlayerCubeDetacher : IPlayerCubeDetacher
    {
        [Inject] private readonly ICollectableCubesParentProvider            _collectableCubesParentProvider;
        [Inject] private readonly IConfigProvider<PlayerCubeDetachingConfig> _configProvider;

        public void Detach(CollectableCubeEntity cube, TrackPartEntity trackPart)
        {
            cube.transform.SetParent(_collectableCubesParentProvider.DefaultParent);

            trackPart?.State.CollectableCubes.Add(cube.transform.position, cube);
        }

        public bool IsCollisionFromDetachableDirection(Collision collision)
        {
            Vector3 contactPoint       = collision.contacts.Average(contact => contact.point);
            Vector3 collisionDirection = IgnoreSmallestAxis(collision.collider.transform.position - contactPoint);

            float objectsAngle = Vector3.Angle(collisionDirection, _configProvider.Config.NotDetachableDirection);
            return objectsAngle > _configProvider.Config.MaxDetachAngle;
        }

        private static Vector3 IgnoreSmallestAxis(Vector3 source)
        {
            Vector3 sourceAbs = source.Abs();
            return sourceAbs.x < sourceAbs.y
                ? sourceAbs.x < sourceAbs.z
                    ? new Vector3(x: default, source.y, source.z)
                    : new Vector3(source.x,   source.y, z: default)
                : sourceAbs.y < sourceAbs.z
                    ? new Vector3(source.x, y: default, source.z)
                    : new Vector3(source.x, source.y,   z: default);
        }
    }
}