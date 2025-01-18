using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StickmanSliding.Data.Static.Configuration
{
    public partial class WallObstacleSpawnerConfig
    {
        public IReadOnlyDictionary<string, float[,]> ObstacleCubeSpawnProbabilities { get; private set; }
    }

    public partial class WallObstacleSpawnerConfig // JSON deserialization class part
    {
        [JsonExtensionData] private IDictionary<string, JToken> _deserializedData;

        [OnDeserialized]
        private void Deserialize(StreamingContext context) => ObstacleCubeSpawnProbabilities =
            _deserializedData.ToDictionary(pair => pair.Key, pair => pair.Value.ToObject<float[,]>());
    }
}