using StickmanSliding.Features.CollectableCube;

namespace StickmanSliding.Features.Player
{
    public interface IPlayerCubeSpawner
    {
        void SpawnInitial();

        CollectableCubeEntity Spawn();
    }
}