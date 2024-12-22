using System.Collections.Generic;
using System.Linq;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.Track;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.ObjectCreation;
using StickmanSliding.Infrastructure.Randomization;
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

            Vector3 cubeLocalPosition = GenerateRandomLocalPosition(trackPart, cube);

            trackPart.State.CollectableCubes.Add(cubeLocalPosition, cube);

            cube.transform.position = trackPart.transform.position + cubeLocalPosition;
            cube.transform.rotation = Quaternion.identity;

            return cube;
        }

        private Vector3 GenerateRandomLocalPosition(TrackPart trackPart, CollectableCube cube)
        {
            (int Horizontal, int Vertical) positionExtremum = FindPositionExtremum(trackPart, cube);

            Vector3 cubeLocalPosition;
            do
            {
                int randomHorizontalPosition =
                    _randomizer.NextInclusive(-positionExtremum.Horizontal, positionExtremum.Horizontal);
                int randomVerticalPosition =
                    _randomizer.NextInclusive(-positionExtremum.Vertical, positionExtremum.Vertical);

                cubeLocalPosition = trackPart.transform.right   * randomHorizontalPosition +
                                    trackPart.transform.forward * randomVerticalPosition;
            }
            while (trackPart.State.CollectableCubes.ContainsKey(cubeLocalPosition));

            return cubeLocalPosition;
        }

        // TODO: Move width and length calculation to the properties of the objects
        private (int Horizontal, int Vertical) FindPositionExtremum(TrackPart trackPart, CollectableCube cube)
        {
            const float snapToGridDistance = 0.5f;

            float cubeWidth = Mathf.Abs(Vector3.Dot(cube.transform.forward, cube.transform.lossyScale));

            float trackPartWidth     = Mathf.Abs(Vector3.Dot(trackPart.transform.right, trackPart.Body.lossyScale));
            float horizontalPosition = trackPartWidth / 2 - snapToGridDistance - (cubeWidth / 2 - snapToGridDistance);

            float trackPartLength  = Mathf.Abs(Vector3.Dot(trackPart.transform.forward, trackPart.Body.lossyScale));
            float verticalPosition = trackPartLength / 2 - snapToGridDistance - (cubeWidth / 2 - snapToGridDistance);

            return ((int)horizontalPosition, (int)verticalPosition);
        }

        private List<CollectableCube> SpawnCubes(TrackPart trackPart, int count) =>
            Enumerable.Range(default, count).Select(index => SpawnCube(trackPart)).ToList();
    }
}