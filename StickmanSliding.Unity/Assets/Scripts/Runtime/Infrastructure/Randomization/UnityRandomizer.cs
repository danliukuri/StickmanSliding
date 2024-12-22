using UnityEngine;

namespace StickmanSliding.Infrastructure.Randomization
{
    public class UnityRandomizer : IRandomizer
    {
        private const int MinIntStep = 1;

        public int Next(int minInclusive, int maxExclusive) => Random.Range(minInclusive, maxExclusive);

        public int NextInclusive(int min, int max) => Next(min, max + MinIntStep);
    }
}