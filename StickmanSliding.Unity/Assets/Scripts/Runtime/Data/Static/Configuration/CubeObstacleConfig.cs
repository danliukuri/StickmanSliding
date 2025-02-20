using System.Collections.Generic;
using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(CubeObstacleConfig), menuName = "Configuration/Obstacles/Cube")]
    public class CubeObstacleConfig : ScriptableObject
    {
        [field: SerializeField] public List<Color> AvailableColors { get; private set; }
    }
}