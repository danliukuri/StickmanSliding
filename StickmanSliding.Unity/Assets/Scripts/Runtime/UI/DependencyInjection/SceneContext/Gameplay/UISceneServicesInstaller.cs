using StickmanSliding.UI.Features.Animation;
using StickmanSliding.UI.Features.EventsListeners;
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
            BindGameoverButtonsEventsListener();
            BindUIMediator();
            BindGameoverUIAppearingAnimator();
        }

        private void BindUIMediator() =>
            Container.BindInterfacesTo<GameoverUIMediator>().AsSingle().WithArguments(_gameoverUI.gameObject);

        private void BindGameoverButtonsEventsListener() =>
            Container.BindInterfacesTo<GameoverButtonsEventsListener>().AsSingle().WithArguments(_gameoverUI);

        private void BindGameoverUIAppearingAnimator() =>
            Container.BindInterfacesTo<GameoverUIAppearingAnimator>().AsSingle().WithArguments(_gameoverUI);
    }
}
