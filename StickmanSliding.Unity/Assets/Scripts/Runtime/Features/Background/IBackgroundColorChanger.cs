namespace StickmanSliding.Features.Background
{
    public interface IBackgroundColorChanger
    {
        void Initialize(float colorHue);

        void StartChanging();
        void StopChanging();
    }
}