using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(TimeDependentConfig), menuName = "Configuration/Player/TimeDependent")]
    public class TimeDependentConfig : ScriptableObject
    {
        [field: SerializeField] public AnimationCurve WallObstacleComplexity { get; private set; }
    }
}