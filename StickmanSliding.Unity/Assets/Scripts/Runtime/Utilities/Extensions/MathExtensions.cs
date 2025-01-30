using UnityEngine;

namespace StickmanSliding.Utilities.Extensions
{
    public static class MathExtensions
    {
        private const float EvenDivisor = 2f;

        public static bool IsZero(this float value) => Mathf.Approximately(value, Mathf.Epsilon);

        public static bool IsEven(this float value) => (value % EvenDivisor).IsZero();
    }
}