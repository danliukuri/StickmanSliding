using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace StickmanSliding.Infrastructure.ObjectCreation
{
    public interface IGameObjectFactory<TComponent> : IDisposable where TComponent : Component
    {
        TComponent Prefab { get; }

        UniTask Initialize();

        TComponent Create();
        void       Release(TComponent component);
    }
}