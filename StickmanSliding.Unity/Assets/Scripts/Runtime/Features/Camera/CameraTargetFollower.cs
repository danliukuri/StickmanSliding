using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Camera
{
    public class CameraTargetFollower : ICameraTargetFollower
    {
        [Inject] private readonly CinemachineCamera _camera;

        public void StartFollowing(Transform target) => _camera.Follow = target;

        public void StopFollowing() => _camera.Follow = default;
    }
}