using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Player
{
    public class Player : MonoBehaviour
    {
        [Inject] private IConfigLoader<PlayerConfig> _configLoader;

        [Inject] public IPlayerMover Mover { get; }

        public UniTask Initialize() => _configLoader.Load();
    }
}