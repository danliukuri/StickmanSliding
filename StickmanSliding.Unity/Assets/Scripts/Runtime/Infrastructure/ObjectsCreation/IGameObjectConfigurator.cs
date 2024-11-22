using UnityEngine;

namespace StickmanSliding.Infrastructure.ObjectsCreation
{
    public interface IGameObjectConfigurator<in TComponent> where TComponent : Component
    {
        public void Configure(TComponent component);
    }
}