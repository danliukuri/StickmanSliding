using StickmanSliding.UI.Features.EventsListeners;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace StickmanSliding.UI.DependencyInjection.SceneContext.GameHub
{
    public class UIEventListenersInstaller : MonoInstaller
    {
        [SerializeField] private UIDocument mainMenuDocument;

        public override void InstallBindings() => BindPlayButtonEventsListener();

        private void BindPlayButtonEventsListener() =>
            Container.BindInterfacesTo<PlayButtonEventsListener>().AsSingle().WithArguments(mainMenuDocument);
    }
}