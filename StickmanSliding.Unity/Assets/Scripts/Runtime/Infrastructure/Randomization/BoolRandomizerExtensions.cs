namespace StickmanSliding.Infrastructure.Randomization
{
    public static class BoolRandomizerExtensions
    {
        private const float MinProbability   = 0f;
        private const float EqualProbability = 0.5f;
        private const float MaxProbability   = 1f;

        public static bool IsProbable(this IRandomizer randomizer, float trueValueProbability = EqualProbability) =>
            randomizer.Next(MinProbability, MaxProbability) < trueValueProbability;
    }
}