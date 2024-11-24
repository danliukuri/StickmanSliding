using UnityEngine;

namespace StickmanSliding.Infrastructure.ObjectCreation
{
    public interface IGameObjectConfigurator<in TComponent> where TComponent : Component
    {
        public void Configure(TComponent component);
    }
}