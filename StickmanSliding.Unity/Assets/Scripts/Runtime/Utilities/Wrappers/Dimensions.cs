using UnityEngine;

namespace StickmanSliding.Utilities.Wrappers
{
    public struct Dimensions
    {
        private const float HalfDimensionMultiplier = 0.5f;

        public float Width  { get; set; }
        public float Height { get; set; }
        public float Length { get; set; }

        public float HalfWidth  => Width  * HalfDimensionMultiplier;
        public float HalfHeight => Height * HalfDimensionMultiplier;
        public float HalfLength => Length * HalfDimensionMultiplier;

        public static implicit operator Dimensions(Vector3 dimensions) =>
            new() { Width = dimensions.x, Height = dimensions.y, Length = dimensions.z };

        public static implicit operator Dimensions(Transform transform) => transform.lossyScale;

        public static implicit operator Dimensions(Component component) => component.transform;
    }
}