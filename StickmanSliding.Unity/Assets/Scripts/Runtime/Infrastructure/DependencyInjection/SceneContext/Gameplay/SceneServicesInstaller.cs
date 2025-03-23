using StickmanSliding.Features.Background;
using StickmanSliding.Features.Camera;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.SceneContext.Gameplay
{
    public class SceneServicesInstaller : MonoInstaller
    {
        [SerializeField] private new CinemachineCamera camera;

        [SerializeField] private float backgroundColorChangingSpeed;


        public override void InstallBindings()
        {
            BindCameraTargetFollower();
            BindBackgroundColorChanger();
        }

        private void BindCameraTargetFollower() => Container.BindInterfacesTo<CameraTargetFollower>().AsSingle()
            .WithArguments(camera);

        private void BindBackgroundColorChanger()
        {
            Container.BindInterfacesTo<ColorChanger>().AsTransient();

            Container.BindInterfacesTo<SkyboxBackgroundColorChanger>().AsSingle()
                .WithArguments(backgroundColorChangingSpeed);
        }
    }
}