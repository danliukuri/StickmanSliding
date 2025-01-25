using System.Collections.Generic;
using System.Linq;
using StickmanSliding.Features.Track;
using StickmanSliding.Infrastructure.ObjectCreation;
using StickmanSliding.Infrastructure.Randomization;
using Zenject;

namespace StickmanSliding.Features.CollectableCube
{
    public class CollectableCubeSpawner : ICollectableCubeSpawner
    {
        [Inject] private readonly IGameObjectFactory<CollectableCubeEntity> _factory;
        [Inject] private readonly IRandomizer                               _randomizer;
        [Inject] private readonly ITrackPartObjectPositionGenerator         _positionGenerator;

        public void Spawn(TrackPartEntity trackPart)
        {
            int numberOfCubesToSpawn = _randomizer.NextElement(trackPart.State.WallObstacleCubesCountPerColumn);
            SpawnCubes(trackPart, numberOfCubesToSpawn);
        }

        public void Despawn(TrackPartEntity trackPart)
        {
            foreach (CollectableCubeEntity cube in trackPart.State.CollectableCubes.Values)
            {
                cube.CollectingSubscriber.UnsubscribeToCollectByPlayer();
                _factory.Release(cube);
            }

            trackPart.State.CollectableCubes.Clear();
        }

        public void Despawn(CollectableCubeEntity cube)
        {
            cube.CollectingSubscriber.UnsubscribeToCollectByPlayer();
            cube.TrackPlacementState.OriginTrackPart?.State.CollectableCubes
                .Remove(cube.TrackPlacementState.OriginLocalPosition);
            _factory.Release(cube);
        }

        private CollectableCubeEntity SpawnCube(TrackPartEntity trackPart)
        {
            CollectableCubeEntity cube = _factory.Create();

            cube.TrackPlacementState.OriginLocalPosition =
                _positionGenerator.GenerateRandomLocalPositionInGrid(trackPart, cube);
            cube.TrackPlacementState.OriginTrackPart = trackPart;
            trackPart.State.CollectableCubes.Add(cube.TrackPlacementState.OriginLocalPosition, cube);

            cube.transform.position = trackPart.transform.position + cube.TrackPlacementState.OriginLocalPosition;

            cube.CollectingSubscriber.SubscribeToCollectByPlayer();

            return cube;
        }

        private List<CollectableCubeEntity> SpawnCubes(TrackPartEntity trackPart, int count) =>
            Enumerable.Range(start: default, count).Select(index => SpawnCube(trackPart)).ToList();
    }
}