using System;
using UnityEngine;

namespace StickmanSliding.Utilities.Wrappers
{
    public readonly struct InactiveGameObject : IDisposable
    {
        private readonly GameObject _gameObject;
        private readonly bool       _gameObjectState;

        public InactiveGameObject(GameObject gameObject)
        {
            _gameObject      = gameObject;
            _gameObjectState = gameObject.activeSelf;

            _gameObject.SetActive(false);
        }

        public void Dispose() => _gameObject.SetActive(_gameObjectState);
    }
}