using System;
using Cysharp.Threading.Tasks;
using R3;
using StickmanSliding.UI.Features.Mediation;
using StickmanSliding.UI.Utilities.Extensions;
using UnityEngine.UIElements;
using Zenject;
using static StickmanSliding.UI.Data.Static.MainMenuVisualElementsNameConstants;

namespace StickmanSliding.UI.Features.EventsListeners
{
    public class PlayButtonEventsListener : IInitializable, IDisposable
    {
        [Inject] private UIDocument _mainMenuDocument;
        [Inject] private IMediator  _uiMediator;

        private IDisposable _clickingSubscription;

        public void Initialize() => _mainMenuDocument.WaitUntilIsActiveThenDo(Subscribe).Forget();

        private void Subscribe()
        {
            var button = _mainMenuDocument.rootVisualElement.Q<Button>(PlayButton);

            _clickingSubscription = button.OnClickOnceAsObservable().Select(_ => PlayButton)
                .Subscribe(_uiMediator.Notify<ClickEvent>).AddTo(_mainMenuDocument);
        }

        public void Dispose() => _clickingSubscription?.Dispose();
    }
}
