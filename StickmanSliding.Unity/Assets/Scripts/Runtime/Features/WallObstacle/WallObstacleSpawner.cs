using System.Collections.Generic;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.ObstacleCube;
using StickmanSliding.Features.Track;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.ObjectCreation;
using StickmanSliding.Infrastructure.Randomization;
using StickmanSliding.Utilities.Extensions;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.WallObstacle
{
    public class WallObstacleSpawner : IWallObstacleSpawner
    {
        [Inject] private readonly IGameObjectFactory<ObstacleCubeEntity>     _factory;
        [Inject] private readonly IRandomizer                                _randomizer;
        [Inject] private readonly IConfigProvider<WallObstacleSpawnerConfig> _configProvider;

        public List<ObstacleCubeEntity> Spawn(TrackPartEntity trackPart)
        {
            float[,] spawnProbabilities =
                _randomizer.NextElement(_configProvider.Config.ObstacleCubeSpawnProbabilities).Value;

            Vector3 wallPosition = GenerateRandomWallLocalPositionInGrid(trackPart, spawnProbabilities);

            var cubes = new List<ObstacleCubeEntity>();
            for (int i = spawnProbabilities.RowFirstIndex(); i < spawnProbabilities.RowLength(); i++)
                for (int j = spawnProbabilities.ColumnFirstIndex(); j < spawnProbabilities.ColumnLength(); j++)
                    if (_randomizer.IsProbable(spawnProbabilities[i, j]))
                    {
                        Vector3 cubeLocalPosition =
                            trackPart.transform.up * _factory.Prefab.Height() *
                            (spawnProbabilities.RowLastIndex() - i) +
                            trackPart.transform.right * _factory.Prefab.Width() * j;

                        cubes.Add(SpawnObstacleCube(trackPart, wallPosition + cubeLocalPosition));
                    }

            return cubes;
        }

        public void Despawn(TrackPartEntity trackPart)
        {
            foreach (ObstacleCubeEntity cube in trackPart.State.ObstacleCubes.Values)
            {
                cube.PlayerCubeDetachingSubscriber.UnsubscribeToDetachPlayerCube(cube.PlayerCubesDetachCollider);
                _factory.Release(cube);
            }

            trackPart.State.ObstacleCubes.Clear();
        }

        private ObstacleCubeEntity SpawnObstacleCube(TrackPartEntity trackPart, Vector3 localPosition)
        {
            ObstacleCubeEntity cube = _factory.Create();

            cube.TrackPlacementState.OriginTrackPart     = trackPart;
            cube.TrackPlacementState.OriginLocalPosition = localPosition;

            cube.transform.position = trackPart.transform.position + localPosition;
            trackPart.State.ObstacleCubes.Add(localPosition, cube);

            cube.PlayerCubeDetachingSubscriber.SubscribeToDetachPlayerCube(cube.PlayerCubesDetachCollider, trackPart);

            return cube;
        }

        private Vector3 GenerateRandomWallLocalPositionInGrid(TrackPartEntity trackPart, float[,] spawnProbabilities)
        {
            (float Width, float Length) wallHalfSize =
                (_factory.Prefab.HalfWidth() * spawnProbabilities.ColumnLength(), _factory.Prefab.HalfLength());

            Vector3 leftmostCubeOrigin = trackPart.transform.right * (wallHalfSize.Width - _factory.Prefab.HalfWidth());

            return GenerateRandomLocalPositionInGrid(trackPart, wallHalfSize) - leftmostCubeOrigin;
        }

        private Vector3 GenerateRandomLocalPositionInGrid(TrackPartEntity             trackPart,
                                                          (float Width, float Length) halfSize)
        {
            var horizontalPositionExtremum = (int)(trackPart.Body.HalfWidth()  - halfSize.Width);
            var verticalPositionExtremum   = (int)(trackPart.Body.HalfLength() - halfSize.Length);

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
            while (trackPart.State.ObstacleCubes.ContainsKey(cubeLocalPosition));

            return cubeLocalPosition;
        }
    }
}