using StickmanSliding.Features.Background;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.SceneContext.GameHub
{
    public class SceneServicesInstaller : MonoInstaller
    {
        [SerializeField] private new Camera camera;

        [SerializeField] private float backgroundColorChangingSpeed;

        public override void InstallBindings() => BindBackgroundColorChanger();

        private void BindBackgroundColorChanger()
        {
            Container.BindInterfacesTo<ColorChanger>().AsTransient();

            Container.BindInterfacesTo<CameraBackgroundColorChanger>().AsSingle()
                .WithArguments(backgroundColorChangingSpeed, camera);
        }
    }
}