namespace StickmanSliding.Infrastructure.Randomization
{
    public static class ColorRandomizerExtensions
    {
        private const float MinComponentValueOfHSV = 0f, MaxComponentValueOfHSV = 1f;

        public static float NextHue(this IRandomizer randomizer) =>
            NextComponentOfHSV(randomizer);

        private static float NextComponentOfHSV(IRandomizer randomizer) =>
            randomizer.NextInclusive(MinComponentValueOfHSV, MaxComponentValueOfHSV);
    }
}