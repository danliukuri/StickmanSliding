using StickmanSliding.Utilities.Wrappers;
using UnityEngine;

namespace StickmanSliding.Features.Track
{
    public interface ITrackPartObjectPositionGenerator
    {
        Vector3 GenerateRandomLocalPositionInGrid(TrackPartEntity trackPart, Dimensions halfSize);

        Vector3 GenerateRandomWallLocalPositionInGrid(TrackPartEntity trackPart,
                                                      Dimensions      cubeHalfSize,
                                                      float[,]        spawnProbabilities);
    }
}