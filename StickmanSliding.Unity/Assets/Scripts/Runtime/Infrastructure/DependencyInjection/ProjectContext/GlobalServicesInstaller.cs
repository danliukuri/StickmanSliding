using System;
using System.Collections.Generic;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Features.Track;
using StickmanSliding.Infrastructure.AssetLoading;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.InputServices;
using StickmanSliding.Infrastructure.Randomization;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.ProjectContext
{
    public class GlobalServicesInstaller : MonoInstaller
    {
        [SerializeField] private PoolConfigReferences  poolConfigReferences;
        [SerializeField] private InputActionReferences inputActionReferences;

        public override void InstallBindings()
        {
            BindSceneLoader();
            BindAssetLoader();
            BindPoolConfigLoader();
            BindInputServices();
            BindRandomizer();
        }


        private void BindSceneLoader() => Container.BindInterfacesTo<SceneLoader>().AsSingle();

        private void BindAssetLoader() => Container.BindInterfacesTo<AssetLoader>().AsSingle();

        private void BindPoolConfigLoader() => Container.BindInterfacesTo<PoolConfigLoader>().AsSingle()
            .WithArguments(poolConfigReferences.ToDictionary());

        private void BindInputServices() => Container.BindInterfacesTo<MoveInputService>().AsSingle()
            .WithArguments(inputActionReferences.Move);

        private void BindRandomizer() => Container.BindInterfacesTo<UnityRandomizer>().AsSingle();

        [Serializable]
        public class PoolConfigReferences
        {
            [field: SerializeField] public AssetReferenceT<PoolConfig> TrackPart       { get; private set; }
            [field: SerializeField] public AssetReferenceT<PoolConfig> CollectableCube { get; private set; }

            public Dictionary<Type, AssetReference> ToDictionary() => new()
            {
                [typeof(TrackPart)]       = TrackPart,
                [typeof(CollectableCube)] = CollectableCube
            };
        }

        [Serializable]
        public class InputActionReferences
        {
            [field: SerializeField] public AssetReferenceT<InputActionReference> Move { get; private set; }
        }
    }
}