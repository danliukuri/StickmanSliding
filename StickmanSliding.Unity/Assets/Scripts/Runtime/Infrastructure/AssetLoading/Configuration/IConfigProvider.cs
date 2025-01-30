namespace StickmanSliding.Infrastructure.AssetLoading.Configuration
{
    public interface IConfigProvider<out TConfig>
    {
        TConfig Config { get; }
    }
}