using System;
using UnityEngine;

namespace StickmanSliding.Features.Background
{
    public interface IColorChanger
    {
        void Initialize(Color originalColor, Color initialColor, float speed, Action<Color> changeColor);
        void StartChanging();
        void StopChanging();
    }
}