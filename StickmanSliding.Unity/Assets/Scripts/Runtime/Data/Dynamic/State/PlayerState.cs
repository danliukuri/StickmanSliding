using ObservableCollections;
using R3;
using StickmanSliding.Features.CollectableCube;

namespace StickmanSliding.Data.Dynamic.State
{
    public class PlayerState
    {
        public ReactiveProperty<bool>                IsCharacterGrounded { get; } = new();
        public ReactiveProperty<bool>                IsAlive             { get; } = new(true);
        public ObservableList<CollectableCubeEntity> CollectedCubes      { get; } = new();
    }
}