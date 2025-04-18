﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        [Inject] private readonly IConfigProvider<WallObstacleSpawnerConfig> _configProvider;
        [Inject] private readonly IConfigProvider<TimeDependentConfig>       _timeDependentConfigProvider;
        [Inject] private readonly IConfigProvider<CubeObstacleConfig>        _cubeObstacleConfigProvider;
        [Inject] private readonly IRandomizer                                _randomizer;
        [Inject] private readonly ITrackPartObjectPositionGenerator          _positionGenerator;

        public ObstacleCubeEntity[,] Spawn(TrackPartEntity trackPart)
        {
            float[,] spawnProbabilities = PickRandomCubeSpawnProbabilities();

            Color wallColor = _randomizer.NextElement(_cubeObstacleConfigProvider.Config.AvailableColors);
            Vector3 wallPosition = _positionGenerator
                .GenerateRandomWallLocalPositionInGrid(trackPart, _factory.Prefab, spawnProbabilities);

            var cubes = new ObstacleCubeEntity[spawnProbabilities.RowLength(), spawnProbabilities.ColumnLength()];
            for (int i = spawnProbabilities.RowFirstIndex(); i < spawnProbabilities.RowLength(); i++)
                for (int j = spawnProbabilities.ColumnFirstIndex(); j < spawnProbabilities.ColumnLength(); j++)
                    if (_randomizer.IsProbable(spawnProbabilities[i, j]))
                    {
                        Vector3 cubeLocalPosition =
                            trackPart.transform.up * _factory.Prefab.Height() *
                            (spawnProbabilities.RowLastIndex() - i) +
                            trackPart.transform.right * _factory.Prefab.Width() * j;

                        cubes[i, j] = SpawnObstacleCube(trackPart, wallPosition + cubeLocalPosition);
                        cubes[i, j].Renderer.material.color = wallColor;
                    }

            trackPart.State.WallObstacleCubesCountPerColumn
                .AddRange(cubes.ColumnIndexes().Select(j => cubes.RowIndexes().Count(i => cubes[i, j] != default)));

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

            trackPart.State.WallObstacleCubesCountPerColumn.Clear();
            trackPart.State.WallObstacleCubesCountPerColumn.Clear();
        }

        private float[,] PickRandomCubeSpawnProbabilities()
        {
            var totalMinutesPassed = (float)TimeSpan.FromSeconds(Time.time).TotalMinutes;

            float allowedWallComplexity =
                _timeDependentConfigProvider.Config.WallObstacleComplexity.Evaluate(totalMinutesPassed);

            Dictionary<float[,], float> allowedSpawnProbabilities =
                _configProvider.Config.ObstacleCubeSpawnProbabilities
                    .Where(pair => _configProvider.Config.WallComplexity[pair.Key] <= allowedWallComplexity)
                    .ToDictionary(pair => pair.Value, pair => _configProvider.Config.WallComplexity[pair.Key]);

            return _randomizer.NextWeightedElement(allowedSpawnProbabilities);
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
    }
}