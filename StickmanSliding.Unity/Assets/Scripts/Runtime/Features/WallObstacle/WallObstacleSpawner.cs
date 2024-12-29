using StickmanSliding.Features.Track;
using StickmanSliding.Infrastructure.ObjectCreation;
using StickmanSliding.Infrastructure.Randomization;
using StickmanSliding.Utilities.Extensions;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.WallObstacle
{
    public class WallObstacleSpawner : IWallObstacleSpawner
    {
        [Inject] private readonly IGameObjectFactory<ObstacleCubeEntity> _factory;
        [Inject] private readonly IRandomizer                            _randomizer;

        public ObstacleCubeEntity Spawn(TrackPartEntity trackPart)
        {
            ObstacleCubeEntity cube = _factory.Create();

            cube.TrackPlacementState.OriginLocalPosition = GenerateRandomLocalPositionInGrid(trackPart, cube);
            cube.TrackPlacementState.OriginTrackPart     = trackPart;
            trackPart.State.ObstacleCubes.Add(cube.TrackPlacementState.OriginLocalPosition, cube);

            cube.transform.position = trackPart.transform.position + cube.TrackPlacementState.OriginLocalPosition;
            cube.transform.rotation = Quaternion.identity;

            return cube;
        }

        public void Despawn(TrackPartEntity trackPart)
        {
            foreach (ObstacleCubeEntity cube in trackPart.State.ObstacleCubes.Values)
                _factory.Release(cube);

            trackPart.State.ObstacleCubes.Clear();
        }

        private Vector3 GenerateRandomLocalPositionInGrid(TrackPartEntity trackPart, ObstacleCubeEntity cube)
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
            while (trackPart.State.CollectableCubes.ContainsKey(cubeLocalPosition) ||
                   trackPart.State.ObstacleCubes.ContainsKey(cubeLocalPosition));

            return cubeLocalPosition;
        }
    }
}