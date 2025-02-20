namespace StickmanSliding.Infrastructure.Randomization
{
    public interface IRandomizer
    {
        int Next(int minInclusive, int maxExclusive);

        int NextInclusive(int min, int max);

        float Next(float minInclusive, float maxExclusive);

        float NextInclusive(float min, float max);
    }
}