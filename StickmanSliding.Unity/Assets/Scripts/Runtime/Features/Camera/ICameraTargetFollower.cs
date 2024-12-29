using UnityEngine;

namespace StickmanSliding.Features.Camera
{
    public interface ICameraTargetFollower
    {
        void StartFollowing(Transform target);
        void StopFollowing();
    }
}