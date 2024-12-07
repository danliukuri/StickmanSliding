using Cysharp.Threading.Tasks;
using StickmanSliding.Infrastructure.AssetLoading;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Architecture.GameStates.Global
{
    public class BootstrapGameState : IAsyncEnterableState
    {
        [Inject] private readonly IAssetLoader _assetLoader;

        public UniTask Enter() => _assetLoader.Initialize();
    }
}