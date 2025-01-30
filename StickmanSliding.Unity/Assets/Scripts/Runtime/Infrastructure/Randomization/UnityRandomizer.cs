using UnityEngine;

namespace StickmanSliding.Infrastructure.Randomization
{
    public class UnityRandomizer : IRandomizer
    {
        private const int MinIntStep = 1;

        public int Next(int minInclusive, int maxExclusive) => Random.Range(minInclusive, maxExclusive);

        public int NextInclusive(int min, int max) => Next(min, max + MinIntStep);

        public float Next(float minInclusive, float maxExclusive) =>
            NextInclusive(minInclusive, maxExclusive - Mathf.Epsilon);

        public float NextInclusive(float min, float max) => Random.Range(min, max);
    }
}