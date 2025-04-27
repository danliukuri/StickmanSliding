using StickmanSliding.UI.Features.Animation;
using StickmanSliding.UI.Features.Mediation;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace StickmanSliding.UI.DependencyInjection.SceneContext.Gameplay
{
    public class UISceneServicesInstaller : MonoInstaller
    {
        [SerializeField] private UIDocument _gameoverUI;

        public override void InstallBindings()
        {
            BindUIMediator();
            BindGameoverUIAppearingAnimator();
        }

        private void BindUIMediator() =>
            Container.BindInterfacesTo<GameplayUIMediator>().AsSingle().WithArguments(_gameoverUI.gameObject);

        private void BindGameoverUIAppearingAnimator() =>
            Container.BindInterfacesTo<GameoverUIAppearingAnimator>().AsSingle().WithArguments(_gameoverUI);
    }
}