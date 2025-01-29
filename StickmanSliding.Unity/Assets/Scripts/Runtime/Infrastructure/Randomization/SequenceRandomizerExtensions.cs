using System.Collections.Generic;
using System.Linq;

namespace StickmanSliding.Infrastructure.Randomization
{
    public static class SequenceRandomizerExtensions
    {
        private const int MinSequenceLength = 0;

        public static int NextElementIndex<T>(this IRandomizer randomizer, IReadOnlyCollection<T> collection) =>
            randomizer.Next(MinSequenceLength, collection.Count);

        public static T NextElement<T>(this IRandomizer randomizer, IReadOnlyCollection<T> collection) =>
            collection.ElementAt(randomizer.NextElementIndex(collection));

        public static T NextWeightedElement<T>(this IRandomizer                            randomizer,
                                               IReadOnlyCollection<KeyValuePair<T, float>> weightedElements)
        {
            const float minWeight     = 0f;
            float       maxWeight     = weightedElements.Sum(pair => pair.Value);
            float       currentWeight = randomizer.Next(minWeight, maxWeight - float.Epsilon);

            return weightedElements
                .OrderBy(item => item.Value)
                .ToArray()
                .First(item => (currentWeight -= item.Value) < minWeight).Key;
        }
    }
}