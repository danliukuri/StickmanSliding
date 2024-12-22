using UnityEngine;

namespace StickmanSliding.Utilities.Extensions
{
    public static class TransformDimensionsExtensions
    {
        private const float HalfMultiplier = 0.5f;

        public static float Width(this  Transform transform) => transform.lossyScale.x;
        public static float Height(this Transform transform) => transform.lossyScale.y;
        public static float Length(this Transform transform) => transform.lossyScale.z;

        public static float HalfWidth(this  Transform transform) => transform.Width()  * HalfMultiplier;
        public static float HalfHeight(this Transform transform) => transform.Height() * HalfMultiplier;
        public static float HalfLength(this Transform transform) => transform.Length() * HalfMultiplier;
    }

    public static class ComponentDimensionsExtensions
    {
        public static float Width(this  Component component) => component.transform.Width();
        public static float Height(this Component component) => component.transform.Height();
        public static float Length(this Component component) => component.transform.Length();

        public static float HalfWidth(this  Component component) => component.transform.HalfWidth();
        public static float HalfHeight(this Component component) => component.transform.HalfHeight();
        public static float HalfLength(this Component component) => component.transform.HalfLength();
    }
}