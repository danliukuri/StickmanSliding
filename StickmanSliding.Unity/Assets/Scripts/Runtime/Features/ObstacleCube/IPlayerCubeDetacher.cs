using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Features.Player;
using StickmanSliding.Features.Track;
using UnityEngine;

namespace StickmanSliding.Features.ObstacleCube
{
    public interface IPlayerCubeDetacher
    {
        void Detach(PlayerEntity player, CollectableCubeEntity cube, TrackPartEntity trackPart);

        bool IsCollisionFromDetachableDirection(Collision collision);
    }
}