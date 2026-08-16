using StickmanSliding.Features.Player;
using StickmanSliding.Features.Track;

namespace StickmanSliding.Features.ObstacleCube
{
    public interface IPlayerCollisionHandler
    {
        void HandleEnter((PlayerEntity Entity, UnityEngine.Collision Collision) player, TrackPartEntity track);
    }
}