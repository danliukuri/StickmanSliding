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
    }
}