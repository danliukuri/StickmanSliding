﻿using StickmanSliding.Data.Static.Configuration;
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
        [SerializeField] private AssetReferenceGameObject                trackPartPrefab;
        [SerializeField] private AssetReferenceT<TrackPartSpawnerConfig> trackPartSpawnerConfig;
        [SerializeField] private Transform                               trackPartsParent;

        public override void InstallBindings()
        {
            BindTrackPartFactory();
            BindTrackPartSpawnerConfigLoader();
            BindTrackPartSpawner();
            BindTrackPartTrackPartConfigurator();
        }

        private void BindTrackPartFactory() =>
            Container.BindInterfacesTo<PooledGameObjectFactory<TrackPart>>().AsSingle()
                .WithArguments(trackPartPrefab, trackPartsParent);

        private void BindTrackPartSpawnerConfigLoader() =>
            Container.BindInterfacesTo<TrackPartSpawnerConfigLoader>().AsSingle()
                .WithArguments(trackPartSpawnerConfig);

        private void BindTrackPartSpawner() =>
            Container.BindInterfacesTo<TrackPartSpawner>().AsSingle();

        private void BindTrackPartTrackPartConfigurator() =>
            Container.BindInterfacesTo<TrackPartConfigurator>().AsSingle();
    }
}