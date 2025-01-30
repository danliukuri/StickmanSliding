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
            const float minExtremum    = 0f;
            const float gridHalfUnit   = 0.5f;
            const float zeroGridOffset = 0f;

            float horizontalPositionExtremum = Mathf.Max(trackPart.Body.HalfWidth()  - size.HalfWidth,  minExtremum);
            float verticalPositionExtremum   = Mathf.Max(trackPart.Body.HalfLength() - size.HalfLength, minExtremum);

            float horizontalPivotOffset = size.Width.IsEven() ? gridHalfUnit : zeroGridOffset;
            float verticalPivotOffset   = size.Length.IsEven() ? gridHalfUnit : zeroGridOffset;

            int minHorizontalPosition = -Mathf.FloorToInt(horizontalPositionExtremum);
            int maxHorizontalPosition = Mathf.CeilToInt(horizontalPositionExtremum);
            int minVerticalPosition   = -Mathf.FloorToInt(verticalPositionExtremum);
            int maxVerticalPosition   = Mathf.CeilToInt(verticalPositionExtremum);

            Vector3 cubeLocalPosition;
            do
            {
                float randomHorizontalPosition =
                    _randomizer.NextInclusive(minHorizontalPosition, maxHorizontalPosition) - horizontalPivotOffset;
                float randomVerticalPosition =
                    _randomizer.NextInclusive(minVerticalPosition, maxVerticalPosition) - verticalPivotOffset;

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