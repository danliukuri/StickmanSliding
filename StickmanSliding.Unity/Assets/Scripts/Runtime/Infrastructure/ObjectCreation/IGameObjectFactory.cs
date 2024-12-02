using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace StickmanSliding.Infrastructure.ObjectCreation
{
    public interface IGameObjectFactory<TComponent> : IDisposable where TComponent : Component
    {
        public TComponent Prefab { get; }

        public UniTask Initialize();

        public TComponent Create();
        public void       Release(TComponent component);
    }
}