﻿using UnityEngine;

namespace StickmanSliding.Infrastructure.ObjectCreation
{
    public interface IGameObjectResetter<in TComponent> where TComponent : Component
    {
        public void Reset(TComponent component);
    }
}