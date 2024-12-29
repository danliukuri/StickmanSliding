using System;
using UnityEngine;

namespace StickmanSliding.Utilities.Wrappers
{
    [Serializable]
    public class Range<T>
    {
        [field: SerializeField] public T Start { get; set; }
        [field: SerializeField] public T End   { get; set; }
    }
}