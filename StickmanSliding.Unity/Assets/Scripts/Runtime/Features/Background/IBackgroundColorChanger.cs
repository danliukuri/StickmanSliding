using System;

namespace StickmanSliding.Features.Background
{
    public interface IBackgroundColorChanger : IDisposable
    {
        void IDisposable.Dispose() => StopChanging();

        void Initialize(float initialColorHue);

        void StartChanging();
        void StopChanging();
    }
}