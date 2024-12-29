using System.Collections.Generic;
using System.Linq;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.Track;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.ObjectCreation;
using StickmanSliding.Infrastructure.Randomization;
using StickmanSliding.Utilities.Extensions;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.CollectableCube
{
    public class CollectableCubeSpawner : ICollectableCubeSpawner
    {
        [Inject] private readonly IGameObjectFactory<CollectableCubeEntity>     _factory;
        [Inject] private readonly IRandomizer                                   _randomizer;
        [Inject] private readonly IConfigProvider<CollectableCubeSpawnerConfig> _configProvider;

        public void Spawn(TrackPartEntity trackPart)
        {
            int numberOfCubesToSpawn = _randomizer.NextInclusive(_configProvider.Config.CubesRangeToSpawn);
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
            cube.TrackPlacementState.OriginTrackPart.State.CollectableCubes
                .Remove(cube.TrackPlacementState.OriginLocalPosition);
            _factory.Release(cube);
        }

        private CollectableCubeEntity SpawnCube(TrackPartEntity trackPart)
        {
            CollectableCubeEntity cube = _factory.Create();

            cube.TrackPlacementState.OriginLocalPosition = GenerateRandomLocalPositionInGrid(trackPart, cube);
            cube.TrackPlacementState.OriginTrackPart     = trackPart;
            trackPart.State.CollectableCubes.Add(cube.TrackPlacementState.OriginLocalPosition, cube);

            cube.transform.position = trackPart.transform.position + cube.TrackPlacementState.OriginLocalPosition;
            cube.transform.rotation = Quaternion.identity;

            cube.CollectingSubscriber.SubscribeToCollectByPlayer();

            return cube;
        }

        private Vector3 GenerateRandomLocalPositionInGrid(TrackPartEntity trackPart, CollectableCubeEntity cube)
        {
            var horizontalPositionExtremum = (int)(trackPart.Body.HalfWidth()  - cube.HalfWidth());
            var verticalPositionExtremum   = (int)(trackPart.Body.HalfLength() - cube.HalfLength());

            Vector3 cubeLocalPosition;
            do
            {
                int randomHorizontalPosition =
                    _randomizer.NextInclusive(-horizontalPositionExtremum, horizontalPositionExtremum);
                int randomVerticalPosition =
                    _randomizer.NextInclusive(-verticalPositionExtremum, verticalPositionExtremum);

                cubeLocalPosition = trackPart.transform.right   * randomHorizontalPosition +
                                    trackPart.transform.forward * randomVerticalPosition;
            }
            while (trackPart.State.CollectableCubes.ContainsKey(cubeLocalPosition));

            return cubeLocalPosition;
        }

        private List<CollectableCubeEntity> SpawnCubes(TrackPartEntity trackPart, int count) =>
            Enumerable.Range(start: default, count).Select(index => SpawnCube(trackPart)).ToList();
    }
}