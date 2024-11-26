using StickmanSliding.Infrastructure.AssetLoading;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.ProjectContext
{
    public class GlobalServicesBindingsInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindSceneLoader();

        private void BindSceneLoader() => Container.BindInterfacesTo<SceneLoader>().AsSingle();
    }
}