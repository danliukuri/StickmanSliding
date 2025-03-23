using UnityEngine;

namespace StickmanSliding.Utilities.Extensions
{
    public static class ColorExtensions
    {
        public static Color ChangeHue(this Color color, float newHue)
        {
            Color.RGBToHSV(color, out float hue, out float saturation, out float value);
            return Color.HSVToRGB(newHue, saturation, value);
        }

        public static float GetHue(this Color color)
        {
            Color.RGBToHSV(color, out float hue, out float saturation, out float value);
            return hue;
        }
    }
}