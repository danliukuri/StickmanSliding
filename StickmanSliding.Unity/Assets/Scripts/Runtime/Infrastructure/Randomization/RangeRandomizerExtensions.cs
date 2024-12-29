using StickmanSliding.Utilities.Wrappers;

namespace StickmanSliding.Infrastructure.Randomization
{
    public static class RangeRandomizerExtensions
    {
        public static int Next(this IRandomizer randomizer, Range<int> range) =>
            randomizer.Next(range.Start, range.End);

        public static int NextInclusive(this IRandomizer randomizer, Range<int> range) =>
            randomizer.NextInclusive(range.Start, range.End);
    }
}