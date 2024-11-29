using System;
using System.Collections.Generic;
using StickmanSliding.Data.Static.Configuration.ObjectCreation;
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
        [SerializeField] private FactoryConfigReferences factoryConfigReferences;

        public override void InstallBindings()
        {
            BindSceneLoader();
            BindAssetLoader();
            BindFactoryConfigLoader();
        }

        private void BindSceneLoader() => Container.BindInterfacesTo<SceneLoader>().AsSingle();

        private void BindAssetLoader() => Container.BindInterfacesTo<AssetLoader>().AsSingle();

        private void BindFactoryConfigLoader() => Container.BindInterfacesTo<FactoryConfigLoader>().AsSingle()
            .WithArguments(factoryConfigReferences.ToDictionary());

        [Serializable]
        public class FactoryConfigReferences
        {
            [field: SerializeField] public AssetReferenceT<FactoryConfig> TrackPart { get; private set; }

            public Dictionary<Type, AssetReference> ToDictionary() => new()
            {
                { typeof(TrackPart), TrackPart }
            };
        }
    }
}