using UnityEngine;

namespace StickmanSliding.Infrastructure.ObjectCreation
{
    public interface IGameObjectConfigurator<in TComponent> where TComponent : Component
    {
        void Configure(TComponent component);
    }
}