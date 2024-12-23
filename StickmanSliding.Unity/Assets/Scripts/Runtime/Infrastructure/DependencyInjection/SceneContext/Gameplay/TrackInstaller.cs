using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.Track;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.ObjectCreation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.SceneContext.Gameplay
{
    public class TrackInstaller : MonoInstaller
    {
        [SerializeField] private AssetReferenceT<TrackSpawnerConfig> trackSpawnerConfig;
        [SerializeField] private AssetReferenceGameObject            trackPartPrefab;
        [SerializeField] private Transform                           trackPartsParent;

        public override void InstallBindings()
        {
            BindTrackPartFactory();
            BindTrackSpawnerConfigLoader();
            BindTrackSpawner();
        }

        private void BindTrackPartFactory() =>
            Container.BindInterfacesTo<PooledGameObjectFactory<TrackPartEntity>>().AsSingle()
                .WithArguments(trackPartPrefab, trackPartsParent);

        private void BindTrackSpawnerConfigLoader() =>
            Container.BindInterfacesTo<ConfigLoader<TrackSpawnerConfig>>().AsSingle()
                .WithArguments(trackSpawnerConfig);

        private void BindTrackSpawner() =>
            Container.BindInterfacesTo<TrackSpawner>().AsSingle();
    }
}