using System.Collections.Generic;

namespace StickmanSliding.Features.Track
{
    public interface ITrackPartProvider
    {
        IEnumerable<TrackPartEntity> TrackParts { get; }
    }
}