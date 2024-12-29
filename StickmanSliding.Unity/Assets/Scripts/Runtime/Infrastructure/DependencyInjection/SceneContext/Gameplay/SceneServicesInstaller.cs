using StickmanSliding.Features.Camera;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.SceneContext.Gameplay
{
    public class SceneServicesInstaller : MonoInstaller
    {
        [SerializeField] private new CinemachineCamera camera;

        public override void InstallBindings() => BindCameraTargetFollower();

        private void BindCameraTargetFollower() => Container.BindInterfacesTo<CameraTargetFollower>().AsSingle()
            .WithArguments(camera);
    }
}