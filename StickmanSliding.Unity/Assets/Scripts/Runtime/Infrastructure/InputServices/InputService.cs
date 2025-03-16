using Cysharp.Threading.Tasks;
using StickmanSliding.Infrastructure.AssetLoading;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using Zenject;

namespace StickmanSliding.Infrastructure.InputServices
{
    public abstract class InputService : IInputService
    {
        [Inject] private readonly IAssetLoader                          _assetLoader;
        [Inject] private readonly AssetReferenceT<InputActionReference> _inputActionReference;

        protected InputAction _inputAction;

        public async UniTask Initialize() =>
            _inputAction = await _assetLoader.Load<InputActionReference>(_inputActionReference);

        public void Dispose()
        {
            _inputAction?.Dispose();
            _assetLoader.Release(_inputActionReference);
        }

        public void Enable()  => _inputAction.Enable();
        public void Disable() => _inputAction.Disable();
    }
}