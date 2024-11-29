using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace StickmanSliding.Infrastructure.ObjectCreation
{
    public interface IPooledGameObjectFactory<TComponent> : IDisposable where TComponent : Component
    {
        GameObject Prefab { get; }

        public UniTask Initialize();

        public TComponent Create();
        public void       Release(TComponent component);
    }
}