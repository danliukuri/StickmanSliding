using StickmanSliding.Features.CollectableCube;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.GameObjectContext
{
    public class CollectableCubeInstaller : MonoInstaller
    {
        [SerializeField] private CollectableCubeEntity cube;

        public override void InstallBindings() => BindCollectingSubscriber();

        private void BindCollectingSubscriber() =>
            Container.BindInterfacesTo<CubeCollectingSubscriber>().AsSingle().WithArguments(cube);
    }
}