using StickmanSliding.Utilities.Wrappers;
using UnityEngine;

namespace StickmanSliding.Utilities.Extensions.Wrappers
{
    public static class InactiveGameObjectExtensions
    {
        public static InactiveGameObject AsInactive(this GameObject gameObject) => new(gameObject);
        public static InactiveGameObject AsInactive(this Component  component)  => component.gameObject.AsInactive();
    }
}