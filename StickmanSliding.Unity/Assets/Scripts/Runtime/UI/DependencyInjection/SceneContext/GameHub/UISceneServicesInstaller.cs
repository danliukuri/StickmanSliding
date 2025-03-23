using StickmanSliding.UI.Features.Mediation;
using Zenject;

namespace StickmanSliding.UI.DependencyInjection.SceneContext.GameHub
{
    public class UISceneServicesInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindUIMediator();

        private void BindUIMediator() => Container.BindInterfacesTo<UIMediator>().AsSingle();
    }
}