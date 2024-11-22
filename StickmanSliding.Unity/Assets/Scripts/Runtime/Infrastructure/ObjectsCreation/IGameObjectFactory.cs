using UnityEngine;

namespace StickmanSliding.Infrastructure.ObjectsCreation
{
    public interface IGameObjectFactory<TComponent> where TComponent : Component
    {
        public void Initialize();
        public void Dispose();

        public TComponent Create();
        public void       Release(TComponent component);
    }
}