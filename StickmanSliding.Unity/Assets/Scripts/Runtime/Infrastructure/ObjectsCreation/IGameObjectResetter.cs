using UnityEngine;

namespace StickmanSliding.Infrastructure.ObjectsCreation
{
    public interface IGameObjectResetter<in TComponent> where TComponent : Component
    {
        public void Reset(TComponent component);
    }
}