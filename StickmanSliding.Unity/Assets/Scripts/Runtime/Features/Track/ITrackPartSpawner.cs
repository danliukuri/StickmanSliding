using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace StickmanSliding.Features.Track
{
    public interface ITrackPartSpawner
    {
        UniTask         Initialize();
        TrackPart       Spawn();
        List<TrackPart> Spawn(int count);
    }
}