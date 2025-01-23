using StickmanSliding.Infrastructure.Randomization;
using StickmanSliding.Utilities.Extensions;
using StickmanSliding.Utilities.Wrappers;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Track
{
    public class TrackPartObjectPositionGenerator : ITrackPartObjectPositionGenerator
    {
        [Inject] private readonly IRandomizer _randomizer;

        public Vector3 GenerateRandomLocalPositionInGrid(TrackPartEntity trackPart, Dimensions size)
        {
            var horizontalPositionExtremum = (int)(trackPart.Body.HalfWidth()  - size.HalfWidth);
            var verticalPositionExtremum   = (int)(trackPart.Body.HalfLength() - size.HalfLength);

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

        public Vector3 GenerateRandomWallLocalPositionInGrid(TrackPartEntity trackPart,
                                                             Dimensions      cubeSize,
                                                             float[,]        spawnProbabilities)
        {
            Dimensions wallSize =
                new() { Width = cubeSize.Width * spawnProbabilities.ColumnLength(), Length = cubeSize.Length };

            Vector3 leftmostCubeOrigin = trackPart.transform.right * (wallSize.HalfWidth - cubeSize.HalfWidth);

            return GenerateRandomLocalPositionInGrid(trackPart, wallSize) - leftmostCubeOrigin;
        }
    }
}