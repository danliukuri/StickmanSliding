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
        [Inject] private readonly IGameObjectFactory<CollectableCube>           _factory;
        [Inject] private readonly IRandomizer                                   _randomizer;
        [Inject] private readonly IConfigProvider<CollectableCubeSpawnerConfig> _configProvider;

        public void Spawn(TrackPart trackPart)
        {
            int numberOfCubesToSpawn = _randomizer.NextInclusive(_configProvider.Config.CubesRangeToSpawn);
            SpawnCubes(trackPart, numberOfCubesToSpawn);
        }

        public void Despawn(TrackPart trackPart)
        {
            foreach (CollectableCube cube in trackPart.State.CollectableCubes.Values)
                _factory.Release(cube);

            trackPart.State.CollectableCubes.Clear();
        }

        private CollectableCube SpawnCube(TrackPart trackPart)
        {
            CollectableCube cube = _factory.Create();

            Vector3 cubeLocalPosition = GenerateRandomLocalPositionInGrid(trackPart, cube);

            trackPart.State.CollectableCubes.Add(cubeLocalPosition, cube);

            cube.transform.position = trackPart.transform.position + cubeLocalPosition;
            cube.transform.rotation = Quaternion.identity;

            return cube;
        }

        private Vector3 GenerateRandomLocalPositionInGrid(TrackPart trackPart, CollectableCube cube)
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

        private List<CollectableCube> SpawnCubes(TrackPart trackPart, int count) =>
            Enumerable.Range(default, count).Select(index => SpawnCube(trackPart)).ToList();
    }
}