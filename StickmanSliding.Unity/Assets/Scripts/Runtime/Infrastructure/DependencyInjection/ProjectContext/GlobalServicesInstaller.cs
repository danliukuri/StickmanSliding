using System;
using System.Collections.Generic;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.Track;
using StickmanSliding.Infrastructure.AssetLoading;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.ProjectContext
{
    public class GlobalServicesInstaller : MonoInstaller
    {
        [SerializeField] private PoolConfigReferences poolConfigReferences;

        public override void InstallBindings()
        {
            BindSceneLoader();
            BindAssetLoader();
            BindPoolConfigLoader();
        }

        private void BindSceneLoader() => Container.BindInterfacesTo<SceneLoader>().AsSingle();

        private void BindAssetLoader() => Container.BindInterfacesTo<AssetLoader>().AsSingle();

        private void BindPoolConfigLoader() => Container.BindInterfacesTo<PoolConfigLoader>().AsSingle()
            .WithArguments(poolConfigReferences.ToDictionary());

        [Serializable]
        public class PoolConfigReferences
        {
            [field: SerializeField] public AssetReferenceT<PoolConfig> TrackPart { get; private set; }

            public Dictionary<Type, AssetReference> ToDictionary() => new()
            {
                { typeof(TrackPart), TrackPart }
            };
        }
    }
}