namespace StickmanSliding.Infrastructure.Randomization
{
    public interface IRandomizer
    {
        public int Next(int minInclusive, int maxExclusive);

        public int NextInclusive(int min, int max);
    }
}